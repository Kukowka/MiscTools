using GemBox.Spreadsheet;
using JoinningDataManager.ChangeReport;
using JoinningDataManagerTests;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using static JoinningDataManager.JdmConst;

namespace JoinningDataManager.Tests
{
    [TestFixture]
    public class JdmCompareRunnerTests
    {
        public static string VDL_EXCEL_PATH = @"e:\PS_Coding\JoiningDataManager\Verbindungsdatenliste_P983_IHS_Maßnahmen_Boxter_LL2.xlsx";
        public static string VDL_EXCEL_SHEET_NAME = "Punktliste";
        public static int VDL_START_ROW_INDEX = 2; //zero based index
        public static string VTA_EXPORT_PATH = @"e:\PS_Coding\JoiningDataManager\VTA_Export_20240117.xlsx";

        [OneTimeSetUp]
        public void SetUp()
        {
            SpreadsheetInfo.SetLicense(Const.GEMBOX_LIC);
        }

        public static JdmChangeReportDiffPartPropertie DiffPartProp1 = new JdmChangeReportDiffPartPropertie("Material2", "1.1.1.84.111 # VW 50065-CR300LA #", "2.1.2.30.002 # VW 50067-AL2027 # O/H111", "983.813.777.___VTA", new List<string>()
        {
            "983.800.702___-020-D2-001-L",
            "983.800.702___-020-D2-002-L",
            "983.800.702___-020-D2-003-L",
        });


        public static JdmChangeReportDiffPartPropertie DiffPartProp2 = new JdmChangeReportDiffPartPropertie("Material2", "1.1.1.84.111 # VW 50065-CR300LA #", "2.1.2.30.002 # VW 50067-AL2027 # O/H111", "983.813.774.___VTA", new List<string>()
        {
            "983.800.702___-020-D2-001-R",
            "983.800.702___-020-D2-002-R",
            "983.800.702___-020-D2-003-R",
        });

        public static JdmChangeReportDiffPartPropertie DiffPartProp3 = new JdmChangeReportDiffPartPropertie("Material2", "1.1.1.84.300 # VW 50066-CR1500-MB-DS#", "2.1.2.30.002 # VW 50067-AL2027 # O/H111", "983.813.774.___VTA", new List<string>()
        {
            "983.800.702___-053-B2-001-R",
            "983.800.702___-053-J9-002-R",
        });


        public static JdmChangeReportDiffPartPropertie DiffPartProp4 = new JdmChangeReportDiffPartPropertie("Material3", "1.1.1.84.300 # VW 50066-CR1500-MB-DS#", "2.1.2.30.002 # VW 50067-AL2027 # O/H111", "983.813.777.___VTA", new List<string>()
        {
            "983.800.702___-053-C8-001-L",
            "983.800.702___-053-J3-001-L",
        });

        public static JdmChangeReportDiffPart DiffPart1 = new JdmChangeReportDiffPart("983.813.773", "983.813.777", new List<string>()
        {
            "983.800.701___-025-E1-002-L",
            "983.800.702___-025-J6-001-L",
            "983.800.702___-053-B2-001-L",
            "983.800.702___-053-J9-002-L",
        });

        public static JdmChangeReportDiffPart DiffPart2 = new JdmChangeReportDiffPart("983.813.778", "983.813.774", new List<string>()
        {
            "983.800.702___-053-C8-001-R",
            "983.800.702___-053-J3-001-R",
        });

        public static JdmChangeReportDiffPart DiffPart3 = new JdmChangeReportDiffPart("983.809.209", "983.813.209", new List<string>()
        {
            "983.800.702___-015-K2-001-L",
        });

        public static JdmChangeReportDiffPart DiffPart4 = new JdmChangeReportDiffPart("983.809.210", "983.813.210", new List<string>()
        {
            "983.800.702___-015-K2-001-R",
        });

