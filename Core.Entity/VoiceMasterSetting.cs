using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Entity
{
    public class VoiceMasterSetting
    {
        #region Declarations

        private bool _boolObjectChanged;
        private Int64 _intID;
        private string _strUUID;
        private string _strDescription;
        private string _strVoiceImage;
        private byte _bytStatusId;
        private DateTime _datCreatedDate;
        private DateTime _datUpdatedDate;
        private Int64 _intVoiceMasterID;
        private string _strAgeBracket;
        private string _strVoiceProvider;
        private string _strCharLimit;
        private string _strSampleUrl;
        private string _strVMDisplayName;
        private string _strVMLocalName;
        private string _strVMShortName;
        private string _strVMLocale;
        private string _strVMLocaleName;
        private string _strVMSampleRateHertz;
        private string _strVMVoiceType;
        private string _strVMStatus;
        private string _strVMWordsPerMinute;
        private string _strVMName;
        private string _strVMGender;
        private string _strVMStyleList;
        private string _strVMUUID;


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

        public string UUID
        {
            get { return this._strUUID; }
            set { this._strUUID = value; }
        }

        public string Description
        {
            get { return this._strDescription; }
            set { this._strDescription = value; }
        }

        public string VoiceImage
        {
            get { return this._strVoiceImage; }
            set { this._strVoiceImage = value; }
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

        public DateTime UpdatedDate
        {
            get { return this._datUpdatedDate; }
            set { this._datUpdatedDate = value; }
        }

        public Int64 VoiceMasterID
        {
            get { return this._intVoiceMasterID; }
            set { this._intVoiceMasterID = value; }
        }

        public string AgeBracket
        {
            get { return this._strAgeBracket; }
            set { this._strAgeBracket = value; }
        }

        public string VoiceProvider
        {
            get { return this._strVoiceProvider; }
            set { this._strVoiceProvider = value; }
        }

        public string CharLimit
        {
            get { return this._strCharLimit; }
            set { this._strCharLimit = value; }
        }

        public string SampleUrl
        {
            get { return this._strSampleUrl; }
            set { this._strSampleUrl = value; }
        }

        public string VMDisplayName
        {
            get { return this._strVMDisplayName; }
            set { this._strVMDisplayName = value; }
        }

        public string VMLocalName
        {
            get { return this._strVMLocalName; }
            set { this._strVMLocalName = value; }
        }

        public string VMShortName
        {
            get { return this._strVMShortName; }
            set { this._strVMShortName = value; }
        }

        public string VMLocale
        {
            get { return this._strVMLocale; }
            set { this._strVMLocale = value; }
        }

        public string VMLocaleName
        {
            get { return this._strVMLocaleName; }
            set { this._strVMLocaleName = value; }
        }

        public string VMSampleRateHertz
        {
            get { return this._strVMSampleRateHertz; }
            set { this._strVMSampleRateHertz = value; }
        }

        public string VMVoiceType
        {
            get { return this._strVMVoiceType; }
            set { this._strVMVoiceType = value; }
        }

        public string VMStatus
        {
            get { return this._strVMStatus; }
            set { this._strVMStatus = value; }
        }

        public string VMWordsPerMinute
        {
            get { return this._strVMWordsPerMinute; }
            set { this._strVMWordsPerMinute = value; }
        }

        public string VMName
        {
            get { return this._strVMName; }
            set { this._strVMName = value; }
        }

        public string VMGender
        {
            get { return this._strVMGender; }
            set { this._strVMGender = value; }
        }

        public string VMStyleList
        {
            get { return this._strVMStyleList; }
            set { this._strVMStyleList = value; }
        }

        public string VMUUID
        {
            get { return this._strVMUUID; }
            set { this._strVMUUID = value; }
        }

        public Int64 RowNumber { get; set; }

        public string? TagName { get; set; }
        public string? BestForName { get; set; } 
        public string? StyleName { get; set; } 

        public List<TagMaster> _listTagMasters { get; set; } 
        public List<BestForMaster>? _listBestForMaster { get; set; } 

        public List<Int64> TagMasterIDs { get; set; } 
        public Int64? VmBestForMasterID { get; set; }
        public int IsAdded { get; set; }
        public string? VMLanguage { get; set; }
        public string? VMAccent { get; set; }
        public List<VMStyleList> _StyleListValues { get; set; }
        
        public Int64 UserID { get; set; }
        public int Type { get; set; }
        public Int64 VoiceMasterUserMapID { get; set; }


        #endregion Properties
    }
}
