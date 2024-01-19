using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using static JoinningDataManager.JdmConst;

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

                var cleanVal = CleanParamValue(parameter.Name, parameter.Value);
                dict.Add(parameter.Name, cleanVal);
            }

            if(name.Equals("983.800.701___-015-E2-002-L"))
            {
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

        private static string CleanParamValue(string parameterName, string parameterValue)
        {
            if (parameterValue is null)
                return null;

            if (parameterName.Equals(FIELD_NAME_DICKE1) || parameterName.Equals(FIELD_NAME_DICKE2) || parameterName.Equals(FIELD_NAME_DICKE3) || parameterName.Equals(FIELD_NAME_DICKE4))
                return parameterValue.Replace("mm", "");


            return parameterValue;
        }




        public static JdmProduct Convert2Product(this VTATableElementPartPartParameter[] productParams)
        {
            var name = productParams.First(m => m.Name.Equals("Part")).Value;
            return new JdmProduct(name);
        }

        public static bool EqualWithTolerance(this double val, double other, double tolerance)
        {
            if (Math.Abs(val - other) <= tolerance)
                return true;
            return false;
        }

        public static bool ImprovedDoubleTryParse(this string inVal, out double outVal) => double.TryParse(inVal.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out outVal);


        public static string ToCsvSafe(this string input) => input?.Replace(",", ".").Trim();
    }
}