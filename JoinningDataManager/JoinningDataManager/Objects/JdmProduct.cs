namespace JoinningDataManager
{
    public class JdmProduct
    {
        public JdmProduct(string name, int id = -1)
        {
            Name = name;
            Id = id;
        }

        public JdmProduct(string name, string dicke, int id) : this(name, id)
        {
            Dicke = dicke;
        }

        public string Name { get; }

        public string Dicke { get; set; }

        public string Material { get; set; }

        public string Oberflache { get; set; }

        public int Id { get; }
    }
}