using GemBox.Spreadsheet;
using System.Collections.Generic;
using System.Linq;

namespace JoinningDataManager;

public class JdmChangesExporter
{
    public const string NEW_POINTS_SHEET_NAME = "New points";
    public const string CHANGED_PARAM_SHEET_NAME = "Changed params";
    public const string DELETED_POINTS_SHEET_NAME = "Deleted points";

    private ExcelFile _workbook;

    public void ExportChanges2Excel(List<JdmCompareReport> reports, string outPath, List<JdmColumnConfig> columnConfig)
    {
        _workbook = new ExcelFile();

        ExportNewPoints(reports.Where(m => m.ChangeType == Program.ChangeTypes.New).ToList(), columnConfig);
        ExportChangedParam(reports.Where(m => m.ChangeType == Program.ChangeTypes.ParamChanged).ToList());
        ExportDeletedPoints(reports.Where(m => m.ChangeType == Program.ChangeTypes.Deleted).ToList());

        _workbook.Save(outPath);
    }

    private void ExportDeletedPoints(List<JdmCompareReport> reportForDeletedPoints)
    {
        var worksheet = _workbook.Worksheets.Add(DELETED_POINTS_SHEET_NAME);

        worksheet.Cells[0, 0].SetValue("Point name");

        for (int i = 0; i < reportForDeletedPoints.Count; i++)
            worksheet.Cells[i + 1, 0].SetValue(reportForDeletedPoints[i].Name);
    }

    private void ExportChangedParam(List<JdmCompareReport> reportsWithChangedParam)
    {
        var worksheet = _workbook.Worksheets.Add(CHANGED_PARAM_SHEET_NAME);

        var grpByParamName = reportsWithChangedParam.GroupBy(m => m.ParamName);

        worksheet.Cells[0, 0].SetValue("Param name");
        worksheet.Cells[0, 1].SetValue("Point name");
        worksheet.Cells[0, 2].SetValue("Old value");
        worksheet.Cells[0, 3].SetValue("New value");

        var index = 0;
        foreach (var grpByName in grpByParamName)
        {
            for (int i = 0; i < grpByName.Count(); i++)
            {
                index++;
                worksheet.Cells[index, 0].SetValue(grpByName.ElementAt(i).ParamName);
                worksheet.Cells[index, 1].SetValue(grpByName.ElementAt(i).Name);
                worksheet.Cells[index, 2].SetValue(grpByName.ElementAt(i).OldValue);
                worksheet.Cells[index, 3].SetValue(grpByName.ElementAt(i).NewValue);
            }
        }
    }

    private void ExportNewPoints(List<JdmCompareReport> newPointsReport, List<JdmColumnConfig> columnConfig)
    {
        var worksheet = _workbook.Worksheets.Add(NEW_POINTS_SHEET_NAME);

        JdmVtaExcelExporter.ExportPoints2ExcelWorkSheet(newPointsReport.Select(m => m.NewPoint).Cast<IJdmComparable>().ToList(), columnConfig, worksheet);
    }
}