        public static JdmChangeReportParamChanged ParamChanged1 = new JdmChangeReportParamChanged("983.800.702___-053-B2-001-L", "Klebstoff1", null, "Ja");
        public static JdmChangeReportParamChanged ParamChanged2 = new JdmChangeReportParamChanged("983.800.702___-053-B2-001-L", "Klebstoff2", null, "Ja");
        public static JdmChangeReportParamChanged ParamChanged3 = new JdmChangeReportParamChanged("983.800.702___-053-B2-001-R", "Klebstoff1", null, "Ja");
        public static JdmChangeReportParamChanged ParamChanged4 = new JdmChangeReportParamChanged("983.800.702___-053-B2-001-R", "Klebstoff2", null, "Ja");
        public static JdmChangeReportParamChanged ParamChanged5 = new JdmChangeReportParamChanged("983.800.702___-053-B2-001-L", "RES-Element", null, "noch abzustimmen");
        public static JdmChangeReportParamChanged ParamChanged6 = new JdmChangeReportParamChanged("983.800.702___-053-B2-001-R", "RES-Element", null, "noch abzustimmen");

        public static JdmChangeReportParamChanged ParamChanged7 = new JdmChangeReportParamChanged("983.800.702___-053-C8-001-L", "Klebstoff1", null, "Ja");
        public static JdmChangeReportParamChanged ParamChanged8 = new JdmChangeReportParamChanged("983.800.702___-053-C8-001-L", "Klebstoff2", null, "Ja");
        public static JdmChangeReportParamChanged ParamChanged9 = new JdmChangeReportParamChanged("983.800.702___-053-C8-001-R", "Klebstoff1", null, "Ja");
        public static JdmChangeReportParamChanged ParamChanged10 = new JdmChangeReportParamChanged("983.800.702___-053-C8-001-R", "Klebstoff2", null, "Ja");
        public static JdmChangeReportParamChanged ParamChanged11 = new JdmChangeReportParamChanged("983.800.702___-053-C8-001-L", "RES-Element", null, "noch abzustimmen");
        public static JdmChangeReportParamChanged ParamChanged12 = new JdmChangeReportParamChanged("983.800.702___-053-C8-001-R", "RES-Element", null, "noch abzustimmen");

        public static JdmChangeReportParamChanged ParamChanged13 = new JdmChangeReportParamChanged("983.800.702___-053-J3-001-L", "Klebstoff1", null, "Ja");
        public static JdmChangeReportParamChanged ParamChanged14 = new JdmChangeReportParamChanged("983.800.702___-053-J3-001-L", "Klebstoff2", null, "Ja");
        public static JdmChangeReportParamChanged ParamChanged15 = new JdmChangeReportParamChanged("983.800.702___-053-J3-001-R", "Klebstoff1", null, "Ja");
        public static JdmChangeReportParamChanged ParamChanged16 = new JdmChangeReportParamChanged("983.800.702___-053-J3-001-R", "Klebstoff2", null, "Ja");
        public static JdmChangeReportParamChanged ParamChanged17 = new JdmChangeReportParamChanged("983.800.702___-053-J3-001-L", "RES-Element", null, "noch abzustimmen");
        public static JdmChangeReportParamChanged ParamChanged18 = new JdmChangeReportParamChanged("983.800.702___-053-J3-001-R", "RES-Element", null, "noch abzustimmen");

        public static JdmChangeReportParamChanged ParamChanged19 = new JdmChangeReportParamChanged("983.800.702___-053-J9-002-L", "Klebstoff1", null, "Ja");
        public static JdmChangeReportParamChanged ParamChanged20 = new JdmChangeReportParamChanged("983.800.702___-053-J9-002-L", "Klebstoff2", null, "kein_Klebstoff");
        public static JdmChangeReportParamChanged ParamChanged21 = new JdmChangeReportParamChanged("983.800.702___-053-J9-002-R", "Klebstoff1", null, "Ja");
        public static JdmChangeReportParamChanged ParamChanged22 = new JdmChangeReportParamChanged("983.800.702___-053-J9-002-R", "Klebstoff2", null, "kein_Klebstoff");
        public static JdmChangeReportParamChanged ParamChanged23 = new JdmChangeReportParamChanged("983.800.702___-053-J9-002-L", "RES-Element", null, "noch abzustimmen");
        public static JdmChangeReportParamChanged ParamChanged24 = new JdmChangeReportParamChanged("983.800.702___-053-J9-002-R", "RES-Element", null, "noch abzustimmen");

