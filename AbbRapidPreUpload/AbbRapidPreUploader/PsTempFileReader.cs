using System;
using System.Text.RegularExpressions;

namespace AbbRapidPreUploader
{
    public class PsTempFileReader
    {

        public static string UPLOAD_PATH_BEGIN = "UPLOAD_FILE,";

        public static string UPLOAD_PATH_REGEX = UPLOAD_PATH_BEGIN + ".*";
        private IFileManager _fileManager;

        public PsTempFileReader(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        public string GetUploadFilePath( string tmpFilePath)
        {
            var content = _fileManager.FileReadAllText(tmpFilePath);

            var match = Regex.Match(content, UPLOAD_PATH_REGEX);


            var filePath = Regex.Replace(match.Value, "^" + UPLOAD_PATH_BEGIN, "");
            return filePath.Trim();
        }
    }
}