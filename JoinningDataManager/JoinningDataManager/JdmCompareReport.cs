﻿using System;
using System.Collections.Generic;

namespace JoinningDataManager
{
    public class JdmCompareReport
    {
        public static string TO_STRING_PARAM_WITH_NAME = "WITH_POINT_NAME";
        public static string TO_STRING_PARAM_WITH_CHANGE_TYPE = "WITH_POINT_NAME";

        public JdmCompareReport(Program.ChangeTypes changeType, string pointName, string paramName, string oldValue, string newValue)
        {
            ChangeType = changeType;
            Name = pointName;
            ParamName = paramName;
            OldValue = oldValue;
            NewValue = newValue;
        }

        public JdmCompareReport(string PointName)
        {
            ChangeType = Program.ChangeTypes.New;
            this.Name = PointName;
        }

        public Program.ChangeTypes ChangeType { get; }
        public string Name { get; }
        public string ParamName { get; }
        public string OldValue { get; }
        public string NewValue { get; }

        public override string ToString() => $"{Name.ToCsvSafe()},{ParamName.ToCsvSafe()},{OldValue.ToCsvSafe()},{NewValue.ToCsvSafe()}";

        public string ToString(string param)
        {
            if (param.Equals(TO_STRING_PARAM_WITH_NAME))
                return this.ToString();
            if (param.Equals(TO_STRING_PARAM_WITH_CHANGE_TYPE))
                return $"{ChangeType},{ParamName.ToCsvSafe()},{OldValue.ToCsvSafe()},{NewValue.ToCsvSafe()}";

            throw new ArgumentOutOfRangeException(nameof(param));
        }
    }
}