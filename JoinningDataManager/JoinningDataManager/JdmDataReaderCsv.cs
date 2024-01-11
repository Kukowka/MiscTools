using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace JoinningDataManager;

public class JdmDataReaderCsv
{
    private static int LAST_FIELD_COLUMN_INDEX = 65; // index of title with name: "Sicherheitsklasse_Kleben"
    private static string VW_ASSEMBLY_REGEX = @"[A-Z0-9]{3}\.[A-Z0-9]{3}\.[A-Z0-9]{3}\.[A-Z]*";

    public bool HaveVtaPointsUniqueNames(List<JdmRawVtaPoint> vtaPoints)
    {
        var grp = vtaPoints.GroupBy(m => m.Name);

        if (grp.Any(m => m.Count() > 1))
        {
            var notUniqueNames = grp.Where(m => m.Count() > 1).Select(m => m.Key).ToList();

            return false;
        }

        return true;
    }
    public List<JdmRawVtaPoint> ExtractVtaPointsFromCsvs(string vtaCsvsPath, string[] fieldNames, List<JdmVariantAssembly> allVariants)
    {
        var allCsvsInDir = Directory.GetFiles(vtaCsvsPath, "*.csv", SearchOption.AllDirectories);
        var allPoints = new List<JdmRawVtaPoint>();

        foreach (var csvPath in allCsvsInDir)
        {
            var points = ReadVtaCsv(csvPath, fieldNames);
            foreach (var point in points)
            {
                if (point.Name.Equals("992.800.702___-003-F8-001-L"))
                {

                }

                var variants = GetVariantFromCsvName(csvPath, allVariants);
                var theSamePoint = allPoints.FirstOrDefault(m => m.Name.Equals(point.Name));

                if (theSamePoint is not null)
                {
                    if (!theSamePoint.HasTheSameFields(point.FieldNameVsValue, out var differentFieldName))
                        throw new InvalidCastException();

                    theSamePoint.AddNewVariants(variants);
                    continue;
                }

                point.UsedVariants.AddRange(variants);
                allPoints.Add(point);
            }
        }

        return allPoints;
    }

    private string[] GetVariantFromCsvName(string vtaCsvsPath, List<JdmVariantAssembly> variantWithAssemblies)
    {
        var fileName = Path.GetFileNameWithoutExtension(vtaCsvsPath);
        var assemblyName = Regex.Match(fileName, VW_ASSEMBLY_REGEX).Value;

        if (assemblyName.Equals("992.800.702.AB"))
        {

        }

        var variants = variantWithAssemblies.Where(m => m.ContainsAssembly(assemblyName)).Select(m => m.VariantName).ToArray();

        if (variants.Length == 0)
            throw new InvalidCastException();

        return variants;
    }

    private static List<JdmRawVtaPoint> ReadVtaCsv(string filePath, string[] fieldNames)
    {
        var allLines = File.ReadAllLines(filePath).ToList();

        var products = ReadProductsFromCsv(allLines);

        if (products.Count == 0)
            throw new InvalidDataException();

        var points = ReadPointsFromCsv(allLines, fieldNames, products);

        if (points.Count == 0)
            throw new InvalidDataException();

        return points;
    }

    private static List<JdmProduct> ReadProductsFromCsv(List<string> allLines)
    {
        var firstLine = allLines.First(m => m.StartsWith("\"Parts\""));
        var firstLineIndex = allLines.IndexOf(firstLine);

        var lastLine = allLines.First(m => m.StartsWith("\"Elements\""));
        var lastLineIndex = allLines.IndexOf(lastLine);

        var products = new List<JdmProduct>();

        for (int i = firstLineIndex + 2; i < lastLineIndex; i++)
        {
            var line = allLines[i];

            if (string.IsNullOrEmpty(line))
                break;

            var product = GetProductFromOneCsvRow(line);
            products.Add(product);
        }


        return products;
    }

