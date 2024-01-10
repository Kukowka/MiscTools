using System;
using System.IO;

namespace ExcelProtectionRemover
{
    class Program
    {
        public const string ROOT = @"e:\20231007\OneDrive_1_10-7-2023";

        static void Main(string[] args)
        {
            var allExcelFiles = Directory.GetFiles(ROOT, "*.xlsm", SearchOption.AllDirectories);

            foreach (var excelFile in allExcelFiles)
            {
                Console.WriteLine(excelFile);
                var protectionRemover = new ExcelProtectionRemover();
                protectionRemover.RemoveProtectionFromFile(excelFile);
            }
        }


    }
}
