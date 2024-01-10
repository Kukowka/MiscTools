using JoinningDataManager.Comparers;

namespace JoinningDataManager
{
    public class JdmColumnConfig
    {
        public JdmColumnConfig(string fieldName, string vdlColumnName, IJdmComparer comparer)
        {
            FieldName = fieldName;
            VdlColumnName = vdlColumnName;
            Comparer = comparer;
        }

        public string FieldName { get; } 
        public string VdlColumnName { get; }
        public IJdmComparer  Comparer { get; }
    }
}