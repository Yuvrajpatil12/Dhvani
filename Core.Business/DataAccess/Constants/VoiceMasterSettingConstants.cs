using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Business.DataAccess.Constants
{
    public class VoiceMasterSettingDBFields
    {
        public static string IU_Flag = "IU_Flag";

        public static string TableNameVal = "VoiceMasterSetting";
        public static string ID = "ID";
        public static string UUID = "UUID";
        public static string Description = "Description";
        public static string VoiceImage = "VoiceImage";
        public static string StatusId = "StatusId";
        public static string CreatedDate = "CreatedDate";
        public static string UpdatedDate = "UpdatedDate";
        public static string VoiceMasterID = "VoiceMasterID";
        public static string AgeBracket = "AgeBracket";
        public static string VoiceProvider = "VoiceProvider";
        public static string CharLimit = "CharLimit";
        public static string SampleUrl = "SampleUrl";
        public static string VMDisplayName = "VMDisplayName";
        public static string VMLocalName = "VMLocalName";
        public static string VMShortName = "VMShortName";
        public static string VMLocale = "VMLocale";
        public static string VMLocaleName = "VMLocaleName";
        public static string VMSampleRateHertz = "VMSampleRateHertz";
        public static string VMVoiceType = "VMVoiceType";
        public static string VMStatus = "VMStatus";
        public static string VMWordsPerMinute = "VMWordsPerMinute";
        public static string VMName = "VMName";
        public static string VMGender = "VMGender";
        public static string VMStyleList = "VMStyleList";
        public static string VMUUID = "VMUUID";
        public static string VMLanguage = "VMLanguage";
        public static string VMAccent = "VMAccent";
        public static string RowNumber = "RowNumber";
        public static string TagNames = "TagNames";
        public static string BestForNames = "BestForNames";
        public static string StyleListName = "StyleListName";
        public static string UserID = "UserID";
        public static string VoiceMasterUserMapID = "VoiceMasterUserMapID";
        public static string IsAdded = "IsAdded";
    }

    public class VoiceMasterSettingStoredProcedures
    {
        #region Object StoredProcedure

        public static string VoiceMasterSettingSAVE = "VoiceMasterSetting_SAVE";
        public static string VoiceMasterSettingGetRecordById = "VoiceMasterSetting_GetRecordById";

        public static string GetVoiceMasterSettingRecords = "VoiceMasterSetting_GetRecords";
        public static string GetVoiceMasterSettingRecordByValue = "VoiceMasterSetting_GetRecordByValue";
        public static string GetVoiceMasterSettingRecordByValueArray = "VoiceMasterSetting_GetRecordByValueArray";
        public static string VoiceMasterGetRecords = "VoiceMaster_GetList";

        #endregion Object StoredProcedure

        #region Collection StoredProcedure

        public static string VoiceMasterSettingSearch = "VoiceMasterSetting_Search";
        public static string VoiceMasterSettingSearchByValue = "VoiceMasterSetting_SearchByValue";
        public static string VoiceMasterSettingSearchByValueArray = "VoiceMasterSetting_SearchByValueArray";

        #endregion Collection StoredProcedure

        public static string IsExist = "";
        public static string GetCollectionForQuery = "";
        public static string SortingString = "SortOrder";
    }
}