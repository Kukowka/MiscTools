﻿using GemBox.Spreadsheet;
using System.Collections.Generic;
using System.Linq;

namespace JoinningDataManager;

public class JdmVtaExcelExporter
{
    private static string fieldVal;
    public const string VTA_EXPORT_SHEET_NAME = "Vta export";

    public void ExportVta2Excel(string path, List<JdmRawVtaPoint> points, List<JdmColumnConfig> columnConfig)
    {
        var workbook = new ExcelFile();

        var worksheet = workbook.Worksheets.Add(VTA_EXPORT_SHEET_NAME);

        ExportPoints2ExcelWorkSheet(points.Cast<IJdmComparable>().ToList(), columnConfig, worksheet);

        workbook.Save(path);
    }

    public static void ExportPoints2ExcelWorkSheet(List<IJdmComparable> points, List<JdmColumnConfig> columnConfig, ExcelWorksheet worksheet)
    {
        foreach (var config in columnConfig)
        {
            var columnIndex = ExcelColumnCollection.ColumnNameToIndex(config.VdlColumnName);
            worksheet.Cells[0, columnIndex].SetValue(config.FieldName);
        }

        for (var index = 0; index < points.Count; index++)
        {
            var point = points[index];

            //if (point.Name.Equals("983.800.701___-025-H2-002-R"))
            //{
            //}

            foreach (var config in columnConfig)
            {
                var columnIndex = ExcelColumnCollection.ColumnNameToIndex(config.VdlColumnName);
                fieldVal = point.GetField2Compare(config.FieldName);
                worksheet.Cells[index + 1, columnIndex].SetValue(fieldVal);
            }
        }
    }
}


