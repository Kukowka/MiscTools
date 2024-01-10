using System;
using System.Collections.Generic;
using System.Linq;

namespace JoinningDataManager
{
    public class JdmReportFormatter
    {
        public string GetReportsGroupedByType(List<JdmCompareReport> allReport)
        {
            var result = "ChangeType,PointName,ParamName,OldValue,NewValue" + Environment.NewLine;

            var groupedByType = allReport.GroupBy(m => m.ChangeType);

            foreach (var typeGrp in groupedByType)
            {
                result += typeGrp.Key.ToString() + Environment.NewLine;
                foreach (var report in typeGrp)
                    result += "," + report.ToString(JdmCompareReport.TO_STRING_PARAM_WITH_NAME) + Environment.NewLine;
            }

            return result;
        }
    }
}