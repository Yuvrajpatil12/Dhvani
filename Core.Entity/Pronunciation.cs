namespace Core.Entity
{
    public class Pronunciation
    {
        #region Declarations

        private bool _boolObjectChanged;
        private Int64 _intID;
        private Int64 _intUserID;
        private string _strOriginalWord;
        private string _strOriginalWordUrl;
        private string _strFormattedWord;
        private string _strFormattedWordUrl;
        private string _strLanguage;
        private string _strFullName;
        private byte _bytStatusId;
        private DateTime _datCreatedDate;
        private DateTime _datUpdateDate;

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

        public Int64 UserID
        {
            get { return this._intUserID; }
            set { this._intUserID = value; }
        }

        public string InitialText
        {
            get { return this._strOriginalWord; }
            set { this._strOriginalWord = value; }
        }

        public string AlternateText
        {
            get { return this._strFormattedWord; }
            set { this._strFormattedWord = value; }
        }

        public string Language
        {
            get { return this._strLanguage; }
            set { this._strLanguage = value; }
        }

        public string Locale
        {
            get;
            set;
        }

        public string UUID
        {
            get;
            set;
        }

        public string UserUUID
        {
            get;
            set;
        }

        public string LocaleCode
        {
            get;
            set;
        }

        public string AccentCode
        {
            get;
            set;
        }

        public string Accent
        {
            get;
            set;
        }

        public string VMUUID
        {
            get;
            set;
        }

        public string DisplayName
        {
            get;
            set;
        }

        public string Gender
        {
            get;
            set;
        }

        public string SampleUrl
        {
            get;
            set;
        }

        public string AgeBracket
        {
            get;
            set;
        }

        public string VoiceImage
        {
            get;
            set;
        }

        public string FullName
        {
            get { return this._strFullName; }
            set { this._strFullName = value; }
        }

        public byte StatusId
        {
            get { return this._bytStatusId; }
            set { this._bytStatusId = value; }
        }

        public DateTime CreatedDate
        {
            get { return this._datCreatedDate; }
            set { this._datCreatedDate = value; }
        }

        public DateTime UpdateDate
        {
            get { return this._datUpdateDate; }
            set { this._datUpdateDate = value; }
        }

        public string ShortName
        {
            get;
            set;
        }

        #endregion Properties
    }

    public class GetPronunciaions
    {
        public string voiceUUID
        {
            get;
            set;
        }

        public string original_Word
        {
            get;
            set;
        }

        public string revised_word
        {
            get;
            set;
        }

        public string ampvoicelocale
        {
            get;
            set;
        }

        public string ampvoicelocaleID
        {
            get;
            set;
        }

        public string ampvoiceaccent
        {
            get;
            set;
        }

        public string ampvoiceaccentID
        {
            get;
            set;
        }

        public string ampvoiceUUID
        {
            get;
            set;
        }

        public string ampDisplayName
        {
            get;
            set;
        }

        public string ampvoiceGender
        {
            get;
            set;
        }

        public string ampvoicesampleMp3
        {
            get;
            set;
        }

        public string ampvoicePhoto
        {
            get;
            set;
        }

        public string ampvoiceagerange
        {
            get;
            set;
        }
    }

    public class SetPronunciation
    {
        public string? UUID
        {
            get;
            set;
        }

        public string? UserUUID
        {
            get;
            set;
        }

        public string? original_word
        {
            get;
            set;
        }

        public string? revised_word
        {
            get;
            set;
        }

        public string? ampvoicelocale
        {
            get;
            set;
        }

        public string? ampvoicelocaleID
        {
            get;
            set;
        }

        public string? ampvoiceaccent
        {
            get;
            set;
        }

        public string? ampvoiceaccentID
        {
            get;
            set;
        }

        public string? ampvoiceUUID
        {
            get;
            set;
        }
        public byte StatusID
        {
            get;
            set;
        }
    }
}