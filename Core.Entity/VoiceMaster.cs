namespace Core.Entity
{
    public class VoiceMaster
    {
        #region Declarations

        private bool _boolObjectChanged;
        private int _intID;
        private string _strName;
        private string _strDisplayName;
        private string _strLocalName;
        private string _strShortName;
        private string _strGender;
        private string _strLocale;
        private string _strLocaleName;
        private string _strSampleRateHertz;
        private string _strVoiceType;
        private string _strStatus;
        private string _strWordsPerMinute;
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

        public int ID
        {
            get { return this._intID; }
            set { this._intID = value; }
        }

        public string Name
        {
            get { return this._strName; }
            set { this._strName = value; }
        }

        public string DisplayName
        {
            get { return this._strDisplayName; }
            set { this._strDisplayName = value; }
        }

        public string LocalName
        {
            get { return this._strLocalName; }
            set { this._strLocalName = value; }
        }

        public string ShortName
        {
            get { return this._strShortName; }
            set { this._strShortName = value; }
        }

        public string Gender
        {
            get { return this._strGender; }
            set { this._strGender = value; }
        }

        public string Locale
        {
            get { return this._strLocale; }
            set { this._strLocale = value; }
        }

        public string LocaleName
        {
            get { return this._strLocaleName; }
            set { this._strLocaleName = value; }
        }

        public string SampleRateHertz
        {
            get { return this._strSampleRateHertz; }
            set { this._strSampleRateHertz = value; }
        }

        public string VoiceType
        {
            get { return this._strVoiceType; }
            set { this._strVoiceType = value; }
        }

        public string Status
        {
            get { return this._strStatus; }
            set { this._strStatus = value; }
        }

        public string WordsPerMinute
        {
            get { return this._strWordsPerMinute; }
            set { this._strWordsPerMinute = value; }
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

        public Int64 IsAdded { get; set; }
        public Int64? VoiceMasterUserMapID { get; set; }

        public Int64? VoiceMasterID { get; set; }
        public Int64 RowNumber { get; set; }
        public Int64? UserID { get; set; }
        public Int64? VoiceID { get; set; }
        public string UserAPIKey { get; set; }
        public string TagName { get; set; }
        public string BestForName { get; set; }
        public string StyleName { get; set; }
        public string StyleValue { get; set; }
        public string VMUUID { get; set; }
        public string Description { get; set; }
        public string CharLimit { get; set; }
        public string ProviderFullName { get; set; }
        public int Type { get; set; }
        
        public string title { get; set; }
        public string timePerformance { get; set; }
        public string basicEmotion { get; set; }
        public string SampleURL { get; set; }
        public Int64 ProviderId { get; set; }
        public string VoiceImage { get; set; }
        public string Alias { get; set; }
        public string LocaleCode { get; set; }

        public string Accent { get; set; }
        public string AccentCode { get; set; }
        public string AgeBracket { get; set; }

        public string Provider { get; set; }

        public string ampvoiceaccent { get; set; }

        public string ampvoiceUUID { get; set; }
        public string ampvoiceaccentID { get; set; }
        public string ampshortName { get; set; }
        public string ampvoiceagerange { get; set; }
        public string ampvoicePhoto { get; set; }
        public string ampDisplayName { get; set; }
        public string ampVoiceProvider { get; set; }
        public string ampvoicelocaleID { get; set; }
        public string ampvoicelocale { get; set; }
        public string ampvoicedescription { get; set; }
        public string ampWordsPerMinute { get; set; }
        public string ampCharacterLimit { get; set; }
        public string ampvoiceGender { get; set; }
        public string ampStatus { get; set; }
        public string ampvoiceType { get; set; }
        public string ampSampleRateHertz { get; set; }
        public string ampvoicesampleMp3 { get; set; }

        public List<StyleList> StyleList { get; set; }
        public List<Tags> Tags { get; set; }
        public List<BestFor> BestFor { get; set; }

        #endregion Properties
    }

    public class Voices
    {   public string ampvoiceaccent { get; set; }
        public string ampvoiceaccentID { get; set; }
        public string ampvoiceagerange { get; set; }
        public string ampvoicePhoto { get; set; }
        public string ampDisplayName { get; set; }
        public string ampVoiceProvider { get; set; }
        public string ampvoicelocaleID { get; set; }
        public string ampvoicelocale { get; set; }
        public string ampvoicedescription { get; set; }
        public string ampWordsPerMinute { get; set; }
        public string ampCharacterLimit { get; set; }
        public string ampvoiceGender { get; set; }
        public string Type { get; set; }
        public string VoicampvoiceTypeeType { get; set; }
        public string ampStatus { get; set; }
        public string ampSampleRateHertz { get;set;}
        public string ampvoicesampleMp3 { get; set; }
     
        public List<StyleList> ampStyleList { get; set; }
        public List<Tags> amptags { get; set; }
        public List<BestFor> ampbestFor { get; set; }
    }

    public class StyleList
    {
        public string StyleName { get; set; }
    }

    public class GetUsersAPIKey
    {
        public string UserAPIKey { get; set; }
    }

    public class ListVoice
    {
        public string? ID { get; set; }
    }

    public class ListVoiceMaster
    {
        public Int64? UserID { get; set; }
        public Int64? VoiceMasterUserMapID { get; set; }
        public Int64? VoiceID { get; set; }
        public int Type { get; set; }
        public List<ListVoice>? lstVoice { get; set; }
    }

    public class Tags
    {
        public string TagName { get; set; }
    }

    public class BestFor
    {
        public string BestForName { get; set; }
    }
}