//using GemBox.Spreadsheet;
//using System.Collections.Generic;

//namespace JoinningDataManager
//{
//    public class JdmNewPointsExporter
//    {
//        public JdmNewPointsExporter()
//        {

//        }
//        public void ExportNewPoints2Excel(string excelPath, List<JdmVdlPoint> vtaNewPoints, List<JdmColumnConfig> columnConfigs)
//        {
//            var workbook = new ExcelFile();
//            var worksheet = workbook.Worksheets.Add("NewPoints");

//            for (var pointIndex = 0; pointIndex < vtaNewPoints.Count; pointIndex++)
//            {
//                var point = vtaNewPoints[pointIndex];
//                foreach (var columnConfig in columnConfigs)
//                {
//                    var columnIndex = ExcelColumnCollection.ColumnNameToIndex(columnConfig.VdlColumnName);
//                    if (columnConfig.FieldName.Equals(JdmConst.FIELD_NAME_NAME))
//                        worksheet.Cells[pointIndex, columnIndex].Value = point.Name;
//                    else
//                        worksheet.Cells[pointIndex, columnIndex].Value = point.GetFields2Compare(columnConfig.FieldName);
//                }
//            }

//            workbook.Save(excelPath);
//        }
//    }
//}