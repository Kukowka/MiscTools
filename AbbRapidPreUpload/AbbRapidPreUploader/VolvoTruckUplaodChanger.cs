using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMS_Utils.Utils.BackupReading.Backup.ABB;
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

        private const string SPOT_DATA_BEGIN_REG_EX = @".*spotdata.*:=";
        private const string VALUE_IN_BRACKET_REGEX = @"\[.*\];";

        private const string SPOT_DATA_REG_EX = SPOT_DATA_BEGIN_REG_EX + VALUE_IN_BRACKET_REGEX;


        public VolvoTruckUplaodChanger(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        public bool ShouldFixSyntax(string uploadFileContent)
        {
            var shouldCorrectSpotDefs = HasTextWronSpotDef(uploadFileContent);

            var reader = new Std_BR_ABBPorscheProcReader();
            _procesInFile = reader.ReadAllProcPrgInModFile(uploadFileContent, "");

            if (shouldCorrectSpotDefs)
                return true;

            if (_procesInFile.Any(m => m.ProcessPoints.Any(n => HasPointSpotMotion(n as Std_BR_AbbPorscheBackupPoint))))
                return true;

            return false;
        }

        private static bool HasTextWronSpotDef(string uploadFileContent)
        {
            var withoutWhiteSigns = uploadFileContent.Replace(" ", "");
            var shouldCorrectSpotDefs = Regex.IsMatch(withoutWhiteSigns, SPOT_DATA_REG_EX);
            return shouldCorrectSpotDefs;
        }


        public string FixSyntax(string uploadFileContent, string modFilePath)
        {
            var result = ReduceNrOfParamsInSpotData(uploadFileContent);
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
            if (point.PointDef is null)
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

        private static string[] SplittAndSwapValuesInArray(string callDefAsText)
        {
            var splitted = callDefAsText.Split(',');

            if (splitted.Length != 5)
                throw new NotImplementedException("point definition is different");

            splitted.SwapValues(2, 3);
            return splitted;
        }

        private string ReduceNrOfParamsInSpotData(string uploadFileContent)
        {
            var splittedByWhiteLine = uploadFileContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            for (var index = 0; index < splittedByWhiteLine.Length; index++)
            {
                var line = splittedByWhiteLine[index];

                if (HasTextWronSpotDef(line))
                    splittedByWhiteLine[index] = ReduceNrOfParamsInSpotDataInLine(line);
            }

            var result = string.Join(Environment.NewLine, splittedByWhiteLine);
            return result;
        }

        private string ReduceNrOfParamsInSpotDataInLine(string line)
        {
            var valueBeforeEqualSign = Regex.Match(line, SPOT_DATA_BEGIN_REG_EX).Value;
            var orgInBracket = Regex.Match(line, VALUE_IN_BRACKET_REGEX).Value;


            var paramsAsText = orgInBracket.Replace("[", "").Replace("];", "");

            var splitted = paramsAsText.Split(',');

            var result = valueBeforeEqualSign + "[" + string.Join(",", splitted.Take(4)) + "];";

            return result;
        }
    }
}