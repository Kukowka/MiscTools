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

            int[] partOrder = null;

            foreach (var parameter in element.Parameters)
            {
                if (parameter.Name.Equals("Element"))
                    name = parameter.Value;

                if (parameter.Name.Equals("Fuegepaket"))
                {
                    var splitted = parameter.Value.Split(';');
                    partOrder = splitted.Select(m => int.Parse(m)).ToArray();
                }

                if (!fieldNames.Contains(parameter.Name))
                    continue;

                var cleanVal = CleanParamValue(parameter.Name, parameter.Value);
                dict.Add(parameter.Name, cleanVal);
            }

            var productCopy = new List<JdmProduct>();

            foreach (VTATableElementPartPartParameter[] productParams in element.Parts)
            {
                var newProduct = productParams.Convert2Product();
                productCopy.Add(newProduct);
            }

            foreach (var partId in partOrder)
            {
                var matchingProduct = productCopy.First(m => m.Id.Equals(partId));
                products.Add(matchingProduct);
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
            var idTxt = productParams.First(m => m.Name.Equals("Idx")).Value;
            var id = int.Parse(idTxt);
            return new JdmProduct(name, id);
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