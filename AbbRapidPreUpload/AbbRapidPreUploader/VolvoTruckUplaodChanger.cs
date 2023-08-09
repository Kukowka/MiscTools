using System;
using System.Linq;
using System.Text.RegularExpressions;

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

        private const string SPOT_DATA_BEGIN_REG_EX = @".*spotdata.*:=";
        private const string VALUE_IN_BRACKET_REGEX = @"\[.*\];";

        private const string SPOT_DATA_REG_EX = SPOT_DATA_BEGIN_REG_EX + VALUE_IN_BRACKET_REGEX;


        public VolvoTruckUplaodChanger(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        public bool HasSpotDataDefsInText(string uploadFileContent)
        {
            var withoutWhiteSigns = uploadFileContent.Replace(" ", "");
            return Regex.IsMatch(withoutWhiteSigns, SPOT_DATA_REG_EX);
        }


        public string FixSyntax(string uploadFileContent)
        {
            var result = ReduceNrOfParamsInSpotData(uploadFileContent);
            return result;
        }

        private string ReduceNrOfParamsInSpotData(string uploadFileContent)
        {
            var splittedByWhiteLine = uploadFileContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            for (var index = 0; index < splittedByWhiteLine.Length; index++)
            {
                var line = splittedByWhiteLine[index];

                if (HasSpotDataDefsInText(line))
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