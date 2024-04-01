namespace Core.Entity
{
    public class LanguageMaster
    {
        #region Declarations

        private bool _boolObjectChanged;
        private int _intID;
        private string _strLanguage;
        private string _strLanguageCode;
        private byte _bytStatusID;
        private DateTime _datCreatedDate;

        #endregion Declarations

        #region Properties

        public bool ObjectChanged
        {
            get { return this._boolObjectChanged; }
            set { this._boolObjectChanged = value; }
        }

        public int ID
        {
            get { return this._intID; }
            set { this._intID = value; }
        }

        public string Language
        {
            get { return this._strLanguage; }
            set { this._strLanguage = value; }
        }

        public string LanguageCode
        {
            get { return this._strLanguageCode; }
            set { this._strLanguageCode = value; }
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

        #endregion Properties
    }
}