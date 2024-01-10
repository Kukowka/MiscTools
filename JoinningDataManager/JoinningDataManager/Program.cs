using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static JoinningDataManager.JdmConst;

namespace JoinningDataManager
{

    public class Program
    {
        public static string VTA_XMLS_PATH = @"\\fs125\eM_Planner_Data\SystemRoot\TMS_PS_add-ins\01_Organisation\26_Future_Joining_Concept\ExampleData\01_Porsche\01_InputData\47_PO455_Boxster_Zwischendatenstand_Crash_Probleme_Aufbau";
        public static string VTA_CSVS_PATH = @"e:\PS_Coding\JoiningDataManager\InputData\62_PO992_W23Q73_Bauteildaten_FDS_Verschiebung_Schweller";

        public static string VDL_EXCEL_PATH = @"e:\PS_Coding\JoiningDataManager\Verbindungsdatenliste_P992_983_Rev01_.xlsx";
        public static string VDL_EXCEL_SHEET_NAME = "Punktliste";
        public static int VDL_START_ROW_INDEX = 2; //zero based index

        public static string RESULT_DIR = @"e:\PS_Coding\JoiningDataManager";
        public static string PART_UPDATE_TAG = "34\\35";
        public static string GetChangesReportPath() => RESULT_DIR + "Changes.csv";
        public static string GetNewPointsReportPath() => RESULT_DIR + "NewPoints.xlsx";

        static void Main(string[] args)
        {
            if (!Directory.Exists(VTA_CSVS_PATH))
                throw new ArgumentOutOfRangeException(nameof(VTA_CSVS_PATH), "Directory does not exists");

            if (!File.Exists(VDL_EXCEL_PATH))
                throw new ArgumentOutOfRangeException(nameof(VDL_EXCEL_PATH), "File does not exists");

            var dataReader = new JdmDataReaderExcel();
            var vtaPoints = dataReader.ExtractVtaPointsFromCsvs(VTA_CSVS_PATH, FIELD_NAMES);
            var vdlPoints = dataReader.ExtractVdlPoints(VDL_EXCEL_PATH, VDL_EXCEL_SHEET_NAME, VDL_COLUMN_CONFIG, VDL_START_ROW_INDEX);

            if (!HaveVtaPointsUniqueNames(vtaPoints))
                throw new InvalidCastException();

            //if (!HaveVdlPointsUniqueNames(vdlPoints))
            //    throw new InvalidCastException();

            var compareRunner = new JdmCompareRunner(vtaPoints, vdlPoints, VDL_COLUMN_CONFIG);
            //var reports = compareRunner.ComparePoints();
            //compareRunner.GetRedundantVariants(vdlPoints, VARIANT_NAMES);
            //ExportOutputData(reports, vtaPoints);
        }

        private static bool HaveVdlPointsUniqueNames(List<JdmVdlPoint> vdlPoints)
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

        private static bool HaveVtaPointsUniqueNames(List<JdmRawVtaPoint> vtaPoints)
        {
            var grp = vtaPoints.GroupBy(m => m.Name);

            if (grp.Any(m => m.Count() > 1))
            {
                var notUniqueNames = grp.Where(m => m.Count() > 1).Select(m => m.Key).ToList();

                return false;
            }

            return true;
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
