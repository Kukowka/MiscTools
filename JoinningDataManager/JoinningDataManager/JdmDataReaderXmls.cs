using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using JoinningDataManager;

namespace JoinningDataManager
{
    public class JdmDataReaderXmls
    {
        public List<JdmRawVtaPoint> ExtractVtaPointsFromXmls(string vtaXmlsPath, string[] fieldNames)
        {
            var allXmlsInDir = Directory.GetFiles(vtaXmlsPath, "*.xml", SearchOption.AllDirectories);
            var allPoints = new List<JdmRawVtaPoint>();

            foreach (var xmlPath in allXmlsInDir)
            {
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

