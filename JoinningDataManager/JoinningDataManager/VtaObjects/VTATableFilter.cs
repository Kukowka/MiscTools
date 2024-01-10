namespace JoinningDataManager
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class VTATableFilter
    {

        private byte pointField;

        private byte curveField;

        private byte surfaceField;

        private object processTypesField;

        /// <remarks/>
        public byte Point
        {
            get => this.pointField;
            set => this.pointField = value;
        }

        /// <remarks/>
        public byte Curve
        {
            get => this.curveField;
            set => this.curveField = value;
        }

        /// <remarks/>
        public byte Surface
        {
            get => this.surfaceField;
            set => this.surfaceField = value;
        }

        /// <remarks/>
        public object ProcessTypes
        {
            get => this.processTypesField;
            set => this.processTypesField = value;
        }
    }
}