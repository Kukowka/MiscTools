using System.Collections.Generic;

namespace JoinningDataManager;

public class JdmComparerConfig
{
    public List<JdmColumnConfig> Columns { get; }

    public List<JdmPartDef> PartDefinitions { get; }

    public JdmXyzColumnDef XyzColumnDefs { get; }

    public string VdlSheetName { get; }

    public string VtaSheetName { get; }

    public int VdlHeaderRowIndex { get; }

    public int VtaHeaderRowIndex { get; }

}