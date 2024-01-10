using GemBox.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static JoinningDataManager.JdmConst;

namespace JoinningDataManager
{
    public class JdmDataReaderExcel
    {
        public static string GEMBOX_LIC = "SN-2023May23-OQ1MibKojfVvZoPCvWlB0q7iakOKFM4c0IZiRe2u605diDgqM17xjS0uBeCZXtREEREWQ71d6K3zkJXK1QXD/K5Z4AA==A";
        public JdmDataReaderExcel()
        {
            SpreadsheetInfo.SetLicense(GEMBOX_LIC);
        }
        public List<JdmVdlPoint> ExtractVdlPoints(string vdlExcelPath, string sheetName, List<JdmColumnConfig> vdlColumnConfig, int vdlStartRowIndex)
        {
            var vdlExcel = ExcelFile.Load(vdlExcelPath);

            if (!vdlExcel.Worksheets.Any(m => m.Name.Equals(sheetName)))
                throw new ArgumentOutOfRangeException(nameof(sheetName), "Excel doe not have sheet with required name");

            var vdlPoints = new List<JdmVdlPoint>();
            var usedRange = vdlExcel.Worksheets[sheetName].GetUsedCellRange(true);

            for (int rowIndex = vdlStartRowIndex; rowIndex < usedRange.LastRowIndex; rowIndex++)
            {
                var newPoint = GetVdlPointFromExcelRow(sheetName, vdlColumnConfig, vdlExcel, rowIndex);
                if (newPoint is not null)
                    vdlPoints.Add(newPoint);
            }

            return vdlPoints;
        }


        /// <param name="sheetName"></param>
        /// <param name="vdlColumnConfig"></param>
        /// <param name="vdlExcel"></param>
        /// <param name="rowIndex"></param>
        /// <returns>null when row does not have point name</returns>
        private static JdmVdlPoint GetVdlPointFromExcelRow(string sheetName, List<JdmColumnConfig> vdlColumnConfig, ExcelFile vdlExcel, int rowIndex)
        {
            var values = new Dictionary<string, string>();
            var pointName = GetCellValueByIndexInCofig(sheetName, vdlColumnConfig, 0, vdlExcel, rowIndex);

            if (string.IsNullOrEmpty(pointName))
                return null;

            for (int i = 1; i < vdlColumnConfig.Count; i++)
            {
                var columnValue = GetCellValueByIndexInCofig(sheetName, vdlColumnConfig, i, vdlExcel, rowIndex);
                values.Add(vdlColumnConfig[i].FieldName, columnValue?.Trim());
            }

            var newPoint = new JdmVdlPoint(pointName, values);
            return newPoint;
        }

        private static string GetCellValueByIndexInCofig(string sheetName, List<JdmColumnConfig> vdlColumnConfig, int i, ExcelFile vdlExcel, int rowIndex)
        {
            var fieldNameVsColumnName = vdlColumnConfig.ElementAt(i);
            var columnIndex = ExcelColumnCollection.ColumnNameToIndex(fieldNameVsColumnName.VdlColumnName);
            var cell = vdlExcel.Worksheets[sheetName].Cells[rowIndex, columnIndex];

            if (cell.Value is null)
                return null;
            return cell.StringValue;
        }

        public List<JdmRawVtaPoint> ExtractVtaPointsFromCsvs(string vtaCsvsPath, string[] fieldNames)
        {
            var allXmlsInDir = Directory.GetFiles(vtaCsvsPath, "*.csv", SearchOption.AllDirectories);
            var allPoints = new List<JdmRawVtaPoint>();

            foreach (var csvPath in allXmlsInDir)
            {
                var points = ReadVtaCsv(csvPath, fieldNames);
                foreach (var point in points)
                {
                    var theSamePoint = allPoints.FirstOrDefault(m => m.Name.Equals(point.Name));
                    if (theSamePoint is not null)
                    {
                        if (theSamePoint.HasTheSameFields(point.FieldNameVsValue))
                            continue;

                        throw new InvalidCastException();
                    }

                    allPoints.Add(point);
                }
            }

            return allPoints;
        }




        private static List<JdmRawVtaPoint> ReadVtaCsv(string filePath, string[] fieldNames)
        {
            var allLines = File.ReadAllLines(filePath).ToList();
            var firstLine = allLines.First(m => m.StartsWith("\"Elements\""));
            var firstLineIndex = allLines.IndexOf(firstLine);

            var lastLine = allLines.First(m => m.Equals("\"Support Points\""));
            var lastLineIndex = allLines.IndexOf(lastLine);

            if (!IsCsvTitleLineOk(allLines[firstLineIndex + 1], out var titlesList))
                throw new InvalidCastException();

            var result = new List<JdmRawVtaPoint>();

            for (int i = firstLineIndex + 2; i < lastLineIndex; i++)
            {
                var line = allLines[i];

                if (string.IsNullOrEmpty(line))
                    break;

                var point = GetPointFromOneCsvRow(fieldNames, line, titlesList);
                result.Add(point);
            }

            return result;
        }

        private static JdmRawVtaPoint GetPointFromOneCsvRow(string[] fieldNames, string line, List<string> titlesList)
        {
            var rowSplitted = line.Split(';').ToList();

            var pointNameRowIndex = PointNameRowIndexByTitleName(titlesList, FIELD_NAME_ELEMENT);
            var pointName = RemoveDoubleQuoteFromStartAndEnd(rowSplitted[pointNameRowIndex]);

            var fields = new Dictionary<string, string>();
            foreach (var fieldName in fieldNames)
            {
                var fieldIndex = PointNameRowIndexByTitleName(titlesList, fieldName);
                fields.Add(fieldName, RemoveDoubleQuoteFromStartAndEnd(rowSplitted[fieldIndex]));
            }

            return new JdmRawVtaPoint(pointName, fields);
        }

        private static string RemoveDoubleQuoteFromStartAndEnd(string input)
        {
            if (input.StartsWith("\""))
                input = input.Substring(1);

            if (input.EndsWith("\""))
                input = input.Substring(0, input.Length - 1);

            return input;
        }

        private static int PointNameRowIndexByTitleName(List<string> titlesList, string title) => titlesList.IndexOf("\"" + title + "\"");


        private static bool IsCsvTitleLineOk(string titleLine, out List<string> titlesList)
        {
            titlesList = null;

            if (!titleLine.StartsWith("\"Element\""))
                return false;

            var splitted = titleLine.Split(';').ToList();

            var lastTitleIndex = splitted.IndexOf("\"Sicherheitsklasse_Kleben\"");

            if (lastTitleIndex != 65)
                return false;

            titlesList = splitted.GetRange(0, lastTitleIndex - 1);

            return true;
        }


    }
}