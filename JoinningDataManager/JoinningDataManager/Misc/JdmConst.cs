using JoinningDataManager.Comparers;
using System.Collections.Generic;

namespace JoinningDataManager
{


    public class JdmConst
    {
        public const string FIELD_NAME_BAUTEIL1 = "Bauteil1";
        public const string FIELD_NAME_DICKE1 = "Dicke1";
        public const string FIELD_NAME_MATERIAL1 = "Material1";
        public const string EXPORT_FIELD_NAME_BAUTEIL1 = "Bauteil 1";
        public const string EXPORT_FIELD_NAME_DICKE1 = "Dicke 1";
        public const string EXPORT_FIELD_NAME_MATERIAL1 = "Material 1";

        public const string FIELD_NAME_OBERFLÄCHE1 = "Oberfläche1";
        public const string FIELD_NAME_STRECKGRENZE1 = "Streckgrenze1";
        public const string FIELD_NAME_BAUTEIL2 = "Bauteil2";
        public const string FIELD_NAME_DICKE2 = "Dicke2";
        public const string FIELD_NAME_MATERIAL2 = "Material2";
        public const string EXPORT_FIELD_NAME_BAUTEIL2 = "Bauteil 2";
        public const string EXPORT_FIELD_NAME_DICKE2 = "Dicke 2";
        public const string EXPORT_FIELD_NAME_MATERIAL2 = "Material 2";

        public const string FIELD_NAME_OBERFLAECHE2 = "Oberfläche2";
        public const string FIELD_NAME_STRECKGRENZE2 = "Streckgrenze2";
        public const string FIELD_NAME_BAUTEIL3 = "Bauteil3";
        public const string FIELD_NAME_DICKE3 = "Dicke3";
        public const string FIELD_NAME_MATERIAL3 = "Material3";
        public const string EXPORT_FIELD_NAME_BAUTEIL3 = "Bauteil 3";
        public const string EXPORT_FIELD_NAME_DICKE3 = "Dicke 3";
        public const string EXPORT_FIELD_NAME_MATERIAL3 = "Material 3";
        public const string FIELD_NAME_OBERFLAECHE3 = "Oberfläche3";
        public const string FIELD_NAME_STRECKGRENZE3 = "Streckgrenze3";
        public const string FIELD_NAME_BAUTEIL4 = "Bauteil4";
        public const string FIELD_NAME_DICKE4 = "Dicke4";
        public const string FIELD_NAME_MATERIAL4 = "Material4";
        public const string EXPORT_FIELD_NAME_BAUTEIL4 = "Bauteil 4";
        public const string EXPORT_FIELD_NAME_DICKE4 = "Dicke 4";
        public const string EXPORT_FIELD_NAME_MATERIAL4 = "Material 4";
        public const string FIELD_NAME_OBERFLAECHE4 = "Oberfläche4";
        public const string FIELD_NAME_ELEMENT = "Element";

        public const string FIELD_NAME_INFO_WERKSTOFF1 = "Info_Werkstoff1";
        public const string FIELD_NAME_INFO_WERKSTOFF2 = "Info_Werkstoff2";
        public const string FIELD_NAME_INFO_WERKSTOFF3 = "Info_Werkstoff3";
        public const string FIELD_NAME_INFO_WERKSTOFF4 = "Info_Werkstoff4";

        public const string FIELD_NAME_PROCESS = "Process";
        public const string EXPORT_FIELD_NAME_PROCESS = "Prozess";

        public const string FIELD_NAME_X = "X";
        public const string FIELD_NAME_Y = "Y";
        public const string FIELD_NAME_Z = "Z";

        public const string FIELD_NAME_START_X = "Start-X";
        public const string FIELD_NAME_START_Y = "Start-Y";
        public const string FIELD_NAME_START_Z = "Start-Z";

        public const string FIELD_NAME_END_X = "End-X";
        public const string FIELD_NAME_END_Y = "End-Y";
        public const string FIELD_NAME_END_Z = "End-Z";

        public const string FIELD_NAME_LINSENDURCHMESSER = "Linsendurchmesser";
        public const string EXPORT_FIELD_NAME_LINSENDURCHMESSER = "Linsen-durchmesser";
        public const string FIELD_NAME_LINSENDURCHMESSER2 = "Linsendurchmesser2";
        public const string EXPORT_FIELD_NAME_LINSENDURCHMESSER2 = "Linsen-durchmesser2";
        public const string FIELD_NAME_NIETDURCHMESSER = "NietDurchmesser";
        public const string EXPORT_FIELD_NAME_NIETDURCHMESSER = "Niet-durchmesser";

