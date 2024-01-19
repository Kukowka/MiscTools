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

        public static string VTA_XMLS_PATH = @"e:\PS_Coding\JoiningDataManager\InputData\63_PO455_Boxster_HDBT_VFF_Stern_KW50";

        //public static string VTA_CSVS_PATH = @"e:\PS_Coding\JoiningDataManager\InputData\62_PO992_W23Q73_Bauteildaten_FDS_Verschiebung_Schweller";
        public static string VTA_CSVS_PATH = @"e:\PS_Coding\JoiningDataManager\InputData\63_PO455_Boxster_HDBT_VFF_Stern_KW50";
        //public static string VTA_CSVS_PATH = @"e:\PS_Coding\JoiningDataManager\InputData\63_PO455_Boxster_HDBT_VFF_Stern_KW50";

        public static string VTA_EXPORT_PATH = @"e:\PS_Coding\JoiningDataManager\VTA_Export_20240118.xlsx";
        //public static string VTA_EXPORT_PATH = @"e:\PS_Coding\JoiningDataManager\VTA_ExportTest_20240115.xlsx";

        //public static string VDL_EXCEL_PATH = @"e:\PS_Coding\JoiningDataManager\Verbindungsdatenliste_P992_983_Rev01_.xlsx";
        //public static string VDL_EXCEL_PATH = @"e:\PS_Coding\JoiningDataManager\Verbindungsdatenliste_P992_983_Rev01_Boxer_LL.xlsx";

        //public static string VDL_EXCEL_PATH = @"e:\PS_Coding\JoiningDataManager\Verbindungsdatenliste_P983_IHS_Maßnahmen.xlsx";
        public static string VDL_EXCEL_PATH = @"e:\PS_Coding\JoiningDataManager\Verbindungsdatenliste_P983_IHS_Maßnahmen_Boxter_LL2.xlsx";

        public static string VARIANT_EXCEL_PATH = @"\\at.tmsp.local\Projekte\M1A\21123\13_Eng_Mechanik\15_Bauteile\02_Variantenübersicht\20231107_Zusammenfassung_Fügestufen_TMS_kukowka.xlsx";
        public static string VDL_EXCEL_SHEET_NAME = "Punktliste";
        public static int VDL_START_ROW_INDEX = 2; //zero based index

        public static string RESULT_PATH = @"e:\PS_Coding\JoiningDataManager\PartUpdateChanges_20240119_Boxter2.xlsx";
        public static string PART_UPDATE_TAG = "34\\35";
        //public static string GetChangesReportPath() => RESULT_DIR + "Changes.csv";
        //public static string GetNewPointsReportPath() => RESULT_DIR + "NewPoints.xlsx";

        static void Main(string[] args)
        {
            SpreadsheetInfo.SetLicense(GEMBOX_LIC);

            //var variants = ReadVariants();
            //ReadVtaPointsFromXmls(variants);

            var vdlPoints = ReadVdlPoints(VDL_EXCEL_PATH, VDL_EXCEL_SHEET_NAME, VDL_COLUMN_CONFIG, VDL_START_ROW_INDEX);
            var newVtaPoints = ReadVdlPoints(VTA_EXPORT_PATH, JdmVtaExcelExporter.VTA_EXPORT_SHEET_NAME, VDL_COLUMN_CONFIG, 1);

            if (newVtaPoints.Any(m => m.Name.Equals("992.805.385___-031-H2-001-R")))
            {
            }

            var compareRunner = new JdmCompareRunner();
            var reports = compareRunner.ComparePoints(newVtaPoints, vdlPoints, VDL_COLUMN_CONFIG, PART_COMPARER);

            new JdmChangesExporter().ExportChanges2Excel(reports, RESULT_PATH, VDL_COLUMN_CONFIG);
        }

        public static List<JdmVariantAssembly> ReadVariants()
        {
            var readerVariantExcel = new JdmDataReaderVariantExcel();
            var variants = readerVariantExcel.ExtractVariants(VARIANT_EXCEL_PATH);

            if (!readerVariantExcel.AreReadVariantsOk(variants))
                throw new InvalidDataException();

            return variants;
        }
        private static List<JdmVdlPoint> FilterPointsForVariant(List<JdmVdlPoint> newVtaPoints, string variantName, List<JdmVariantAssembly> variants)
        {
            var boxterVariant = variants.First(m => m.VariantName.Equals(variantName));
            boxterVariant.RemoveUnusedAssemblies();
            var boxterLlPoints = newVtaPoints.Where(m => boxterVariant.IsPointOfVariant(m.Name)).ToList();
            return boxterLlPoints;
        }

        private static List<JdmVdlPoint> ReadVdlPoints(string vdlExcelPath, string sheetName, List<JdmColumnConfig> vdlColumnConfig, int vdlStartRowIndex)
        {
            if (!File.Exists(vdlExcelPath))
                throw new ArgumentOutOfRangeException(nameof(vdlExcelPath), "File does not exists");

            var vdlReader = new JdmDataReaderVdlExcel();
            var vdlPoints = vdlReader.ExtractVdlPoints(vdlExcelPath, sheetName, vdlColumnConfig, vdlStartRowIndex);

            return vdlPoints;
        }

        private static void ReadVtaPointsFromCsvs()
        {
            if (!Directory.Exists(VTA_CSVS_PATH))
                throw new ArgumentOutOfRangeException(nameof(VTA_CSVS_PATH), "Directory does not exists");

            //var readerVariantExcel = new JdmDataReaderVariantExcel();
            //var variants = readerVariantExcel.ExtractVariants(VARIANT_EXCEL_PATH);

            //if (!readerVariantExcel.AreReadVariantsOk(variants))
            //    throw new InvalidDataException();

            var vtaReader = new JdmDataReaderCsv();
            var vtaPoints = vtaReader.ExtractVtaPointsFromCsvs(VTA_CSVS_PATH, FIELD_NAMES, new List<JdmVariantAssembly>());

            //JdmDataReaderCsv.ReadVtaCsv(@"e:\PS_Coding\JoiningDataManager\InputData\63_PO455_Boxster_HDBT_VFF_Stern_KW50\A_ANBAUTEIL_ROHBAU_NACH_LACK\983.805.364.___VER_001.0_20220909.csv", FIELD_NAMES);

            if (!vtaReader.HaveVtaPointsUniqueNames(vtaPoints))
                throw new InvalidCastException();

            new JdmVtaExcelExporter().ExportVta2Excel(VTA_EXPORT_PATH, vtaPoints, JdmConst.VDL_COLUMN_CONFIG);
        }

        private static void ReadVtaPointsFromXmls(List<JdmVariantAssembly> variants)
        {
            if (!Directory.Exists(VTA_XMLS_PATH))
                throw new ArgumentOutOfRangeException(nameof(VTA_XMLS_PATH), "Directory does not exists");

            var xmlReader = new JdmDataReaderXmls();
            var variant = variants.First(m => m.VariantName.Equals(VARIANT_NAME_PO455_LL));
            variant.RemoveUnusedAssemblies();
            var vtaPoints = xmlReader.ExtractVtaPointsFromXmls(VTA_XMLS_PATH, FIELD_NAMES, variant);
            new JdmVtaExcelExporter().ExportVta2Excel(VTA_EXPORT_PATH, vtaPoints, JdmConst.VDL_COLUMN_CONFIG);
        }

        //public enum ChangeTypes
        //{
        //    New,
        //    Deleted,
        //    ParamChanged,
        //    XyzChanged,
        //    AssignedPartChanged,
        //    PartPropertyChanged,
        //}
    }
}
