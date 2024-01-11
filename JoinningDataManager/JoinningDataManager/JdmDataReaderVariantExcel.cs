using GemBox.Spreadsheet;
using System.Collections.Generic;
using System.Linq;

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

            var variantName = usedRange[1, columnIndex].StringValue;
            var assemblies = new List<string>();

            if (variantName.Equals("PO684_4_CUP"))
            {

            }

            for (int rowIndex = 4; rowIndex < usedRange.LastRowIndex; rowIndex++)
            {
                if (usedRange[rowIndex, columnIndex].ValueType == CellValueType.Null)
                    break;

                var assemblyName = usedRange[rowIndex, columnIndex].StringValue;

                if (string.IsNullOrEmpty(assemblyName))
                    continue;

                assemblyName = assemblyName.Trim();

                if (assemblyName.Equals("n.v."))
                    continue;

                assemblies.Add(assemblyName);
            }

            results.Add(new JdmVariantAssembly(variantName, assemblies.ToArray()));
        }


        return results;
    }

    public bool AreReadVariantsOk(List<JdmVariantAssembly> variants)
    {
        foreach (var variant in variants)
        {
            if (JdmConst.VARIANT_NAMES.Contains(variant.VariantName))
                return false;
        }

        return true;
    }
}