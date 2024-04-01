namespace Core.Entity
{
    public class Users
    {
        #region Declarations

        private bool _boolObjectChanged;
        private int _intRowNumber;
        private Int64 _intID;
        private string _strUserName;
        private string _strPassword;
        private string _strFirstName;
        private string _strLastName;
        private string _strAlternateEmail;
        private string _strProfilePicture;
        private string _strUUID;
        private int _intRoleId;
        private int _intParentId;
        private int _intLanguageId;
        private byte _bytIsEmailVerified;
        private string _strEmailVerficationCode;
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

        public int RowNumber
        {
            get { return this._intRowNumber; }
            set { this._intRowNumber = value; }
        }

        public Int64 ID
        {
            get { return this._intID; }
            set { this._intID = value; }
        }

        public string UserName
        {
            get { return this._strUserName; }
            set { this._strUserName = value; }
        }
  

        public string Password
        {
            get { return this._strPassword; }
            set { this._strPassword = value; }
        }

        public string FirstName
        {
            get { return this._strFirstName; }
            set { this._strFirstName = value; }
        }

        public string LastName
        {
            get { return this._strLastName; }
            set { this._strLastName = value; }
        }

        public string AlternateEmail
        {
            get { return this._strAlternateEmail; }
            set { this._strAlternateEmail = value; }
        }

        public string ProfilePicture
        {
            get { return this._strProfilePicture; }
            set { this._strProfilePicture = value; }
        }

        public string UUID
        {
            get { return this._strUUID; }
            set { this._strUUID = value; }
        }

        public int RoleId
        {
            get { return this._intRoleId; }
            set { this._intRoleId = value; }
        }

        public int ParentId
        {
            get { return this._intParentId; }
            set { this._intParentId = value; }
        }

        public int LanguageId
        {
            get { return this._intLanguageId; }
            set { this._intLanguageId = value; }
        }

        public byte IsEmailVerified
        {
            get { return this._bytIsEmailVerified; }
            set { this._bytIsEmailVerified = value; }
        }

        public string EmailVerficationCode
        {
            get { return this._strEmailVerficationCode; }
            set { this._strEmailVerficationCode = value; }
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

        public string? APIKey { get; set; }

        #endregion Properties
    }

    public class dynamicallObject
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; }
    }
}