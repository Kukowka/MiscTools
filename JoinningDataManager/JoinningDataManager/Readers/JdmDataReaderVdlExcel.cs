using GemBox.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JoinningDataManager
{
    public class JdmDataReaderVdlExcel
    {
        public JdmDataReaderVdlExcel()
        {

        }

        public static bool HaveVdlPointsUniqueNames(List<JdmVdlPoint> vdlPoints)
        {
            var grps = vdlPoints.GroupBy(m => m.Name);

            if (grps.Any(m => m.Count() > 1))
            {
                var notUniqueNames = grps.Where(m => m.Count() > 1);
                var pointsWithDifferentXYZ = new List<string>();

                foreach (var grp in notUniqueNames)
                {
                    if (grp.Count() != 2)
                    {
                        if (grp.Key.Equals("992.899.001_NAHT_003"))
                            continue;
                    }

                    if (!grp.ElementAt(0).HasSameXyz(grp.ElementAt(1)))
                        pointsWithDifferentXYZ.Add(grp.Key);
                }

                return false;
            }

            return true;
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








    }
}