using System.Collections.Generic;
using System.Linq;

namespace JoinningDataManager.ChangeReport;

public class JdmChangeReportDiffPartPropertie : IJdmChangeReport
{



    public JdmChangeReportDiffPartPropertie(string propertieName, string oldValue, string newValue, string partName, List<string> affectedPoints)
    {
        PropertieName = propertieName;
        OldValue = oldValue;
        NewValue = newValue;
        PartName = partName;
        AffectedPoints = affectedPoints;
        AffectedPoints.Sort();
    }

    public string PropertieName { get; }
    public string OldValue { get; }
    public string NewValue { get; }
    public string PartName { get; }

    public List<string> AffectedPoints { get; }

    protected bool Equals(JdmChangeReportDiffPartPropertie other)
    {
        return PropertieName == other.PropertieName && OldValue == other.OldValue && NewValue == other.NewValue && PartName == other.PartName && (AffectedPoints.SequenceEqual(other.AffectedPoints));
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((JdmChangeReportDiffPartPropertie)obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = (PropertieName != null ? PropertieName.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (OldValue != null ? OldValue.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (NewValue != null ? NewValue.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (PartName != null ? PartName.GetHashCode() : 0);

            foreach (var foo in AffectedPoints)
                hashCode = hashCode * 31 + foo.GetHashCode();

            return hashCode;
        }
    }


}