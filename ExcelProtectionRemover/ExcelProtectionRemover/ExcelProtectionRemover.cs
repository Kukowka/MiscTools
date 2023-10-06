using System.IO;
using System.IO.Compression;

namespace ExcelProtectionRemover
{
    public class ExcelProtectionRemover
    {
        public void RemoveProtectionFromFile(string filePath)
        {
            var extract2DirName =  ExtractExcel2Directory(filePath);
            RemoveProtectionFromExcelSheets(extract2DirName);
        }

        private void RemoveProtectionFromExcelSheets(string extract2DirName)
        {
            throw new System.NotImplementedException();
        }

        private string ExtractExcel2Directory(string excelPath)
        {
            var parentDir = Directory.GetParent(excelPath).FullName;
            var extract2DirName = parentDir + "\\" + Path.GetFileNameWithoutExtension(excelPath);

            ZipFile.ExtractToDirectory(excelPath, extract2DirName);

            return extract2DirName;
        }
    }
}