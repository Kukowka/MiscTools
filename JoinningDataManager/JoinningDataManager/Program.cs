using GemBox.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static JoinningDataManager.JdmConst;

namespace JoinningDataManager
{

    public class Program
    {
        public static string GEMBOX_LIC = "SN-2023May23-OQ1MibKojfVvZoPCvWlB0q7iakOKFM4c0IZiRe2u605diDgqM17xjS0uBeCZXtREEREWQ71d6K3zkJXK1QXD/K5Z4AA==A";

        public static string VTA_XMLS_PATH = @"\\fs125\eM_Planner_Data\SystemRoot\TMS_PS_add-ins\01_Organisation\26_Future_Joining_Concept\ExampleData\01_Porsche\01_InputData\47_PO455_Boxster_Zwischendatenstand_Crash_Probleme_Aufbau";
        public static string VTA_CSVS_PATH = @"e:\PS_Coding\JoiningDataManager\InputData\62_PO992_W23Q73_Bauteildaten_FDS_Verschiebung_Schweller";
        //public static string VTA_CSVS_PATH = @"e:\PS_Coding\JoiningDataManager\InputData\63_PO455_Boxster_HDBT_VFF_Stern_KW50";

        public static string VTA_EXPORT_PATH = @"e:\PS_Coding\JoiningDataManager\VTA_Export_20240110.xlsx";


        public static string VDL_EXCEL_PATH = @"e:\PS_Coding\JoiningDataManager\Verbindungsdatenliste_P992_983_Rev01_.xlsx";
        public static string VARIANT_EXCEL_PATH = @"\\at.tmsp.local\Projekte\M1A\21123\13_Eng_Mechanik\15_Bauteile\02_Variantenübersicht\20231107_Zusammenfassung_Fügestufen_TMS_kukowka.xlsx";
        public static string VDL_EXCEL_SHEET_NAME = "Punktliste";
        public static int VDL_START_ROW_INDEX = 2; //zero based index

        public static string RESULT_DIR = @"e:\PS_Coding\JoiningDataManager";
        public static string PART_UPDATE_TAG = "34\\35";
        public static string GetChangesReportPath() => RESULT_DIR + "Changes.csv";
        public static string GetNewPointsReportPath() => RESULT_DIR + "NewPoints.xlsx";

        static void Main(string[] args)
        {
            SpreadsheetInfo.SetLicense(GEMBOX_LIC);

            if (!Directory.Exists(VTA_CSVS_PATH))
                throw new ArgumentOutOfRangeException(nameof(VTA_CSVS_PATH), "Directory does not exists");

            if (!File.Exists(VDL_EXCEL_PATH))
                throw new ArgumentOutOfRangeException(nameof(VDL_EXCEL_PATH), "File does not exists");

            var readerVariantExcel = new JdmDataReaderVariantExcel();
            var variants = readerVariantExcel.ExtractVariants(VARIANT_EXCEL_PATH);

            if (!readerVariantExcel.AreReadVariantsOk(variants))
                throw new InvalidDataException();

            var vtaReader = new JdmDataReaderCsv();
            var vtaPoints = vtaReader.ExtractVtaPointsFromCsvs(VTA_CSVS_PATH, FIELD_NAMES, variants);
            var vdlReader = new JdmDataReaderVdlExcel();
            var vdlPoints = vdlReader.ExtractVdlPoints(VDL_EXCEL_PATH, VDL_EXCEL_SHEET_NAME, VDL_COLUMN_CONFIG, VDL_START_ROW_INDEX);


            if (!vtaReader.HaveVtaPointsUniqueNames(vtaPoints))
                throw new InvalidCastException();

            //if (!vdlReader.HaveVdlPointsUniqueNames(vdlPoints))
            //    throw new InvalidCastException();

            new JdmVtaExcelExporter().ExportVta2Excel(VTA_EXPORT_PATH, vtaPoints, JdmConst.VDL_COLUMN_CONFIG);

            //var compareRunner = new JdmCompareRunner(vtaPoints, vdlPoints, VDL_COLUMN_CONFIG);
            //var reports = compareRunner.ComparePoints();
            //compareRunner.GetRedundantVariants(vdlPoints, VARIANT_NAMES);
            //ExportOutputData(reports, vtaPoints);
        }


        private static void ExportOutputData(List<JdmCompareReport> reports, List<JdmRawVtaPoint> vtaPoints)
        {
            var reportsFormatter = new JdmReportFormatter();
            var withTypeOverallReport = reportsFormatter.GetReportsGroupedByType(reports);
            File.WriteAllText(GetChangesReportPath(), withTypeOverallReport);


            var newPointReports = reports.Where(m => m.ChangeType == ChangeTypes.New);
            var newVtaPoints = vtaPoints.Where(m => newPointReports.Any(n => n.Name.Equals(m.Name))).ToList();
            new JdmNewPointsExporter().ExportNewPoints2Excel(GetNewPointsReportPath(), newVtaPoints, JdmConst.VDL_COLUMN_CONFIG);
        }

        public enum ChangeTypes
        {
            New,
            Deleted,
            ParamChanged,
            XyzChanged,
        }
    }
}
