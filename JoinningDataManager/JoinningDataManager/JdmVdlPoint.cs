using System.Collections.Generic;

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
        public string GetFields2Compare(string fieldName) => FieldNameVsValue[fieldName];

        public bool HasSameXyz(JdmVdlPoint other)
        {
            var x1 = this.GetFields2Compare(JdmConst.FIELD_NAME_X);
            var y1 = this.GetFields2Compare(JdmConst.FIELD_NAME_Y);
            var z1 = this.GetFields2Compare(JdmConst.FIELD_NAME_Z);

            var x2 = other.GetFields2Compare(JdmConst.FIELD_NAME_X);
            var y2 = other.GetFields2Compare(JdmConst.FIELD_NAME_Y);
            var z2 = other.GetFields2Compare(JdmConst.FIELD_NAME_Z);

            if (x1.Equals(x2) && y1.Equals(y2) && z1.Equals(z2))
                return true;

            return false;
        }
    }
}