        public static JdmChangeReportParamChanged ParamChanged25 = new JdmChangeReportParamChanged("983.800.702___-015-K2-001-L", "FDS-Durchmesser", "4,00 mm", "5,00 mm");
        public static JdmChangeReportParamChanged ParamChanged26 = new JdmChangeReportParamChanged("983.800.702___-015-K2-001-R", "FDS-Durchmesser", "4,00 mm", "5,00 mm");
        public static JdmChangeReportParamChanged ParamChanged27 = new JdmChangeReportParamChanged("983.800.702___-015-E2-002-L", "FDS-Durchmesser", "4,00 mm", "5,00 mm");
        public static JdmChangeReportParamChanged ParamChanged28 = new JdmChangeReportParamChanged("983.800.702___-015-E2-002-R", "FDS-Durchmesser", "4,00 mm", "5,00 mm");
        public static JdmChangeReportParamChanged ParamChanged29 = new JdmChangeReportParamChanged("983.803.001___-025-G8-003-L", "Nahtlaenge", "43,74 mm", "31,50 mm");
        public static JdmChangeReportParamChanged ParamChanged30 = new JdmChangeReportParamChanged("983.803.001___-025-G8-003-R", "Nahtlaenge", "43,74 mm", "31,50 mm");


        public static JdmChangeReportParamChanged ParamChanged31 = new JdmChangeReportParamChanged("983.803.001___-025-N6-003-L", "Nahtlaenge", "58,93 mm", "46,00 mm");
        public static JdmChangeReportParamChanged ParamChanged32 = new JdmChangeReportParamChanged("983.803.001___-025-N6-003-R", "Nahtlaenge", "58,93 mm", "46,00 mm");
        public static JdmChangeReportParamChanged ParamChanged33 = new JdmChangeReportParamChanged("983.800.702___-025-B7-001-L", "Nahtlaenge", "158,9 mm", "15,89 mm");
        public static JdmChangeReportParamChanged ParamChanged34 = new JdmChangeReportParamChanged("983.800.702___-025-B7-001-R", "Nahtlaenge", "158,9 mm", "15,89 mm");


        public static JdmChangeReportParamChanged ParamChanged35 = new JdmChangeReportParamChanged("983.800.702___-025-F9-001-L", "Nahtlaenge", "173,99 mm", "124,99 mm");
        public static JdmChangeReportParamChanged ParamChanged36 = new JdmChangeReportParamChanged("983.800.702___-025-F9-001-R", "Nahtlaenge", "173,99 mm", "124,99 mm");

        public static JdmChangeReportParamChanged ParamChanged37 = new JdmChangeReportParamChanged("983.800.701___-051-A8-001-R", "Clinchdurchmesser (Aussen)", "6,00 mm", "8,00 mm");
        public static JdmChangeReportParamChanged ParamChanged38 = new JdmChangeReportParamChanged("983.800.701___-051-A8-003-L", "Clinchdurchmesser (Aussen)", "6,00 mm", "8,00 mm");
        public static JdmChangeReportParamChanged ParamChanged39 = new JdmChangeReportParamChanged("983.800.701___-051-A8-003-R", "Clinchdurchmesser (Aussen)", "6,00 mm", "8,00 mm");
        public static JdmChangeReportParamChanged ParamChanged40 = new JdmChangeReportParamChanged("983.800.701___-051-F5-001-L", "Clinchdurchmesser (Aussen)", "6,00 mm", "8,00 mm");
        public static JdmChangeReportParamChanged ParamChanged41 = new JdmChangeReportParamChanged("983.800.701___-051-F5-001-R", "Clinchdurchmesser (Aussen)", "6,00 mm", "8,00 mm");
        public static JdmChangeReportParamChanged ParamChanged42 = new JdmChangeReportParamChanged("983.800.701___-051-F5-002-L", "Clinchdurchmesser (Aussen)", "6,00 mm", "8,00 mm");
        public static JdmChangeReportParamChanged ParamChanged43 = new JdmChangeReportParamChanged("983.800.701___-051-F5-002-R", "Clinchdurchmesser (Aussen)", "6,00 mm", "8,00 mm");
        public static JdmChangeReportParamChanged ParamChanged44 = new JdmChangeReportParamChanged("983.800.701___-051-H3-001-L", "Clinchdurchmesser (Aussen)", "6,00 mm", "8,00 mm");
        public static JdmChangeReportParamChanged ParamChanged45 = new JdmChangeReportParamChanged("983.800.701___-051-H3-001-R", "Clinchdurchmesser (Aussen)", "6,00 mm", "8,00 mm");


