namespace JoinningDataManager.ChangeReport;

public class JdmChangeReportNew : IJdmChangeReport
{
    public JdmChangeReportNew(JdmVdlPoint newPoint)
    {
        NewPoint = newPoint;
    }

    public JdmVdlPoint NewPoint { get; }
}