        public const string FIELD_NAME_FDSDURCHMESSER = "FDS-Durchmesser";
        public const string FIELD_NAME_FDSLAENGE = "FDS-Laenge";
        public const string FIELD_NAME_STUMPFNAHTDICKE = "Stumpfnahtdicke_(s)";
        public const string FIELD_NAME_KEHLNAHTDICKE = "Kehlnahtdicke_(a)";
        public const string FIELD_NAME_NAHTLAENGE = "Nahtlaenge";
        public const string FIELD_NAME_KLEBSTOFF1 = "Klebstoff1";
        public const string FIELD_NAME_KLEBSTOFF2 = "Klebstoff2";
        public const string FIELD_NAME_RESELEMENT = "RES-Element";
        public const string FIELD_NAME_CLINCHDURCHMESSER = "Clinchdurchmesser (Aussen)";
        public const string FIELD_NAME_BOLZENTYP = "Bolzentyp";
        public const string FIELD_NAME_BEMERKUNG = "Bemerkung";
        public const string FIELD_NAME_NAME = "Name";
        public const string EXPORT_FIELD_NAME_NAME = "Punktname";

        public const string VARIANT_NAME_PO684_LL = "PO684_LL";
        public const string VARIANT_NAME_PO684_RL = "PO684_RL";
        public const string VARIANT_NAME_PO685_LL = "PO685_LL";
        public const string VARIANT_NAME_PO685_RL = "PO685_RL";
        public const string VARIANT_NAME_PO684_5_LL = "PO684_5_LL";
        public const string VARIANT_NAME_PO684_5_RL = "PO684_5_RL";
        public const string VARIANT_NAME_PO685_5_LL = "PO685_5_LL";
        public const string VARIANT_NAME_PO685_5_RL = "PO685_5_RL";
        public const string VARIANT_NAME_PO685_3_LL = "PO685_3_LL";
        public const string VARIANT_NAME_PO685_3_RL = "PO685_3_RL";
        public const string VARIANT_NAME_PO684_4_LL = "PO684_4_LL";
        public const string VARIANT_NAME_PO684_4_RL = "PO684_4_RL";
        public const string VARIANT_NAME_PO684_4_LL_CS = "PO684_4_LL_CS";
        public const string VARIANT_NAME_PO684_4_RL_CS = "PO684_4_RL_CS";

        public const string VARIANT_NAME_PO684_5_LL_GT3_RS = "PO684_5_LL_GT3_RS";
        public const string VARIANT_NAME_PO684_5_RL_GT3_RS = "PO684_5_RL_GT3_RS";
        public const string VARIANT_NAME_PO684_5_LL_GT3_RS_CS = "PO684_5_LL_GT3_RS_CS";
        public const string VARIANT_NAME_PO684_5_RL_GT3_RS_CS = "PO684_5_RL_GT3_RS_CS";
        public const string VARIANT_NAME_PO684_8_LL_SC = "PO684_8_LL_SC";
        public const string VARIANT_NAME_PO684_8_RL_SC = "PO684_8_RL_SC";
        public const string VARIANT_NAME_PO684_4_CUP = "PO684_4_CUP";
        public const string VARIANT_NAME_PO684_LL_PA_Basis = "PO684_LL_PA_Basis";
        public const string VARIANT_NAME_PO684_RL_PA_Basis = "PO684_RL_PA_Basis";
        public const string VARIANT_NAME_PO685_LL_PA_Basis = "PO685_LL_PA_Basis";
        public const string VARIANT_NAME_PO685_RL_PA_Basis = "PO685_RL_PA_Basis";
        public const string VARIANT_NAME_PO684_LL_PA_S_GTS = "PO684_LL_PA_S_GTS";
        public const string VARIANT_NAME_PO684_RL_PA_S_GTS = "PO684_RL_PA_S_GTS";
        public const string VARIANT_NAME_PO685_LL_PA_S_GTS = "PO685_LL_PA_S_GTS";
        public const string VARIANT_NAME_PO685_RL_PA_S_GTS = "PO685_RL_PA_S_GTS";
        public const string VARIANT_NAME_PO684_LL_PA_ohne_HSA_Basis = "PO684_LL_PA_ohne_HSA_Basis";
        public const string VARIANT_NAME_PO684_RL_PA_ohne_HSA_Basis = "PO684_RL_PA_ohne_HSA_Basis";
        public const string VARIANT_NAME_PO685_2_LL_PA_Basis = "PO685_2_LL_PA_Basis";
        public const string VARIANT_NAME_PO685_2_RL_PA_Basis = "PO685_2_RL_PA_Basis";
        public const string VARIANT_NAME_PO685_2_LL_PA_S_GTS = "PO685_2_LL_PA_S_GTS";
        public const string VARIANT_NAME_PO685_2_RL_PA_S_GTS = "PO685_2_RL_PA_S_GTS";
        public const string VARIANT_NAME_PO684_LL_PA_ohne_HSA_S_GTS = "PO684_LL_PA_ohne_HSA_S_GTS";
        public const string VARIANT_NAME_PO455_LL = "PO455_LL";
        public const string VARIANT_NAME_PO455_RL = "PO455_RL";
        public const string VARIANT_NAME_PO684_3_LL_PA_S_GTS = "PO684/3_LL_PA_S_GTS";
        public const string VARIANT_NAME_PO684_3_RL_PA_S_GTS = "PO684/3_RL_PA_S_GTS";
        public const string VARIANT_NAME_PO685_3_LL_PA_S_GTS = "PO685/3_LL_PA_S_GTS";
        public const string VARIANT_NAME_PO685_3_RL_PA_S_GTS = "PO685/3_RL_PA_S_GTS";
        public const string VARIANT_NAME_PO684_3_LL_PA_ohne_HSA = "PO684/3_LL_PA_ohne_HSA";
        public const string VARIANT_NAME_PO684_3_RL_PA_ohne_HSA = "PO684/3_RL_PA_ohne_HSA";

