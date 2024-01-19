namespace JoinningDataManager.Comparers;

public class JdmPartColumnConfigUnit
{
    public JdmPartColumnConfigUnit(IJdmComparer comparer, string[] partPropertieNames)
    {
        Comparer = comparer;
        PartPropertieNames = partPropertieNames;
    }

    public IJdmComparer Comparer { get; }

    public string[] PartPropertieNames { get; }



}