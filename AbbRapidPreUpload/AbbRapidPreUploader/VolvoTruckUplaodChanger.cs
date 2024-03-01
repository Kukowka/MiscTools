using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMS_Utils.Utils.BackupReading.Backup.ABB;
using TMS_Utils.Utils.BackupReading.Backup.ABB.AbbLocalDefs;
using TMS_Utils.Utils.BackupReading.Backup.BackupProc;
using TMS_Utils.Utils.BackupReading.BackupReaders.ABB;
using TMS_Utils.Utils.Misc;

namespace AbbRapidPreUploader
{
    /// <summary>
    /// For not it is quite simple, it replace 6 args spot data, to 4 args spotData
    /// LOCAL PERS spotdata sd_wp429210:=[429210,1,1,1,1,-1];
    /// to:
    /// LOCAL PERS spotdata sd_wp429210:=[429210,1,1,1];
    /// </summary>
    public class VolvoTruckUplaodChanger
    {
        private readonly IFileManager _fileManager;
        private List<IStd_BR_BackupProc> _procesInFile;
        private Std_BR_AbbVolvoTruckSyntaxFixer _volvoTruckSyntaxFixer = new Std_BR_AbbVolvoTruckSyntaxFixer();

        private const string SPOT_DATA_BEGIN_REG_EX = @".*spotdata.*:=";
        private const string VALUE_IN_BRACKET_REGEX = @"\[.*\];";

        private const string SPOT_DATA_REG_EX = SPOT_DATA_BEGIN_REG_EX + VALUE_IN_BRACKET_REGEX;


        public VolvoTruckUplaodChanger(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        public bool ShouldFixSyntax(string uploadFileContent)
        {
            var shouldCorrectSpotDefs = _volvoTruckSyntaxFixer.Has2CorrectSyntax(uploadFileContent);

            var reader = new Std_BR_ABBPorscheProcReader();
            _procesInFile = reader.ReadAllProcPrgInModFile(uploadFileContent, "", out _);

            if (shouldCorrectSpotDefs)
                return true;

            if (_procesInFile.Any(m => m.ProcessPoints.Any(n => HasPointSpotMotion(n as Std_BR_AbbPorscheBackupPoint))))
                return true;

            return false;
        }

        public string FixSyntax(string uploadFileContent, string modFilePath)
        {
            var result = _volvoTruckSyntaxFixer.CorrectSyntax();
            result = SwapSpotDataWithGunData(result);
            return result;
        }

        private string SwapSpotDataWithGunData(string fileContent)
        {
            var result = fileContent;
            foreach (var proc in _procesInFile)
            {
                foreach (Std_BR_AbbPorscheBackupPoint point in proc.ProcessPoints)
                {
                    if (HasPointSpotMotion(point))
                    {
                        var newPointCall = SwapSpotDataWithGunData(point);
                        result = result.Replace(point.CallDefAsText, newPointCall);
                    }
                }
            }

            return result;
        }

        private static bool HasPointSpotMotion(Std_BR_AbbPorscheBackupPoint point)
        {
            return point.Type == Std_BR_AbbPorscheBackupPoint.PointTypes.SpotL || point.Type == Std_BR_AbbPorscheBackupPoint.PointTypes.SpotJ;
        }

        private string SwapSpotDataWithGunData(Std_BR_AbbPorscheBackupPoint point)
        {
            var callDefAsText = point.CallDefAsText;

            string[] splitted = null;
            if (point.HasInlinePointDefinition()) // SpotL [[-1366.65,1108.96,920.06],[0.00444309,-0.038809,-0.00344693,0.999226],[1,0,-2,0],[9E+09,115.501,9E+09,9E+09,9E+09,9E+09]], vmax, XQ11Gun, Sd_wp7614_30, XQ11GunTCP;
            {
                var inlinePointDef = point.GetInlinePointDefinition();
                callDefAsText = point.CallDefAsText.Replace(inlinePointDef, "");
                splitted = SplittAndSwapValuesInArray(callDefAsText);
                splitted[0] += " " + inlinePointDef;
            }
            else
                splitted = SplittAndSwapValuesInArray(callDefAsText);


            var result = string.Join(",", splitted);
            return result;
        }

        //SpotL wp31274_2,vmax,XQ11Gun,Sd_wp31274_2_20\QuickRelease,XQ11GunTCP\WObj:=wFML2_CabFrame;
        //SpotL wp31154,vmax,XQ11Gun,Sd_wp31154_20,\QuickRelease,XQ11GunTCP\WObj:=wFML2_CabFrame;
        private static string[] SplittAndSwapValuesInArray(string callDefAsText)
        {
            var splitted = callDefAsText.Split(',');

            if (!(splitted.Length == 5 || splitted.Length == 6))
                throw new NotImplementedException("point definition is different");

            splitted.SwapValues(2, 3);
            return splitted;
        }
    }
}