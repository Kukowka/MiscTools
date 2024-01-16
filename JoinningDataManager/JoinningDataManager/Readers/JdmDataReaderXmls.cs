using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace JoinningDataManager
{
    public class JdmDataReaderXmls
    {
        public List<JdmRawVtaPoint> ExtractVtaPointsFromXmls(string vtaXmlsParentDir, string[] fieldNames, JdmVariantAssembly variant)
        {
            var allXmlsInDir = Directory.GetFiles(vtaXmlsParentDir, "*.xml", SearchOption.AllDirectories);
            var allPoints = new List<JdmRawVtaPoint>();

            foreach (var xmlPath in allXmlsInDir)
            {
                var fileName = Path.GetFileNameWithoutExtension(xmlPath);

                if (fileName.Equals("983.831.051___VER_002.0_20230424"))
                {

                }

                var assemblyNr = Regex.Match(fileName, JdmVariantAssembly.ASSEMBLY_NR_REG_EX).Value;

                if (!variant.ContainsAssembly(assemblyNr))
                    continue;

                var points = ReadVtaXml(xmlPath, fieldNames);
                allPoints.AddRange(points);
            }

            return allPoints;
        }

        private static List<JdmRawVtaPoint> ReadVtaXml(string filePath, string[] fieldNames)
        {
            var mySerializer = new XmlSerializer(typeof(VTATable));
            using var fs = new FileStream(filePath, FileMode.Open);
            var vtaTable = mySerializer.Deserialize(fs) as VTATable;

            var points = new List<JdmRawVtaPoint>();

            foreach (VTATableElement element in vtaTable.VTAElements)
            {
                var newPoint = element.Convert2RawPoint(fieldNames);
                points.Add(newPoint);
            }

            return points;
        }

    }
}

