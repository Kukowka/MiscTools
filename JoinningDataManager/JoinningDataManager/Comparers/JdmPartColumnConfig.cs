namespace JoinningDataManager.Comparers;

public class JdmPartColumnConfig
{
    public string[] PartNameColumns { get; }
    public JdmPartColumnConfigUnit[] PartPropertieUnits { get; }

    public JdmPartColumnConfig(string[] partNameColumns, JdmPartColumnConfigUnit[] partPropertieUnits)
    {
        PartNameColumns = partNameColumns;
        PartPropertieUnits = partPropertieUnits;
    }

}