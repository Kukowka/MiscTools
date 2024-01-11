using System;
using System.Collections.Generic;
using System.Linq;

namespace JoinningDataManager
{
    public static class JdmExtension
    {
        public static JdmRawVtaPoint Convert2RawPoint(this VTATableElement element, string[] fieldNames)
        {
            var dict = new Dictionary<string, string>();
            var products = new List<JdmProduct>();

            string name = "";

            foreach (var parameter in element.Parameters)
            {
                if (parameter.Name.Equals("Element"))
                    name = parameter.Value;

                if (!fieldNames.Contains(parameter.Name))
                    continue;
                dict.Add(parameter.Name, parameter.Value);
            }

            foreach (VTATableElementPartPartParameter[] productParams in element.Parts)
            {
                var newProduct = productParams.Convert2Product();
                products.Add(newProduct);
            }

            if (dict.Count != fieldNames.Length)
                throw new NotFiniteNumberException();


            return new JdmRawVtaPoint(name, dict, products);
        }


        public static JdmProduct Convert2Product(this VTATableElementPartPartParameter[] productParams)
        {
            var name = productParams.First(m => m.Name.Equals("Part")).Value;
            throw new NotImplementedException();
            //return new JdmProduct(name);
        }

        public static bool EqualWithTolerance(this double val, double other, double tolerance)
        {
            if (Math.Abs(val - other) < tolerance)
                return true;
            return false;
        }

        public static Program.ChangeTypes ConvertFiledName2ChangeType(string fieldName)
        {
            if (fieldName.Equals(JdmConst.FIELD_NAME_NAME))
                throw new ArgumentOutOfRangeException(nameof(fieldName), "Field 'name' should not be compared");
            if (JdmConst.FieldNameVsChangeTypesMapTable.ContainsKey(fieldName))
                return JdmConst.FieldNameVsChangeTypesMapTable[fieldName];

            return Program.ChangeTypes.ParamChanged;
        }

        public static string ToCsvSafe(this string input) => input?.Replace(",", ".").Trim();
    }
}