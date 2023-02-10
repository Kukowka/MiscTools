using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace AbbRapidPreUpload
{
    class Program
    {
        public static string ORG_BAT_FILE_NAME = "AbbRapidPreUpload.bat";
        public static string DETECT_FILE_PATH_PREFIX = "UPLOAD_FILE,";
        static void Main(string[] args)
        {
            RunTmsRefactoringCode(args);
            RunOriginalBat(args[0]);
            //Thread.Sleep(1); //necessary, otherwise Process Simulate say it does not work
        }

        private static void RunTmsRefactoringCode(string[] args)
        {
            if (args.Length == 0)
                return;

            if (!Try2GetUploadFilePath(args[0], out string filePath))
                return;

            var fileText = File.ReadAllText(filePath);
            var fileTextAfterRefactor = RefactorSingleFile(fileText);
            File.WriteAllText(filePath, fileTextAfterRefactor);
        }

        private static bool Try2GetUploadFilePath(string tmpFilePath, out string uploadedFilePath)
        {
            var allLines = File.ReadAllText(tmpFilePath);

            var match = Regex.Match(allLines, "^(" + DETECT_FILE_PATH_PREFIX + ").*", RegexOptions.Multiline);
            if (!match.Success)
            {
                uploadedFilePath = null;
                return false;
            }

            uploadedFilePath = match.Value.Replace(DETECT_FILE_PATH_PREFIX, "").Trim();
            return true;
        }

        private static string RefactorSingleFile(string fileText)
        {
            var lines = SplitByNewLine(fileText).ToList();

            for (var index = lines.Count - 1; index >= 0; index--)
            {
                var line = lines[index];
                var withoutSpaces = line.Replace(" ", "");

                if (withoutSpaces.StartsWith("move", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (!line.EndsWith(";"))
                    {
                        if (index == line.Length - 1)
                            break;

                        lines[index] += lines[index + 1].Trim();
                        lines.RemoveAt(index + 1);
                    }
                }
            }

            fileText = string.Join("\r\n", lines);

            return fileText;
        }

        private static void RunOriginalBat(string arg1)
        {
            var process = new Process
            {
                StartInfo = { Arguments = String.Format("\"{0}\" ", arg1) }
            };
            process.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + ORG_BAT_FILE_NAME;
            process.Start();
        }

        public static string[] SplitByNewLine(string source) => source.Split(new[] { Environment.NewLine, "\n" }, StringSplitOptions.None);
    }
}
