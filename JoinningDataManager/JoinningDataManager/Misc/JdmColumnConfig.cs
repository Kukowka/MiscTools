using JoinningDataManager.Comparers;
using System;

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

        [Obsolete("only FieldName should be used for defining column ")]
        public string VdlColumnName { get; }
        public IJdmComparer Comparer { get; }
        public bool IsProductable { get; } //not used in current implementation


    }
}