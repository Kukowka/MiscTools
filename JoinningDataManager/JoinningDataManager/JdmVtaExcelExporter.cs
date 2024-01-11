using GemBox.Spreadsheet;
using System.Collections.Generic;

namespace JoinningDataManager;

public class JdmVtaExcelExporter
{
    public void ExportVta2Excel(string path, List<JdmRawVtaPoint> points, List<JdmColumnConfig> columnConfig)
    {
        var workbook = new ExcelFile();

        var worksheet = workbook.Worksheets.Add("Vta export");

        foreach (var config in columnConfig)
        {
            var columnIndex = ExcelColumnCollection.ColumnNameToIndex(config.VdlColumnName);
            worksheet.Cells[0, columnIndex].SetValue(config.FieldName);
        }

        for (var index = 0; index < points.Count; index++)
        {
            var point = points[index];
            foreach (var config in columnConfig)
            {
                var columnIndex = ExcelColumnCollection.ColumnNameToIndex(config.VdlColumnName);
                worksheet.Cells[index + 1, columnIndex].SetValue(point.GetFields2Compare(config.FieldName));
            }
        }

        workbook.Save(path);
    }
}