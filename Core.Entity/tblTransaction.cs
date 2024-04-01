namespace Core.Entity
{
    public class tblTransaction
    {
        #region Declarations

        private bool _boolObjectChanged;
        private Int64 _intID;
        private Int64 _intUserId;
        private string _strUUID;
        private string _strVoiceText;
        private string _strDisplayName;
        private string _strLocaleName;
        private string _strShortName;
        private string _strSpeakingStyle;
        private string _strSpeakingSpeed;
        private string _strPitch;
        private string _strUserAPIKey;
        private string _strMP3URL;
        private string _strMp3CreationDate;
        private string _strRequestDate;
        private bool _boolStatusId;
        private DateTime _datCreatedDate;
        private DateTime _datUpdatedDate;
        private Int64 _intRowNumber;

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

        public Int64 UserId
        {
            get { return this._intUserId; }
            set { this._intUserId = value; }
        }

        public string UUID
        {
            get { return this._strUUID; }
            set { this._strUUID = value; }
        }

        public string? VoiceText
        {
            get { return this._strVoiceText; }
            set { this._strVoiceText = value; }
        }

        public string? DisplayName
        {
            get { return this._strDisplayName; }
            set { this._strDisplayName = value; }
        }

        public string LocaleName
        {
            get { return this._strLocaleName; }
            set { this._strLocaleName = value; }
        }

        public string ShortName
        {
            get { return this._strShortName; }
            set { this._strShortName = value; }
        }

        public string? SpeakingStyle
        {
            get { return this._strSpeakingStyle; }
            set { this._strSpeakingStyle = value; }
        }

        public string SpeakingSpeed
        {
            get { return this._strSpeakingSpeed; }
            set { this._strSpeakingSpeed = value; }
        }

        public string Pitch
        {
            get { return this._strPitch; }
            set { this._strPitch = value; }
        }

        public string UserAPIKey
        {
            get { return this._strUserAPIKey; }
            set { this._strUserAPIKey = value; }
        }

        public string MP3URL
        {
            get { return this._strMP3URL; }
            set { this._strMP3URL = value; }
        }

        public string Mp3CreationDate
        {
            get { return this._strMp3CreationDate; }
            set { this._strMp3CreationDate = value; }
        }

        public string RequestDate
        {
            get { return this._strRequestDate; }
            set { this._strRequestDate = value; }
        }

        public bool StatusId
        {
            get { return this._boolStatusId; }
            set { this._boolStatusId = value; }
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

        public string TransactionStatus
        {
            get;
            set;
        }

        public Int64 RowNumber
        {
            get { return this._intRowNumber; }
            set { this._intRowNumber = value; }
        }

        public String? LoginName { get; set; }
        public string? Accent { get; set; }
        public string? CallBackUrl { get; set; }
        public string? StatusMessage { get; set; }
        public string? Language { get; set; }
        public string? ArtistName { get; set; }
        public string? ProviderName { get; set; }

        public string? TransactionType { get; set; }
        public string? Duration { get; set; }

        #endregion Properties
    }

    public class Status
    {
        public string UUID { get; set; }
        public string MP3URL { get; set; }
        public string TransactionStatus { get; set; }
        public string StatusMessage { get; set; }
    }

    public class GetUserAPIKey
    {
        public string UUID { get; set; }
    }
}