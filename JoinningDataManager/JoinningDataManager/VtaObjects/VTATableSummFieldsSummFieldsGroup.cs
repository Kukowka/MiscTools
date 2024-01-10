namespace JoinningDataManager
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class VTATableSummFieldsSummFieldsGroup
    {

        private VTATableSummFieldsSummFieldsGroupSummField summFieldField;

        private string nameField;

        /// <remarks/>
        public VTATableSummFieldsSummFieldsGroupSummField SummField
        {
            get => this.summFieldField;
            set => this.summFieldField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get => this.nameField;
            set => this.nameField = value;
        }
    }
}