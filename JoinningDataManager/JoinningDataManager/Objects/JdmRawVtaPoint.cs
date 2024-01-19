using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JoinningDataManager
{
    public class JdmRawVtaPoint : IJdmComparable
    {
        public string Name { get; }

        public Dictionary<string, string> FieldNameVsValue { get; }

        public List<JdmProduct> Products { get; }

        public List<string> UsedVariants { get; } = new List<string>();

        public JdmRawVtaPoint(string pointName, Dictionary<string, string> fieldNameVsValue, List<JdmProduct> products) : this(pointName, fieldNameVsValue)
        {
            if (products.Count == 0)
                throw new InvalidDataException();

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

        public string GetField2Compare(string fieldName)
        {
            if (JdmConst.VARIANT_NAMES.Contains(fieldName))
            {
                if (UsedVariants.Contains(fieldName))
                    return "X";

                return "";
            }

            if (fieldName.Equals(JdmConst.FIELD_NAME_NAME))
                return Name;

            if (fieldName.Equals(JdmConst.FIELD_NAME_MATERIAL1))
                return FieldNameVsValue[JdmConst.FIELD_NAME_INFO_WERKSTOFF1];

            if (fieldName.Equals(JdmConst.FIELD_NAME_MATERIAL2))
                return FieldNameVsValue[JdmConst.FIELD_NAME_INFO_WERKSTOFF2];

            if (fieldName.Equals(JdmConst.FIELD_NAME_MATERIAL3))
                return FieldNameVsValue[JdmConst.FIELD_NAME_INFO_WERKSTOFF3];

            if (fieldName.Equals(JdmConst.FIELD_NAME_MATERIAL4))
                return FieldNameVsValue[JdmConst.FIELD_NAME_INFO_WERKSTOFF4];

            switch (fieldName)
            {
                case JdmConst.FIELD_NAME_BAUTEIL1:
                    return Products[0].Name;
                case JdmConst.FIELD_NAME_BAUTEIL2:
                    if (Products.Count > 1)
                        return Products[1].Name;
                    return "";
                case JdmConst.FIELD_NAME_BAUTEIL3:
                    if (Products.Count > 2)
                        return Products[2].Name;
                    return "";

                case JdmConst.FIELD_NAME_BAUTEIL4:
                    if (Products.Count > 3)
                        return Products[3].Name;
                    return "";


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
                            return FieldNameVsValue[JdmConst.FIELD_NAME_START_Z];
                        break;
                    }
            }

            return FieldNameVsValue[fieldName];
        }

        //public string GetField(string fieldName)
        //{
        //    if (JdmConst.VARIANT_NAMES.Contains(fieldName))
        //    {
        //        if (UsedVariants.Contains(fieldName))
        //            return "X";
        //    }

        //    if (fieldName.Equals(JdmConst.FIELD_NAME_NAME))
        //        return Name;

        //    switch (fieldName)
        //    {
        //        case JdmConst.FIELD_NAME_BAUTEIL1:
        //            return Products[0].Name;

        //        case JdmConst.FIELD_NAME_BAUTEIL2:
        //            if (Products.Count > 1)
        //                return Products[1].Name;
        //            return "";
        //        case JdmConst.FIELD_NAME_BAUTEIL3:
        //            if (Products.Count > 2)
        //                return Products[2].Name;
        //            return "";

        //        case JdmConst.FIELD_NAME_BAUTEIL4:
        //            if (Products.Count > 3)
        //                return Products[3].Name;
        //            return "";
        //    }

        //    return FieldNameVsValue[fieldName];
        //}

        public bool HasTheSameFields(Dictionary<string, string> otherFieldNameVsValue, out string differentFieldName)
        {
            if (otherFieldNameVsValue.Count != this.FieldNameVsValue.Count)
                throw new InvalidDataException();

            for (int i = 0; i < FieldNameVsValue.Count; i++)
            {
                differentFieldName = FieldNameVsValue.Keys.ElementAt(i);
                if (!this.FieldNameVsValue.Keys.ElementAt(i).Equals(otherFieldNameVsValue.Keys.ElementAt(i)))
                    return false;

                if (!this.FieldNameVsValue.Values.ElementAt(i).Equals(otherFieldNameVsValue.Values.ElementAt(i)))
                    return false;
            }

            differentFieldName = null;
            return true;
        }

        public void AddNewVariants(string[] variants)
        {
            foreach (var variant in variants)
            {
                if (UsedVariants.Contains(variant))
                    continue;

                UsedVariants.Add(variant);
            }
        }
    }
}