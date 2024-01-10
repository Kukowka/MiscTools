namespace JoinningDataManager
{
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class VTATable
    {

        private string titleField;

        private VTATableFilter filterField;

        private VTATableHeaderField[] headerFieldsField;

        private VTATableElement[] vTAElementsField;

        private VTATableSummFields summFieldsField;

        public string Title
        {
            get => this.titleField;
            set => this.titleField = value;
        }

        public VTATableFilter Filter
        {
            get => this.filterField;
            set => this.filterField = value;
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("HeaderField", IsNullable = false)]
        public VTATableHeaderField[] HeaderFields
        {
            get => this.headerFieldsField;
            set => this.headerFieldsField = value;
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("Element", IsNullable = false)]
        public VTATableElement[] VTAElements
        {
            get => this.vTAElementsField;
            set => this.vTAElementsField = value;
        }

        public VTATableSummFields SummFields
        {
            get => this.summFieldsField;
            set => this.summFieldsField = value;
        }
    }
}