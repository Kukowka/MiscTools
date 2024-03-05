using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AbbRapidPreUploader
{
    public class PsTempFileReader
    {

        public static string UPLOAD_PATH_BEGIN = "UPLOAD_FILE,";
        public static string UPLOAD_PATH_REGEX = UPLOAD_PATH_BEGIN + ".*";

        public static string RRS_VERSION_BEGIN = "RRS_VERSION";
        public static string RRS_VERSION_REGEX = RRS_VERSION_BEGIN + ".*";

        private static string VOLVO_TRUCK_PREFIX = ",VolvoTruck_";

        public static string DOWNLOAD_FILES_BEGIN = "DOWNLOAD_FILES";
        public static string DOWNLOAD_FILES_REGEX = DOWNLOAD_FILES_BEGIN + ".*";

        public static string DOWNLOAD_FOLDER_BEGIN = "DOWNLOAD_FOLDER";
        public static string DOWNLOAD_FOLDER_REGEX = DOWNLOAD_FOLDER_BEGIN + ".*";

        private readonly IFileManager _fileManager;

        public PsTempFileReader(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        public string GetDetailsFromUploadTempFile(string tmpFilePath, out int rrsVersion)
        {
            var content = _fileManager.FileReadAllText(tmpFilePath);

            var match = Regex.Match(content, UPLOAD_PATH_REGEX);

            var filePath = Regex.Replace(match.Value, "^" + UPLOAD_PATH_BEGIN, "");

            rrsVersion = GetRrsVersion(content);

            return filePath.Trim();
        }

        private static int GetRrsVersion(string content)
        {
            var rrsVersionMatch = Regex.Match(content, RRS_VERSION_REGEX);
            var rrsVersionText = Regex.Replace(rrsVersionMatch.Value, "^" + RRS_VERSION_BEGIN, "");
            rrsVersionText = rrsVersionText.Replace(VOLVO_TRUCK_PREFIX, "");
            rrsVersionText = Regex.Replace(rrsVersionText, @"\..*", "");
            rrsVersionText = rrsVersionText.Replace(",", "");

            var rrsVersion = int.Parse(rrsVersionText.Trim());
            return rrsVersion;
        }

        public string GetDetailsFromDownloadTempFile(string tmpFilePath, out int rrsVersion)
        {
            var content = _fileManager.FileReadAllText(tmpFilePath);

            var match = Regex.Match(content, DOWNLOAD_FOLDER_REGEX);
            var dirPath = Regex.Replace(match.Value, "^" + DOWNLOAD_FOLDER_BEGIN, "");
            dirPath = dirPath.Substring(1, dirPath.Length - 1).Trim();

            var filesMatch = Regex.Match(content, DOWNLOAD_FILES_REGEX);
            var filesAsLine = Regex.Replace(filesMatch.Value, "^" + DOWNLOAD_FILES_BEGIN, "");

            var files = filesAsLine.Split(',');

            var modFile = files.First(m => m.EndsWith(".mod"));
            modFile = modFile.Trim();

            var result = Path.Combine(dirPath, modFile);

            rrsVersion = GetRrsVersion(content);
            return result;
        }
    }
}