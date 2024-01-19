using System.Collections.Generic;
using System.Linq;

namespace JoinningDataManager.ChangeReport;

public class JdmChangeReportDiffPart : IJdmChangeReport
{
    public JdmChangeReportDiffPart(string oldPartName, string newPartName, List<string> affectedPointNames)
    {
        OldPartName = oldPartName;
        NewPartName = newPartName;
        AffectedPointNames = affectedPointNames;
        AffectedPointNames.Sort();
    }

    public string OldPartName { get; }
    public string NewPartName { get; }
    public List<string> AffectedPointNames { get; }

    protected bool Equals(JdmChangeReportDiffPart other)
    {
        return OldPartName == other.OldPartName && NewPartName == other.NewPartName && AffectedPointNames.SequenceEqual(other.AffectedPointNames);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((JdmChangeReportDiffPart)obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = (OldPartName != null ? OldPartName.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (NewPartName != null ? NewPartName.GetHashCode() : 0);
            foreach (var foo in AffectedPointNames)
                hashCode = hashCode * 31 + foo.GetHashCode();
            return hashCode;
        }
    }
}