        [Test]
        public void JdmCompareRunnerTest()
        {
            var vdlPoints = ReadVdlPoints(VDL_EXCEL_PATH, VDL_EXCEL_SHEET_NAME, VDL_COLUMN_CONFIG, VDL_START_ROW_INDEX);
            var newVtaPoints = ReadVdlPoints(VTA_EXPORT_PATH, JdmVtaExcelExporter.VTA_EXPORT_SHEET_NAME, VDL_COLUMN_CONFIG, 1);

            var compareRunner = new JdmCompareRunner();
            var reports = compareRunner.ComparePoints(newVtaPoints, vdlPoints, VDL_COLUMN_CONFIG, PART_COMPARER);

            Assert.Contains(DiffPartProp1, reports);
            Assert.Contains(DiffPartProp2, reports);
            Assert.Contains(DiffPartProp3, reports);
            Assert.Contains(DiffPartProp4, reports);

            Assert.Contains(DiffPart1, reports);
            Assert.Contains(DiffPart2, reports);
            Assert.Contains(DiffPart3, reports);
            Assert.Contains(DiffPart4, reports);

            Assert.Contains(ParamChanged1, reports);
            Assert.Contains(ParamChanged2, reports);
            Assert.Contains(ParamChanged3, reports);
            Assert.Contains(ParamChanged4, reports);
            Assert.Contains(ParamChanged5, reports);
            Assert.Contains(ParamChanged6, reports);

            Assert.Contains(ParamChanged7, reports);
            Assert.Contains(ParamChanged8, reports);
            Assert.Contains(ParamChanged9, reports);
            Assert.Contains(ParamChanged10, reports);
            Assert.Contains(ParamChanged11, reports);
            Assert.Contains(ParamChanged12, reports);

            Assert.Contains(ParamChanged13, reports);
            Assert.Contains(ParamChanged14, reports);
            Assert.Contains(ParamChanged15, reports);
            Assert.Contains(ParamChanged16, reports);
            Assert.Contains(ParamChanged17, reports);
            Assert.Contains(ParamChanged18, reports);

            Assert.Contains(ParamChanged19, reports);
            Assert.Contains(ParamChanged20, reports);
            Assert.Contains(ParamChanged21, reports);
            Assert.Contains(ParamChanged22, reports);
            Assert.Contains(ParamChanged23, reports);
            Assert.Contains(ParamChanged24, reports);

            Assert.Contains(ParamChanged25, reports);
            Assert.Contains(ParamChanged26, reports);
            Assert.Contains(ParamChanged27, reports);
            Assert.Contains(ParamChanged28, reports);
            Assert.Contains(ParamChanged29, reports);
            Assert.Contains(ParamChanged30, reports);
            Assert.Contains(ParamChanged31, reports);
            Assert.Contains(ParamChanged32, reports);
            Assert.Contains(ParamChanged33, reports);
            Assert.Contains(ParamChanged34, reports);
            Assert.Contains(ParamChanged35, reports);
            Assert.Contains(ParamChanged36, reports);
            Assert.Contains(ParamChanged37, reports);
            Assert.Contains(ParamChanged38, reports);
            Assert.Contains(ParamChanged39, reports);
            Assert.Contains(ParamChanged40, reports);
            Assert.Contains(ParamChanged41, reports);
            Assert.Contains(ParamChanged42, reports);
            Assert.Contains(ParamChanged43, reports);
            Assert.Contains(ParamChanged44, reports);
            Assert.Contains(ParamChanged45, reports);

        }

        private static List<JdmVdlPoint> ReadVdlPoints(string vdlExcelPath, string sheetName, List<JdmColumnConfig> vdlColumnConfig, int vdlStartRowIndex)
        {
            if (!File.Exists(vdlExcelPath))
                throw new ArgumentOutOfRangeException(nameof(vdlExcelPath), "File does not exists");

            var vdlReader = new JdmDataReaderVdlExcel();
            var vdlPoints = vdlReader.ExtractVdlPoints(vdlExcelPath, sheetName, vdlColumnConfig, vdlStartRowIndex);

            return vdlPoints;
        }
    }
}