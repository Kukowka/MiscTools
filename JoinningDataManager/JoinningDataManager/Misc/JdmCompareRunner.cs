using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JoinningDataManager
{
    public class JdmCompareRunner
    {
        private readonly List<JdmVdlPoint> _vtaPoints;
        private readonly List<JdmVdlPoint> _vdlPoints;
        private readonly List<JdmColumnConfig> _vdlColumnConfig;
        private readonly List<JdmCompareReport> _result = new();

        public JdmCompareRunner(List<JdmVdlPoint> vtaPoints, List<JdmVdlPoint> vdlPoints, List<JdmColumnConfig> vdlColumnConfig)
        {
            _vtaPoints = vtaPoints;
            _vdlPoints = vdlPoints;
            _vdlColumnConfig = vdlColumnConfig;
        }
        public List<JdmCompareReport> ComparePoints()
        {
            GetNewPoints();
            GetDeletedPoints();
            CompareFields();

            return _result;
        }

        private void GetNewPoints()
        {
            for (var index = _vtaPoints.Count - 1; index >= 0; index--)
            {
                var vtaPoint = _vtaPoints[index];
                var matchingVdlPoint = _vdlPoints.FirstOrDefault(m => m.Name.Equals(vtaPoint.Name));

                if (matchingVdlPoint is null) //means point is new
                {
                    _result.Add(new JdmCompareReport(vtaPoint.Name, vtaPoint));
                    _vtaPoints.RemoveAt(index);
                }
            }
        }

        private void GetDeletedPoints()
        {
            for (var index = _vdlPoints.Count - 1; index >= 0; index--)
            {
                var vdlPoint = _vdlPoints[index];

                if (vdlPoint.Name.Equals("983.803.001___-025-E9-002-L"))
                {
                }

                var matchingVtaPoint = _vtaPoints.FirstOrDefault(m => m.Name.Equals(vdlPoint.Name));

                if (matchingVtaPoint is null)
                {
                    _result.Add(new JdmCompareReport(vdlPoint.Name, Program.ChangeTypes.Deleted));
                    _vdlPoints.RemoveAt(index);
                }
            }
        }

        private void CompareFields()
        {
            foreach (var vtaPoint in _vtaPoints)
            {
                var matchingVdlPoint = _vdlPoints.FirstOrDefault(m => m.Name.Equals(vtaPoint.Name));

                if (matchingVdlPoint is null) //means point is new
                    throw new InvalidDataException("new points should be filtered before calling this method");

                var paramsReports = CompareParameters(vtaPoint, matchingVdlPoint);
                _result.AddRange(paramsReports);
            }
        }


        private List<JdmCompareReport> CompareParameters(JdmVdlPoint vtaPoint, JdmVdlPoint matchingVdlPoint)
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

                try
                {
                    var areEqual = columnConfig.Comparer.AreEqual(vtaVal, vdlVal);

                    if (!areEqual)
                    {
                        var changeType = JdmExtension.ConvertFiledName2ChangeType(fieldName);
                        result.Add(new JdmCompareReport(changeType, vtaPoint.Name, fieldName, vdlVal, vtaVal));
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }

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