using GemBox.Spreadsheet;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace JoinningDataManager;

public class JdmDataReaderVariantExcel
{

    public List<JdmVariantAssembly> ExtractVariants(string variantExcelPath)
    {
        var vdlExcel = ExcelFile.Load(variantExcelPath);

        var usedRange = vdlExcel.Worksheets["Aktuelle_Fügestufen"].GetUsedCellRange(true);
        var results = new List<JdmVariantAssembly>();

        for (int columnIndex = 3; columnIndex < usedRange.LastColumnIndex; columnIndex++)
        {
            if (usedRange[1, columnIndex].ValueType == CellValueType.Null)
                break;

            var columnLetter = ExcelColumnCollection.ColumnIndexToName(columnIndex);
            var variantName = usedRange[1, columnIndex].StringValue;
            var assemblies = new List<string>();

            for (int rowIndex = 4; rowIndex <= usedRange.LastRowIndex; rowIndex++)
            {
                if (usedRange[rowIndex, columnIndex].ValueType == CellValueType.Null)
                    continue;

                var assemblyName = usedRange[rowIndex, columnIndex].StringValue;

                if (string.IsNullOrEmpty(assemblyName))
                    continue;

                assemblyName = assemblyName.Trim();



                if (assemblyName.Equals("n.v."))
                    continue;

                var assemblyNr = Regex.Match(assemblyName, JdmVariantAssembly.ASSEMBLY_NR_REG_EX).Value;
                assemblies.Add(assemblyNr);
            }

            results.Add(new JdmVariantAssembly(variantName, assemblies));
        }


        return results;
    }

    public bool AreReadVariantsOk(List<JdmVariantAssembly> variants)
    {
        foreach (var variant in variants)
        {
            if (!IsVariantOk(variant)) return false;
        }

        return true;
    }

    public static bool IsVariantOk(JdmVariantAssembly variant)
    {
        if (!JdmConst.VARIANT_NAMES.Contains(variant.VariantName))
            return false;
        return true;
    }
}