        public const string VARIANT_NAME_PA_GT3_RS_LL = "PA_GT3_RS_LL";
        public const string VARIANT_NAME_PA_GT3_RS_RL = "PA_GT3_RS_RL";

        public const string VARIANT_TOP_CAYMAN_LL = @"TOP Cayman LL";

        public static JdmPartColumnConfig PART_COMPARER = new(
            new[] { FIELD_NAME_BAUTEIL1, FIELD_NAME_BAUTEIL2, FIELD_NAME_BAUTEIL3, FIELD_NAME_BAUTEIL4 },
            new[]
            {
                new JdmPartColumnConfigUnit(new JdmComparerDouble(0), new []{FIELD_NAME_DICKE1, FIELD_NAME_DICKE2, FIELD_NAME_DICKE3, FIELD_NAME_DICKE4}),
                new JdmPartColumnConfigUnit(new JdmComparerString(), new []{FIELD_NAME_MATERIAL1, FIELD_NAME_MATERIAL2, FIELD_NAME_MATERIAL3, FIELD_NAME_MATERIAL4}),
            }
        );

        public static List<JdmColumnConfig> VDL_COLUMN_CONFIG = new()
        {
            new JdmColumnConfig(FIELD_NAME_NAME, "I", null,EXPORT_FIELD_NAME_NAME), //first value must be MFG name
            new JdmColumnConfig(FIELD_NAME_PROCESS, "K", new JdmComparerString(), EXPORT_FIELD_NAME_PROCESS),
            new JdmColumnConfig(FIELD_NAME_BEMERKUNG, "AX", null,FIELD_NAME_BEMERKUNG ),

            new JdmColumnConfig(FIELD_NAME_X, "R", new JdmComparerDouble(1),FIELD_NAME_X),
            new JdmColumnConfig(FIELD_NAME_Y, "S", new JdmComparerDouble(1),FIELD_NAME_Y),
            new JdmColumnConfig(FIELD_NAME_Z, "T", new JdmComparerDouble(1),FIELD_NAME_Z),

            new JdmColumnConfig(FIELD_NAME_END_X, "U", new JdmComparerDouble(1),FIELD_NAME_END_X),
            new JdmColumnConfig(FIELD_NAME_END_Y, "V", new JdmComparerDouble(1),FIELD_NAME_END_Y),
            new JdmColumnConfig(FIELD_NAME_END_Z, "W", new JdmComparerDouble(1),FIELD_NAME_END_Z),

            new JdmColumnConfig(FIELD_NAME_LINSENDURCHMESSER, "BP", new JdmComparerString(),EXPORT_FIELD_NAME_LINSENDURCHMESSER),
            new JdmColumnConfig(FIELD_NAME_LINSENDURCHMESSER2, "BQ", new JdmComparerString(),EXPORT_FIELD_NAME_LINSENDURCHMESSER2),
            new JdmColumnConfig(FIELD_NAME_NIETDURCHMESSER, "BR", new JdmComparerString(),EXPORT_FIELD_NAME_NIETDURCHMESSER),
            new JdmColumnConfig(FIELD_NAME_FDSDURCHMESSER, "BS", new JdmComparerString(),FIELD_NAME_FDSDURCHMESSER),
            new JdmColumnConfig(FIELD_NAME_FDSLAENGE, "BT", new JdmComparerString(),FIELD_NAME_FDSLAENGE),

            new JdmColumnConfig(FIELD_NAME_STUMPFNAHTDICKE, "BV", new JdmComparerString(),FIELD_NAME_STUMPFNAHTDICKE),
            new JdmColumnConfig(FIELD_NAME_KEHLNAHTDICKE, "BW", new JdmComparerString(),FIELD_NAME_KEHLNAHTDICKE),
            new JdmColumnConfig(FIELD_NAME_NAHTLAENGE, "BX", new JdmComparerString(),FIELD_NAME_NAHTLAENGE),
            new JdmColumnConfig(FIELD_NAME_KLEBSTOFF1, "BZ", new JdmComparerString(),FIELD_NAME_KLEBSTOFF1),
            new JdmColumnConfig(FIELD_NAME_KLEBSTOFF2, "CA", new JdmComparerString(),FIELD_NAME_KLEBSTOFF2),

            new JdmColumnConfig(FIELD_NAME_RESELEMENT, "CD", new JdmComparerString(),FIELD_NAME_RESELEMENT),
            new JdmColumnConfig(FIELD_NAME_CLINCHDURCHMESSER, "CE", new JdmComparerString(),FIELD_NAME_CLINCHDURCHMESSER),
            new JdmColumnConfig(FIELD_NAME_BOLZENTYP, "CF", new JdmComparerString(),FIELD_NAME_BOLZENTYP),

            new JdmColumnConfig(FIELD_NAME_BAUTEIL1, "X", null,EXPORT_FIELD_NAME_BAUTEIL1),
            new JdmColumnConfig(FIELD_NAME_DICKE1, "Y", null,EXPORT_FIELD_NAME_DICKE1),
            new JdmColumnConfig(FIELD_NAME_MATERIAL1, "Z", null,EXPORT_FIELD_NAME_MATERIAL1),

            new JdmColumnConfig(FIELD_NAME_BAUTEIL2, "AC", null,EXPORT_FIELD_NAME_BAUTEIL2),
            new JdmColumnConfig(FIELD_NAME_DICKE2, "AD", null,EXPORT_FIELD_NAME_DICKE2),
            new JdmColumnConfig(FIELD_NAME_MATERIAL2, "AE", null,EXPORT_FIELD_NAME_MATERIAL2),

            new JdmColumnConfig(FIELD_NAME_BAUTEIL3, "AH", null,EXPORT_FIELD_NAME_BAUTEIL3),
            new JdmColumnConfig(FIELD_NAME_DICKE3, "AI", null,EXPORT_FIELD_NAME_DICKE3),
            new JdmColumnConfig(FIELD_NAME_MATERIAL3, "AJ", null,EXPORT_FIELD_NAME_MATERIAL3),

            new JdmColumnConfig(FIELD_NAME_BAUTEIL4, "AM", null,EXPORT_FIELD_NAME_BAUTEIL4),
            new JdmColumnConfig(FIELD_NAME_DICKE4, "AN", null,EXPORT_FIELD_NAME_DICKE4),
            new JdmColumnConfig(FIELD_NAME_MATERIAL4, "AO", null,EXPORT_FIELD_NAME_MATERIAL4),
        };

