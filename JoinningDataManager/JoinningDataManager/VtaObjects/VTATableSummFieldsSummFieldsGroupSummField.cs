namespace JoinningDataManager
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class VTATableSummFieldsSummFieldsGroupSummField
    {

        private string nameField;

        private byte countField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get => this.nameField;
            set => this.nameField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Count
        {
            get => this.countField;
            set => this.countField = value;
        }
    }
}