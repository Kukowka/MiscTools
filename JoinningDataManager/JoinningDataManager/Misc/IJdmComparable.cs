namespace JoinningDataManager
{
    public interface IJdmComparable
    {
        public string GetField2Compare(string fieldName);
        string Name { get; }
    }
}