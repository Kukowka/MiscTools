using JoinningDataManager.Comparers;
using System;

namespace JoinningDataManager
{
    public class JdmColumnConfig
    {
        public JdmColumnConfig(string fieldName, string vdlColumnName, IJdmComparer comparer, string exportVdlColumnName)
        {
            FieldName = fieldName;
            VdlColumnName = vdlColumnName;
            Comparer = comparer;
            ExportVdlColumnName = exportVdlColumnName;
        }

        public string FieldName { get; }

        public string VdlColumnName { get; }
        public string ExportVdlColumnName { get; }

        public IJdmComparer Comparer { get; }
        public bool IsProductable { get; } //not used in current implementation
    }
}