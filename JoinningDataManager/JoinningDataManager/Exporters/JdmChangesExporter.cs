using GemBox.Spreadsheet;
using JoinningDataManager.ChangeReport;
using System.Collections.Generic;
using System.Linq;

namespace JoinningDataManager;

public class JdmChangesExporter
{
    public const string NEW_POINTS_SHEET_NAME = "New points";
    public const string CHANGED_PARAM_SHEET_NAME = "Changed params";
    public const string DELETED_POINTS_SHEET_NAME = "Deleted points";
    public const string DIFFERENT_PART_ASSIGNED_SHEET_NAME = "Different part assigned";
    public const string SWAPPED_PARTS_SHEET_NAME = "Swapped parts";
    public const string CHANGED_PART_PROP_SHEET_NAME = "Changed part propertie";


    private ExcelFile _workbook;

    public void ExportChanges2Excel(List<IJdmChangeReport> reports, string outPath, List<JdmColumnConfig> columnConfig)
    {
        _workbook = new ExcelFile();

        ExportNewPoints(reports.OfType<JdmChangeReportNew>().ToList(), columnConfig);
        ExportChangedParam(reports.OfType<JdmChangeReportParamChanged>().ToList());
        ExportDeletedPoints(reports.OfType<JdmChangeReportDeleted>().ToList());
        ExportPointsWithDifferentPartAssigned(reports.OfType<JdmChangeReportDiffPart>().ToList());
        ExportPointsWithPartParamChanged(reports.OfType<JdmChangeReportDiffPartPropertie>().ToList());
        ExportPointsWithSwappedParts(reports.OfType<JdmChangeReportPartSwapped>().ToList());

        AutoFitColumnsForAllSheets();

        _workbook.Save(outPath);
    }

    private void AutoFitColumnsForAllSheets()
    {
        foreach (var worksheet in _workbook.Worksheets)
            AutoFitColumns(worksheet);
    }


    private void ExportPointsWithSwappedParts(List<JdmChangeReportPartSwapped> reports)
    {
        var worksheet = _workbook.Worksheets.Add(SWAPPED_PARTS_SHEET_NAME);

        if (reports.Count == 0)
            return;

        worksheet.Cells[0, 0].SetValue("Point name");
        worksheet.Cells[0, 1].SetValue("Old part index");
        worksheet.Cells[0, 2].SetValue("New parts list");
        worksheet.Cells[0, 3].SetValue("Old parts list");


        for (var index = 0; index < reports.Count; index++)
        {
            var report = reports[index];

            worksheet.Cells[index + 1, 0].SetValue(report.PointName);
            worksheet.Cells[index + 1, 1].SetValue(report.OldPartIndex);
            worksheet.Cells[index + 1, 2].SetValue(string.Join("    ", report.NewPartNames));
            worksheet.Cells[index + 1, 3].SetValue(string.Join("    ", report.OldPartNames));
        }
    }

    private void ExportPointsWithPartParamChanged(List<JdmChangeReportDiffPartPropertie> reports)
    {
        var worksheet = _workbook.Worksheets.Add(CHANGED_PART_PROP_SHEET_NAME);

        if (reports.Count == 0)
            return;

        worksheet.Cells[0, 0].SetValue("Part name");
        worksheet.Cells[0, 1].SetValue("Param name");
        worksheet.Cells[0, 2].SetValue("Old Value");
        worksheet.Cells[0, 3].SetValue("New Value");
        worksheet.Cells[0, 4].SetValue("Affected Points");

        for (var index = 0; index < reports.Count; index++)
        {
            var report = reports[index];

            worksheet.Cells[index + 1, 0].SetValue(report.PartName);
            worksheet.Cells[index + 1, 1].SetValue(report.PropertieName);
            worksheet.Cells[index + 1, 2].SetValue(report.OldValue);
            worksheet.Cells[index + 1, 3].SetValue(report.NewValue);
            worksheet.Cells[index + 1, 4].SetValue(string.Join("    ", report.AffectedPoints));
        }
    }

    private void ExportPointsWithDifferentPartAssigned(List<JdmChangeReportDiffPart> reports)
    {
        var worksheet = _workbook.Worksheets.Add(DIFFERENT_PART_ASSIGNED_SHEET_NAME);

        if (reports.Count == 0)
            return;

        worksheet.Cells[0, 0].SetValue("Old part name");
        worksheet.Cells[0, 1].SetValue("New part name");
        worksheet.Cells[0, 2].SetValue("Affected points");

        var index = 1;
        foreach (var report in reports)
        {
            worksheet.Cells[index, 0].SetValue(report.OldPartName);
            worksheet.Cells[index, 1].SetValue(report.NewPartName);

            foreach (var affectedPoint in report.AffectedPointNames)
            {
                worksheet.Cells[index, 2].SetValue(affectedPoint);
                index++;
            }

            index++;
        }
    }

    private void ExportDeletedPoints(List<JdmChangeReportDeleted> reportForDeletedPoints)
    {
        var worksheet = _workbook.Worksheets.Add(DELETED_POINTS_SHEET_NAME);

        if (reportForDeletedPoints.Count == 0)
            return;

        worksheet.Cells[0, 0].SetValue("Point name");

        for (int i = 0; i < reportForDeletedPoints.Count; i++)
            worksheet.Cells[i + 1, 0].SetValue(reportForDeletedPoints[i].PointName);
    }

    private void ExportChangedParam(List<JdmChangeReportParamChanged> reportsWithChangedParam)
    {
        var worksheet = _workbook.Worksheets.Add(CHANGED_PARAM_SHEET_NAME);

        if (reportsWithChangedParam.Count == 0)
            return;

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

    private void ExportNewPoints(List<JdmChangeReportNew> newPointsReport, List<JdmColumnConfig> columnConfig)
    {
        var worksheet = _workbook.Worksheets.Add(NEW_POINTS_SHEET_NAME);

        if (newPointsReport.Count == 0)
            return;

        JdmVtaExcelExporter.ExportPoints2ExcelWorkSheet(newPointsReport.Select(m => m.NewPoint).Cast<IJdmComparable>().ToList(), columnConfig, worksheet);
    }

    protected void AutoFitColumns(ExcelWorksheet excelSheet)
    {
        var columnCount = excelSheet.CalculateMaxUsedColumns();
        for (var i = 0; i < columnCount; i++)
            excelSheet.Columns[i].AutoFit(1, excelSheet.Rows[0], excelSheet.Rows[excelSheet.Rows.Count - 1]);
    }
}