using System.Text.RegularExpressions;

namespace JoinningDataManager
{
    public class JdmProduct
    {
        public JdmProduct(string name, int id = -1)
        {
            Name = CleanPartName(name);
            Id = id;
        }

        public JdmProduct(string name, string dicke, int id) : this(name, id)
        {
            Dicke = dicke;
        }


        public static string CleanPartName(string partName)
        {
            if (partName == null)
                return null;

            var cleanPart = Regex.Match(partName, JdmDataReaderCsv.VW_ASSEMBLY_REGEX).Value;
            return cleanPart;
        }

        public string Name { get; }

        public string Dicke { get; set; }

        public string Material { get; set; }

        public string Oberflache { get; set; }

        public int Id { get; }
    }
}