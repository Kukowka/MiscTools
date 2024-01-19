using JoinningDataManager.ChangeReport;
using JoinningDataManager.Comparers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JoinningDataManager
{
    public class JdmCompareRunner
    {
        private List<JdmVdlPoint> _vtaPoints;
        private List<JdmVdlPoint> _vdlPoints;
        private List<JdmColumnConfig> _vdlColumnConfig;

        private readonly List<IJdmChangeReport> _result = new();
        private JdmPartColumnConfig _partComparer;

        public JdmCompareRunner()
        {

        }
        public List<IJdmChangeReport> ComparePoints(List<JdmVdlPoint> vtaPoints, List<JdmVdlPoint> vdlPoints, List<JdmColumnConfig> vdlColumnConfig, JdmPartColumnConfig partComparer)
        {
            var notUniqueColumnLetter = vdlColumnConfig.GroupBy(m => m.VdlColumnName).Where(m => m.Count() > 1).ToList();

            if (notUniqueColumnLetter.Count > 0)
                throw new ArgumentOutOfRangeException(nameof(vdlColumnConfig), "Column letter in excel must be unique");

            var notUniqueFieldName = vdlColumnConfig.GroupBy(m => m.FieldName).Where(m => m.Count() > 1).ToList();

            if (notUniqueFieldName.Count > 0)
                throw new ArgumentOutOfRangeException(nameof(vdlColumnConfig), "Column letter in excel must be unique");

            _vtaPoints = vtaPoints;
            _vdlPoints = vdlPoints;
            _vdlColumnConfig = vdlColumnConfig;
            _partComparer = partComparer;

            GetNewPoints();
            GetDeletedPoints();
            CompareFields();

            if (partComparer is not null)
                CompareParts();

            return _result;
        }

        private void CompareParts()
        {
            var diffPartReports = new List<JdmChangeReportDiffPart>();
            var diffPartPropReports = new List<JdmChangeReportDiffPartPropertie>();
            var swappedPartReports = new List<JdmChangeReportPartSwapped>();

            foreach (var vtaPoint in _vtaPoints)
            {
                var matchingVdlPoint = _vdlPoints.FirstOrDefault(m => m.Name.Equals(vtaPoint.Name));

                if (matchingVdlPoint is null) //means point is new
                    throw new InvalidDataException("new points should be filtered before calling this method");



                for (var index = 0; index < _partComparer.PartNameColumns.Length; index++)
                {
                    if (vtaPoint.Name.Equals("983.800.702___-053-B2-001-L"))
                    {

                    }
                    var partNameColumn = _partComparer.PartNameColumns[index];
                    if (WasPartSwapped(index, vtaPoint, matchingVdlPoint, out JdmChangeReportPartSwapped swappedReport))
                    {
                        swappedPartReports.Add(swappedReport);
                        continue;
                    }

                    if (WasDifferentPartAssigned(partNameColumn, vtaPoint, matchingVdlPoint, out var differentPartReport))
                    {
                        AddDifferentPartAssignedReportWithoutRepetitions(ref diffPartReports, differentPartReport);
                        continue;
                    }

                    if (WasPartPropertyChanged(index, vtaPoint, matchingVdlPoint, out var differentPartPropReport))
                        AddDifferentPartPropertieReportWithoutRepetitions(ref diffPartPropReports, differentPartPropReport);
                }
            }

            _result.AddRange(diffPartReports);
            _result.AddRange(diffPartPropReports);
            _result.AddRange(swappedPartReports);
        }

        private void AddDifferentPartPropertieReportWithoutRepetitions(ref List<JdmChangeReportDiffPartPropertie> diffPartReports, JdmChangeReportDiffPartPropertie differentPartPropReport)
        {
            var matchingReport = diffPartReports.FirstOrDefault(m => m.PartName.Equals(differentPartPropReport.PartName)
                                                                     && m.PropertieName.Equals(differentPartPropReport.PropertieName)
                                                                     && m.NewValue.Equals(differentPartPropReport.NewValue)
                                                                     && m.OldValue.Equals(differentPartPropReport.OldValue));
            if (matchingReport is not null)
            {
                matchingReport.AffectedPoints.AddRange(differentPartPropReport.AffectedPoints);
                return;
            }

            diffPartReports.Add(differentPartPropReport);
        }

        private void AddDifferentPartAssignedReportWithoutRepetitions(ref List<JdmChangeReportDiffPart> diffPartReports, JdmChangeReportDiffPart differentPartReport)
        {
            var matchingReport = diffPartReports.FirstOrDefault(m => m.OldPartName.Equals(differentPartReport.OldPartName));

            if (matchingReport is not null)
                matchingReport.AffectedPointNames.AddRange(differentPartReport.AffectedPointNames);
            else
                diffPartReports.Add(differentPartReport);
        }

        private bool WasPartPropertyChanged(int partNameColumnIndex, JdmVdlPoint vtaPoint, JdmVdlPoint vdlPoint, out JdmChangeReportDiffPartPropertie outReport)
        {
            if (vtaPoint.Name.Equals("983.800.702___-053-B2-001-L"))
            {

            }

            foreach (var partPropertieUnit in _partComparer.PartPropertieUnits)
            {
                var filedName = partPropertieUnit.PartPropertieNames[partNameColumnIndex];
                var compared = partPropertieUnit.Comparer;

                var oldValue = vdlPoint.GetField2Compare(filedName);
                var newValue = vtaPoint.GetField2Compare(filedName);

                if (compared.AreEqual(oldValue, newValue))
                    continue;

                var oldPartName = vdlPoint.GetField2Compare(_partComparer.PartNameColumns[partNameColumnIndex]);
                var newPartName = vtaPoint.GetField2Compare(_partComparer.PartNameColumns[partNameColumnIndex]);

                //for example, when part is not assigned do not compare Dick1=null with Dicke2==0,00
                if (string.IsNullOrEmpty(oldPartName) || string.IsNullOrEmpty(newPartName))
                    continue;


                outReport = new JdmChangeReportDiffPartPropertie(filedName, oldValue, newValue, oldPartName, new List<string>() { vtaPoint.Name });
                return true;
            }

            outReport = null;
            return false;
        }

        private bool WasDifferentPartAssigned(string partNameColumn, JdmVdlPoint vtaPoint, JdmVdlPoint vdlPoint, out JdmChangeReportDiffPart outReport)
        {
            var oldPart = vdlPoint.GetField2Compare(partNameColumn);
            oldPart = JdmProduct.CleanPartName(oldPart);
            var newPart = vtaPoint.GetField2Compare(partNameColumn);
            newPart = JdmProduct.CleanPartName(newPart);

            if (!string.IsNullOrEmpty(oldPart) && !string.IsNullOrEmpty(newPart))
            {
                if (!oldPart.Equals(newPart))
                {
                    outReport = new JdmChangeReportDiffPart(oldPart, newPart, new List<string>() { vdlPoint.Name });
                    return true;
                }
            }

            //not implemented yet
            outReport = null;
            return false;
        }

        private bool WasPartSwapped(int partNameColumnIndex, JdmVdlPoint vtaPoint, JdmVdlPoint vdlPoint, out JdmChangeReportPartSwapped outReport)
        {
            var newPartNames = vtaPoint.GetFields2Compare(_partComparer.PartNameColumns);

            var oldPart = vdlPoint.GetField2Compare(_partComparer.PartNameColumns[partNameColumnIndex]);
            oldPart = JdmProduct.CleanPartName(oldPart);

            if (newPartNames[partNameColumnIndex] != oldPart && newPartNames.Contains(oldPart))
            {
                var oldPartNames = vdlPoint.GetFields2Compare(_partComparer.PartNameColumns);

                outReport = new JdmChangeReportPartSwapped(oldPart, partNameColumnIndex, newPartNames, vtaPoint.Name, oldPartNames);
                return true;
            }

            //not implemented yet
            outReport = null;
            return false;
        }



        private void GetNewPoints()
        {
            for (var index = _vtaPoints.Count - 1; index >= 0; index--)
            {
                var vtaPoint = _vtaPoints[index];
                var matchingVdlPoint = _vdlPoints.FirstOrDefault(m => m.Name.Equals(vtaPoint.Name));

                if (matchingVdlPoint is null) //means point is new
                {
                    _result.Add(new JdmChangeReportNew(vtaPoint));
                    _vtaPoints.RemoveAt(index);
                }
            }
        }

        private void GetDeletedPoints()
        {
            for (var index = _vdlPoints.Count - 1; index >= 0; index--)
            {
                var vdlPoint = _vdlPoints[index];

                if (vdlPoint.Name.Equals("983.800.702___-025-F9-001-L"))
                {
                }

                var matchingVtaPoint = _vtaPoints.FirstOrDefault(m => m.Name.Equals(vdlPoint.Name));

                if (matchingVtaPoint is null)
                {
                    _result.Add(new JdmChangeReportDeleted(vdlPoint.Name));
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


        private List<IJdmChangeReport> CompareParameters(JdmVdlPoint vtaPoint, JdmVdlPoint matchingVdlPoint)
        {
            var result = new List<IJdmChangeReport>();

            if (vtaPoint.Name.Equals("983.800.702___-025-F9-001-L"))
            {
            }

            for (var index = 1; index < _vdlColumnConfig.Count; index++)
            {
                var columnConfig = _vdlColumnConfig[index];
                if (columnConfig.Comparer is null)
                    continue;

                var fieldName = columnConfig.FieldName;
                var vtaVal = vtaPoint.GetField2Compare(fieldName);
                var vdlVal = matchingVdlPoint.GetField2Compare(fieldName);

                try
                {
                    var areEqual = columnConfig.Comparer.AreEqual(vtaVal, vdlVal);

                    if (!areEqual)
                        result.Add(new JdmChangeReportParamChanged(vtaPoint.Name, fieldName, vdlVal, vtaVal));
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
            var variantVals = vdlPoints.Select(m => m.GetField2Compare(variantName));
            var firstPointVariantColumn = string.Join(";", variantVals);
            return firstPointVariantColumn;
        }
    }
}