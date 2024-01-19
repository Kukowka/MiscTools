namespace JoinningDataManager.ChangeReport;

public class JdmChangeReportDeleted : IJdmChangeReport
{
    public JdmChangeReportDeleted(string pointName)
    {
        PointName = pointName;
    }

    public string PointName { get; }
}