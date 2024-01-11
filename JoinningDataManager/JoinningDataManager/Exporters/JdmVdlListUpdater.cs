using GemBox.Spreadsheet;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JoinningDataManager
{
    public class JdmVdlListUpdater
    {
        private ExcelFile _excelFile;
        private string _newExcelPath;

        public void UpdateExistingList(string existingListPath, string outDataDir, List<JdmCompareReport> reports, string updateTag, string updateTagColumnName)
        {
            CopyAndLoadExcel(existingListPath, outDataDir);

            var validReports = reports.Where(m => m.ChangeType == Program.ChangeTypes.ParamChanged || m.ChangeType == Program.ChangeTypes.XyzChanged);

            foreach (var report in reports)
            {


            }

            _excelFile.Save(_newExcelPath);
        }

        private void CopyAndLoadExcel(string existingListPath, string outDataDir)
        {
            var existingExcelName = Path.GetFileNameWithoutExtension(existingListPath);
            var existingExcelExtension = Path.GetExtension(existingListPath);
            _newExcelPath = outDataDir + existingExcelName + "_updated" + existingExcelExtension;
            File.Copy(existingListPath, _newExcelPath, true);
            _excelFile = ExcelFile.Load(_newExcelPath);
        }
    }
}