        public static string[] FIELD_NAMES = new string[]
        {
            FIELD_NAME_BEMERKUNG,
            FIELD_NAME_PROCESS,
            FIELD_NAME_X,
            FIELD_NAME_Y,
            FIELD_NAME_Z,

            FIELD_NAME_START_X,
            FIELD_NAME_START_Y,
            FIELD_NAME_START_Z,

            FIELD_NAME_END_X,
            FIELD_NAME_END_Y,
            FIELD_NAME_END_Z,

            FIELD_NAME_DICKE1,
            FIELD_NAME_INFO_WERKSTOFF1,
            FIELD_NAME_DICKE2,
            FIELD_NAME_INFO_WERKSTOFF2,
            FIELD_NAME_DICKE3,
            FIELD_NAME_INFO_WERKSTOFF3,
            FIELD_NAME_DICKE4,
            FIELD_NAME_INFO_WERKSTOFF4,

            FIELD_NAME_LINSENDURCHMESSER,
            FIELD_NAME_LINSENDURCHMESSER2,
            FIELD_NAME_NIETDURCHMESSER,
            FIELD_NAME_FDSDURCHMESSER,
            FIELD_NAME_FDSLAENGE,

            FIELD_NAME_STUMPFNAHTDICKE,
            FIELD_NAME_KEHLNAHTDICKE,
            FIELD_NAME_NAHTLAENGE,
            FIELD_NAME_KLEBSTOFF1,
            FIELD_NAME_KLEBSTOFF2,

            FIELD_NAME_RESELEMENT,
            FIELD_NAME_CLINCHDURCHMESSER,
            FIELD_NAME_BOLZENTYP,
        };

