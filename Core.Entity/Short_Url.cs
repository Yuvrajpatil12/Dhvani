namespace Core.Entity
{
    public class Short_Url
    {
        #region Declarations

        private bool _boolObjectChanged;
        private Int64 _intID;
        private string _strKeyValue;
        private string _strURLString;
        private byte _bytStatusID;
        private DateTime _datCreatedDate;
        private DateTime _datUpdatedDate;

        #endregion Declarations

        #region Properties

        public bool ObjectChanged
        {
            get { return this._boolObjectChanged; }
            set { this._boolObjectChanged = value; }
        }

        public Int64 ID
        {
            get { return this._intID; }
            set { this._intID = value; }
        }

        public string KeyValue
        {
            get { return this._strKeyValue; }
            set { this._strKeyValue = value; }
        }

        public string URLString
        {
            get { return this._strURLString; }
            set { this._strURLString = value; }
        }

        public byte StatusID
        {
            get { return this._bytStatusID; }
            set { this._bytStatusID = value; }
        }

        public DateTime CreatedDate
        {
            get { return this._datCreatedDate; }
            set { this._datCreatedDate = value; }
        }

        public DateTime UpdatedDate
        {
            get { return this._datUpdatedDate; }
            set { this._datUpdatedDate = value; }
        }

        #endregion Properties
    }
}