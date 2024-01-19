//using System;

//namespace JoinningDataManager
//{
//    public class JdmCompareReport
//    {
//        public JdmCompareReport(Program.ChangeTypes changeType, string pointName, string paramName, string oldValue, string newValue)
//        {
//            ChangeType = changeType;
//            Name = pointName;
//            ParamName = paramName;
//            OldValue = oldValue;
//            NewValue = newValue;
//        }

//        public JdmCompareReport(string pointName, JdmVdlPoint newPoint)
//        {
//            ChangeType = Program.ChangeTypes.New;
//            this.Name = pointName;
//            NewPoint = newPoint;
//        }

//        public JdmCompareReport(string pointName, Program.ChangeTypes changeType)
//        {
//            ChangeType = changeType;
//            this.Name = pointName;
//        }

//        public Program.ChangeTypes ChangeType { get; }
//        public string Name { get; }
//        public string ParamName { get; }
//        public string OldValue { get; }
//        public string NewValue { get; }

//        public JdmVdlPoint NewPoint { get; }

//        public override string ToString() => $"{Name.ToCsvSafe()},{ParamName.ToCsvSafe()},{OldValue.ToCsvSafe()},{NewValue.ToCsvSafe()}";


//    }
//}