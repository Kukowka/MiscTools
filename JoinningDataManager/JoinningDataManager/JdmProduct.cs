namespace JoinningDataManager
{
    public class JdmProduct
    {
        public JdmProduct(string name)
        {
            Name = name;
        }

        public string Name { get; } 

        public string Dicke { get; set; }

        public string Material { get; set; }

        public string Oberflache { get; set; }
    }
}