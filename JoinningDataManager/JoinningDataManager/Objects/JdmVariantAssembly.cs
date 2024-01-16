using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace JoinningDataManager;

public class JdmVariantAssembly
{
    public const string ASSEMBLY_NR_REG_EX = @"[A-Z0-9]{3}\.[A-Z0-9]{3}\.[A-Z0-9]{3}";

    public JdmVariantAssembly(string variantName, List<string> assemblies)
    {
        VariantName = variantName;
        Assemblies = assemblies;
    }

    public string VariantName { get; }

    public List<string> Assemblies { get; }


    public bool ContainsAssembly(string otherAssemblyName)
    {
        var otherAssemblyNr = GetAssemblyNrAndLetter(otherAssemblyName, out _);

        foreach (var assembly in Assemblies)
        {
            var assemblyNr = GetAssemblyNrAndLetter(assembly, out _);

            if (assemblyNr == otherAssemblyNr)
                return true;
        }

        return false;
    }

    public static string GetAssemblyNrAndLetter(string assemblyName, out string assemblyLetter)
    {
        var assemblyNr = assemblyName.Substring(0, 11);

        if (!Regex.IsMatch(assemblyNr, ASSEMBLY_NR_REG_EX))
            throw new InvalidDataException();

        if (assemblyName.Length == 11)
        {
            assemblyLetter = null;
            return assemblyNr;
        }

        assemblyLetter = assemblyName.Substring(12, assemblyName.Length - 12);

        if (assemblyLetter.Equals("**"))
            assemblyLetter = null;

        return assemblyNr;
    }

    public bool IsPointOfVariant(string pointName)
    {
        var result = ContainsAssembly(pointName);
        return result;
    }

    public void RemoveUnusedAssemblies()
    {
        if (!VariantName.Equals(JdmConst.VARIANT_NAME_PO455_LL))
            throw new NotImplementedException();

        Assemblies.Remove("992.803.092");
        Assemblies.Remove("983.899.045.A");
        Assemblies.Remove("983.831.051");
    }
}