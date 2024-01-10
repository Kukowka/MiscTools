namespace JoinningDataManager
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class VTATableElement
    {

        private VTATableElementParameter[] parametersField;

        private VTATableElementPartPartParameter[][] partsField;

        private VTATableElementSupportPointSupportPointParameter[][] supportPointsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Parameter", IsNullable = false)]
        public VTATableElementParameter[] Parameters
        {
            get => this.parametersField;
            set => this.parametersField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Part", IsNullable = false)]
        [System.Xml.Serialization.XmlArrayItemAttribute("PartParameter", IsNullable = false, NestingLevel = 1)]
        public VTATableElementPartPartParameter[][] Parts
        {
            get => this.partsField;
            set => this.partsField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("SupportPoint", IsNullable = false)]
        [System.Xml.Serialization.XmlArrayItemAttribute("SupportPointParameter", IsNullable = false, NestingLevel = 1)]
        public VTATableElementSupportPointSupportPointParameter[][] SupportPoints
        {
            get => this.supportPointsField;
            set => this.supportPointsField = value;
        }
    }
}