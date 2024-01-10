using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;

namespace ExcelProtectionRemover
{

    public class ExcelProtectionRemover
    {
        public void RemoveProtectionFromFile(string filePath)
        {
            var extract2DirName = ExtractExcel2Directory(filePath);
            RemoveProtectionFromExcelSheets(extract2DirName);
            var newZip = PackDir2Zip(extract2DirName);
            RenameZip2Xlsm(newZip, filePath);


        }

        private void RenameZip2Xlsm(string newZip, string oldFilePath)
        {
            System.IO.File.Move(newZip, oldFilePath, true);
        }

        private string PackDir2Zip(string extract2DirName)
        {
            var finalZipName = extract2DirName.Substring(0, extract2DirName.Length - 1);
            finalZipName += ".zip";
            ZipFile.CreateFromDirectory(extract2DirName, finalZipName);
            return finalZipName;
        }


        private void RemoveProtectionFromExcelSheets(string extract2DirName)
        {
            var excelSheetDir = extract2DirName + @"xl\worksheets\";
            var allFiles = Directory.GetFiles(excelSheetDir, "*.xml", SearchOption.TopDirectoryOnly);
            var files2Change = allFiles.Where(m => Path.GetFileNameWithoutExtension(m).StartsWith("sheet"));


            foreach (var file in files2Change)
            {
                var allLines = File.ReadAllText(file);
                var afterReplace = Regex.Replace(allLines, @"<sheetProtection.*?>", "");
                File.WriteAllText(file, afterReplace);
            }
        }

        private string ExtractExcel2Directory(string excelPath)
        {
            var parentDir = Directory.GetParent(excelPath).FullName;
            var extract2DirName = parentDir + "\\" + Path.GetFileNameWithoutExtension(excelPath) + "\\";

            ZipFile.ExtractToDirectory(excelPath, extract2DirName, true);

            return extract2DirName;
        }
    }
}