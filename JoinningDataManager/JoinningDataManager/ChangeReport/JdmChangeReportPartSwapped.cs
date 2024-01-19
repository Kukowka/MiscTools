namespace JoinningDataManager.ChangeReport;

public class JdmChangeReportPartSwapped : IJdmChangeReport
{
    public JdmChangeReportPartSwapped(string swappedPartName, int oldPartIndex, string[] newPartNames, string pointName, string[] oldPartNames)
    {
        SwappedPartName = swappedPartName;
        OldPartIndex = oldPartIndex;
        NewPartNames = newPartNames;
        PointName = pointName;
        OldPartNames = oldPartNames;
    }

    public string PointName { get; }
    public string SwappedPartName { get; }

    public int OldPartIndex { get; }

    public string[] NewPartNames { get; }
    public string[] OldPartNames { get; }

}