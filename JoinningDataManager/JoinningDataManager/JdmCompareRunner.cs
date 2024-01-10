using System.Collections.Generic;
using System.Linq;

namespace JoinningDataManager
{
    public class JdmCompareRunner
    {
        private readonly List<JdmRawVtaPoint> _vtaPoints;
        private readonly List<JdmVdlPoint> _vdlPoints;
        private readonly List<JdmColumnConfig> _vdlColumnConfig;

        public JdmCompareRunner(List<JdmRawVtaPoint> vtaPoints, List<JdmVdlPoint> vdlPoints, List<JdmColumnConfig> vdlColumnConfig)
        {
            _vtaPoints = vtaPoints;
            _vdlPoints = vdlPoints;
            _vdlColumnConfig = vdlColumnConfig;
        }
        public List<JdmCompareReport> ComparePoints()
        {
            var result = new List<JdmCompareReport>();

            foreach (var vtaPoint in _vtaPoints)
            {
                var matchingVdlPoint = _vdlPoints.FirstOrDefault(m => m.Name.Equals(vtaPoint.Name));

                if (matchingVdlPoint is null)//means point is null
                {
                    result.Add(new JdmCompareReport(vtaPoint.Name));
                    continue;
                }

                var paramsReports = CompareParameters(vtaPoint, matchingVdlPoint);
                result.AddRange(paramsReports);
            }

            return result;
        }


        private List<JdmCompareReport> CompareParameters(JdmRawVtaPoint vtaPoint, JdmVdlPoint matchingVdlPoint)
        {
            var result = new List<JdmCompareReport>();

            for (var index = 1; index < _vdlColumnConfig.Count; index++)
            {
                var columnConfig = _vdlColumnConfig[index];
                if (columnConfig.Comparer is null)
                    continue;

                var fieldName = columnConfig.FieldName;
                var vtaVal = vtaPoint.GetFields2Compare(fieldName);
                var vdlVal = matchingVdlPoint.GetFields2Compare(fieldName);
                var areEqual = columnConfig.Comparer.AreEqual(vtaVal, vdlVal);

                if (!areEqual)
                {
                    var changeType = JdmExtension.ConvertFiledName2ChangeType(fieldName);
                    result.Add(new JdmCompareReport(changeType, vtaPoint.Name, fieldName, vdlVal, vtaVal));
                }
            }

            return result;
        }

        public List<string> GetRedundantVariants(List<JdmVdlPoint> vdlPoints, string[] variantNames)
        {
            var variantVsPointColumns = variantNames.ToDictionary(m=>m, m=>GetPointVariantColumn(vdlPoints,m));
            var result = new List<string>();

            foreach (var variantVsPointColumn in variantVsPointColumns)
            {
                var sameColumns = variantVsPointColumns.Where(m => m.Value.Equals(variantVsPointColumn.Value));

                if (sameColumns.Count() > 1)
                    result.Add(variantVsPointColumn.Key);
            }


            return result;
        }

        private static string GetPointVariantColumn(List<JdmVdlPoint> vdlPoints, string variantName)
        {
            var variantVals = vdlPoints.Select(m => m.GetFields2Compare(variantName));
            var firstPointVariantColumn = string.Join(";", variantVals);
            return firstPointVariantColumn;
        }
    }
}