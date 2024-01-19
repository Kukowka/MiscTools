using System.Collections.Generic;
using System.Linq;

namespace JoinningDataManager
{
    public class JdmVdlPoint : IJdmComparable
    {
        public JdmVdlPoint(string name, Dictionary<string, string> fieldNameVsValue)
        {
            Name = name;
            FieldNameVsValue = fieldNameVsValue;
        }

        public string Name { get; }
        public Dictionary<string, string> FieldNameVsValue { get; }
        public string GetField2Compare(string fieldName)
        {
            if (fieldName.Equals(JdmConst.FIELD_NAME_NAME))
                return Name;

            return FieldNameVsValue[fieldName];
        }

        public string[] GetFields2Compare(string[] fieldNames) => fieldNames.Select(m => GetField2Compare(m)).ToArray();

        public bool HasSameXyz(JdmVdlPoint other)
        {
            var x1 = this.GetField2Compare(JdmConst.FIELD_NAME_X);
            var y1 = this.GetField2Compare(JdmConst.FIELD_NAME_Y);
            var z1 = this.GetField2Compare(JdmConst.FIELD_NAME_Z);

            var x2 = other.GetField2Compare(JdmConst.FIELD_NAME_X);
            var y2 = other.GetField2Compare(JdmConst.FIELD_NAME_Y);
            var z2 = other.GetField2Compare(JdmConst.FIELD_NAME_Z);

            if (x1.Equals(x2) && y1.Equals(y2) && z1.Equals(z2))
                return true;

            return false;
        }
    }
}