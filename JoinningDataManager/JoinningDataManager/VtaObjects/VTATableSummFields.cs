namespace JoinningDataManager
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class VTATableSummFields
    {

        private VTATableSummFieldsSummField[] summFieldField;

        private VTATableSummFieldsSummFieldsGroup[] summFieldsGroupField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SummField")]
        public VTATableSummFieldsSummField[] SummField
        {
            get => this.summFieldField;
            set => this.summFieldField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SummFieldsGroup")]
        public VTATableSummFieldsSummFieldsGroup[] SummFieldsGroup
        {
            get => this.summFieldsGroupField;
            set => this.summFieldsGroupField = value;
        }
    }
}