        public static string[] VARIANT_NAMES = new[]
        {
            VARIANT_NAME_PO684_LL,
            VARIANT_NAME_PO684_RL,
            VARIANT_NAME_PO685_LL,
            VARIANT_NAME_PO685_RL,
            VARIANT_NAME_PO684_5_LL,
            VARIANT_NAME_PO684_5_RL,
            VARIANT_NAME_PO685_5_LL,
            VARIANT_NAME_PO685_5_RL,
            VARIANT_NAME_PO685_3_LL,
            VARIANT_NAME_PO685_3_RL,
            VARIANT_NAME_PO684_4_LL,
            VARIANT_NAME_PO684_4_RL,
            VARIANT_NAME_PO684_4_LL_CS,
            VARIANT_NAME_PO684_4_RL_CS,
            VARIANT_NAME_PO684_5_LL_GT3_RS,
            VARIANT_NAME_PO684_5_RL_GT3_RS,
            VARIANT_NAME_PO684_5_LL_GT3_RS_CS,
            VARIANT_NAME_PO684_5_RL_GT3_RS_CS,
            VARIANT_NAME_PO684_8_LL_SC,
            VARIANT_NAME_PO684_8_RL_SC,
            VARIANT_NAME_PO684_4_CUP,
            VARIANT_NAME_PO684_LL_PA_Basis,
            VARIANT_NAME_PO684_RL_PA_Basis,
            VARIANT_NAME_PO685_LL_PA_Basis,
            VARIANT_NAME_PO685_RL_PA_Basis,
            VARIANT_NAME_PO684_LL_PA_S_GTS,
            VARIANT_NAME_PO684_RL_PA_S_GTS,
            VARIANT_NAME_PO685_LL_PA_S_GTS,
            VARIANT_NAME_PO685_RL_PA_S_GTS,
            VARIANT_NAME_PO684_LL_PA_ohne_HSA_Basis,
            VARIANT_NAME_PO684_RL_PA_ohne_HSA_Basis,
            VARIANT_NAME_PO685_2_LL_PA_Basis,
            VARIANT_NAME_PO685_2_RL_PA_Basis,
            VARIANT_NAME_PO685_2_LL_PA_S_GTS,
            VARIANT_NAME_PO685_2_RL_PA_S_GTS,
            VARIANT_NAME_PO684_LL_PA_ohne_HSA_S_GTS,
            VARIANT_NAME_PO455_LL,
            VARIANT_NAME_PO455_RL,
            VARIANT_NAME_PO684_3_LL_PA_S_GTS,
            VARIANT_NAME_PO684_3_RL_PA_S_GTS,
            VARIANT_NAME_PO685_3_LL_PA_S_GTS,
            VARIANT_NAME_PO685_3_RL_PA_S_GTS,
            VARIANT_NAME_PO684_3_LL_PA_ohne_HSA,
            VARIANT_NAME_PO684_3_RL_PA_ohne_HSA,
            VARIANT_NAME_PA_GT3_RS_LL,
            VARIANT_NAME_PA_GT3_RS_RL,
            VARIANT_TOP_CAYMAN_LL,
        };

        //public static Dictionary<string, Program.ChangeTypes> FieldNameVsChangeTypesMapTable = new()
        //{
        //    { FIELD_NAME_X, Program.ChangeTypes.XyzChanged },
        //    { FIELD_NAME_Y, Program.ChangeTypes.XyzChanged },
        //    { FIELD_NAME_Z, Program.ChangeTypes.XyzChanged },
        //    { FIELD_NAME_END_X, Program.ChangeTypes.XyzChanged },
        //    { FIELD_NAME_END_Y, Program.ChangeTypes.XyzChanged },
        //    { FIELD_NAME_END_Z, Program.ChangeTypes.XyzChanged },
        //};

        public static string[] ContinuesProcessTypes = new[] //for continues start-x is used as x 
        {
            "Kleben_PN1080 (Bahn)",
            "Kleben (Bahn)",
            "Kleben (Flaeche)",
            "Falzkleben",
            "MAG-Schweissen (Kehlnaht)",
            "MIG-Schweissen (Kehlnaht)",
            "MIG-Schweissen (Stumpfnaht)",
        };
    }
}
