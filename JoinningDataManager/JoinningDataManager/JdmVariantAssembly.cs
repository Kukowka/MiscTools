using System.IO;
using System.Text.RegularExpressions;

namespace JoinningDataManager;

public class JdmVariantAssembly
{
    public JdmVariantAssembly(string variantName, string[] assemblies)
    {
        VariantName = variantName;
        Assemblies = assemblies;
    }

    public string VariantName { get; }

    public string[] Assemblies { get; }


    public bool ContainsAssembly(string otherAssemblyName)
    {
        if (otherAssemblyName.Equals("992.800.702.AA")) //this is exception which is contains in all variants
            return true;

        if (VariantName.Equals("PO684_4_CUP"))
        {
        }

        var otherAssemblyNr = GetAssemblyNrAndLetter(otherAssemblyName, out var otherAssemblyLetter);

        foreach (var assembly in Assemblies)
        {
            var assemblyNr = GetAssemblyNrAndLetter(assembly, out var assemblyLetter);

            if (assemblyNr == otherAssemblyNr)
            {
                if (string.IsNullOrEmpty(assemblyLetter))
                    return true;

                if (assemblyLetter.Equals(otherAssemblyLetter))
                    return true;
            }
        }

        return false;
    }

    private string GetAssemblyNrAndLetter(string assemblyName, out string assemblyLetter)
    {
        var assemblyNr = assemblyName.Substring(0, 11);

        if (!Regex.IsMatch(assemblyNr, @"[A-Z0-9]{3}\.[A-Z0-9]{3}\.[A-Z0-9]{3}"))
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
}