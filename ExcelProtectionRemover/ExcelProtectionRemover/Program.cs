using System;

namespace ExcelProtectionRemover
{
    class Program
    {
        public const string ROOT = @"e:\20231006\OneDrive_1_10-6-2023\WOB_K6BHIN";

        static void Main(string[] args)
        {
            var protectionRemover = new ExcelProtectionRemover();
            protectionRemover.RemoveProtectionFromFile(@"e:\20231006\OneDrive_1_10-6-2023\WOB_K6BHIN\11_A33_31D_433574_WPS_BHIN_ST1430_V00_20230629.xlsm");
        }


    }
}