    private static JdmProduct GetProductFromOneCsvRow(string line)
    {
        var splitted = line.Split(';').ToList();

        var name = RemoveDoubleQuoteFromStartAndEnd(splitted[1]);
        var thickness = RemoveDoubleQuoteFromStartAndEnd(splitted[2]);
        var idText = RemoveDoubleQuoteFromStartAndEnd(splitted[3]);

        var id = int.Parse(idText);

        return new JdmProduct(name, thickness, id);
    }

    private static List<JdmRawVtaPoint> ReadPointsFromCsv(List<string> allLines, string[] fieldNames, List<JdmProduct> products)
    {
        var firstLine = allLines.First(m => m.StartsWith("\"Elements\""));
        var firstLineIndex = allLines.IndexOf(firstLine);

        var lastLine = allLines.First(m => m.Equals("\"Support Points\""));
        var lastLineIndex = allLines.IndexOf(lastLine);

        if (!IsCsvTitleLineOk(allLines[firstLineIndex + 1], out var titlesList))
            throw new InvalidCastException();

        var result = new List<JdmRawVtaPoint>();

        for (int i = firstLineIndex + 2; i < lastLineIndex; i++)
        {
            var line = allLines[i];

            if (string.IsNullOrEmpty(line))
                break;

            var point = GetPointFromOneCsvRow(fieldNames, line, titlesList, products);
            result.Add(point);
        }

        return result;
    }

    private static JdmRawVtaPoint GetPointFromOneCsvRow(string[] fieldNames, string line, List<string> titlesList, List<JdmProduct> products)
    {
        var rowSplitted = line.Split(new[] { "\";" }, StringSplitOptions.RemoveEmptyEntries).ToList();

        var pointNameRowIndex = PointNameRowIndexByTitleName(titlesList, JdmConst.FIELD_NAME_ELEMENT);
        var pointName = RemoveDoubleQuoteFromStartAndEnd(rowSplitted[pointNameRowIndex]);

        var fields = new Dictionary<string, string>();
        foreach (var fieldName in fieldNames)
        {
            var fieldIndex = PointNameRowIndexByTitleName(titlesList, fieldName);
            fields.Add(fieldName, RemoveDoubleQuoteFromStartAndEnd(rowSplitted[fieldIndex]));
        }

        var usedProductIndexes = GetUsedProductIndexes(rowSplitted);
        var uedProducts = products.Where(m => usedProductIndexes.Contains(m.Id)).ToList();

        if (uedProducts.Count == 0)
            throw new InvalidDataException();

        return new JdmRawVtaPoint(pointName, fields, uedProducts);
    }

    private static List<int> GetUsedProductIndexes(List<string> rowSplitted)
    {
        var result = new List<int>();
        for (int i = LAST_FIELD_COLUMN_INDEX + 2; i < rowSplitted.Count; i++)
        {
            var val = rowSplitted[i].Trim();
            if (val.Equals("\"x", StringComparison.InvariantCultureIgnoreCase))
            {
                var id = i - (LAST_FIELD_COLUMN_INDEX + 1);
                result.Add(id);
            }
        }

        return result;
    }

    private static string RemoveDoubleQuoteFromStartAndEnd(string input)
    {
        if (input.StartsWith("\""))
            input = input.Substring(1);

        if (input.EndsWith("\""))
            input = input.Substring(0, input.Length - 1);

        return input;
    }

    private static int PointNameRowIndexByTitleName(List<string> titlesList, string title) => titlesList.IndexOf("\"" + title + "\"");


    private static bool IsCsvTitleLineOk(string titleLine, out List<string> titlesList)
    {
        titlesList = null;

        if (!titleLine.StartsWith("\"Element\""))
            return false;

        var splitted = titleLine.Split(';').ToList();

        var lastTitleIndex = splitted.IndexOf("\"Sicherheitsklasse_Kleben\"");

        if (lastTitleIndex != LAST_FIELD_COLUMN_INDEX)
            return false;

        titlesList = splitted.GetRange(0, lastTitleIndex - 1);

        return true;
    }

}