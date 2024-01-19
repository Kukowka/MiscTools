namespace JoinningDataManager.ChangeReport;

public class JdmChangeReportParamChanged : IJdmChangeReport
{


    public string Name { get; }
    public string ParamName { get; }
    public string OldValue { get; }
    public string NewValue { get; }

    public JdmChangeReportParamChanged(string pointName, string paramName, string oldValue, string newValue)
    {
        Name = pointName;
        ParamName = paramName;
        OldValue = oldValue;
        NewValue = newValue;
    }

    protected bool Equals(JdmChangeReportParamChanged other)
    {
        return Name == other.Name && ParamName == other.ParamName && OldValue == other.OldValue && NewValue == other.NewValue;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((JdmChangeReportParamChanged)obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = (Name != null ? Name.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (ParamName != null ? ParamName.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (OldValue != null ? OldValue.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (NewValue != null ? NewValue.GetHashCode() : 0);
            return hashCode;
        }
    }
}