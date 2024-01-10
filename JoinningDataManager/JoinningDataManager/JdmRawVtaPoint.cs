using System.Collections.Generic;
using System.Linq;

namespace JoinningDataManager
{
    public class JdmRawVtaPoint : IJdmComparable
    {
        public string Name { get; }

        public Dictionary<string, string> FieldNameVsValue { get; }

        public List<JdmProduct> Products { get; }

        public JdmRawVtaPoint(string pointName, Dictionary<string, string> fieldNameVsValue, List<JdmProduct> products) : this(pointName, fieldNameVsValue)
        {
            Products = products;
        }

        public JdmRawVtaPoint(string pointName, Dictionary<string, string> fieldNameVsValue)
        {
            Name = pointName;
            FieldNameVsValue = fieldNameVsValue;
        }

        public bool IsContinuesPoint()
        {
            var process = FieldNameVsValue[JdmConst.FIELD_NAME_PROCESS];

            if (JdmConst.ContinuesProcessTypes.Contains(process))
                return true;

            return false;
        }

        public string GetFields2Compare(string fieldName)
        {
            switch (fieldName)
            {
                case JdmConst.FIELD_NAME_BAUTEIL1:
                    return null;
                case JdmConst.FIELD_NAME_DICKE1:
                    return null;
                case JdmConst.FIELD_NAME_MATERIAL1:
                    return null;
                case JdmConst.FIELD_NAME_OBERFLÄCHE1:
                    return null;
                case JdmConst.FIELD_NAME_STRECKGRENZE1:
                    return null;
                case JdmConst.FIELD_NAME_BAUTEIL2:
                    return null;
                case JdmConst.FIELD_NAME_DICKE2:
                    return null;
                case JdmConst.FIELD_NAME_MATERIAL2:
                    return null;
                case JdmConst.FIELD_NAME_OBERFLAECHE2:
                    return null;
                case JdmConst.FIELD_NAME_STRECKGRENZE2:
                    return null;
                case JdmConst.FIELD_NAME_BAUTEIL3:
                    return null;
                case JdmConst.FIELD_NAME_DICKE3:
                    return null;
                case JdmConst.FIELD_NAME_MATERIAL3:
                    return null;
                case JdmConst.FIELD_NAME_OBERFLAECHE3:
                    return null;
                case JdmConst.FIELD_NAME_STRECKGRENZE3:
                    return null;
                case JdmConst.FIELD_NAME_BAUTEIL4:
                    return null;
                case JdmConst.FIELD_NAME_DICKE4:
                    return null;
                case JdmConst.FIELD_NAME_MATERIAL4:
                    return null;
                case JdmConst.FIELD_NAME_OBERFLAECHE4:
                    return null;


                case JdmConst.FIELD_NAME_X:
                    {
                        if (IsContinuesPoint())
                            return FieldNameVsValue[JdmConst.FIELD_NAME_START_X];
                        break;
                    }
                case JdmConst.FIELD_NAME_Y:
                    {
                        if (IsContinuesPoint())
                            return FieldNameVsValue[JdmConst.FIELD_NAME_START_Y];
                        break;
                    }
                case JdmConst.FIELD_NAME_Z:
                    {
                        if (IsContinuesPoint())
                            return FieldNameVsValue[JdmConst.FIELD_NAME_START_Y];
                        break;
                    }
            }

            return FieldNameVsValue[fieldName];

        }

        public bool HasTheSameFields(Dictionary<string, string> otherFieldNameVsValue)
        {
            if (otherFieldNameVsValue.Count != this.FieldNameVsValue.Count)
                return false;

            for (int i = 0; i < FieldNameVsValue.Count; i++)
            {
                if (!this.FieldNameVsValue.Keys.ElementAt(i).Equals(otherFieldNameVsValue.Keys.ElementAt(i)))
                    return false;

                if (!this.FieldNameVsValue.Values.ElementAt(i).Equals(otherFieldNameVsValue.Values.ElementAt(i)))
                    return false;
            }

            return true;
        }
    }
}