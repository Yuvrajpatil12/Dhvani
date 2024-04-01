namespace Core.Business.DataAccess.Constants
{
    public class VoiceMasterDBFields
    {
        public static string IU_Flag = "IU_Flag";

        public static string TableNameVal = "VoiceMaster";
        public static string ID = "ID";
        public static string Name = "Name";
        public static string DisplayName = "DisplayName";
        public static string LocalName = "LocalName";
        public static string ShortName = "ShortName";
        public static string Gender = "Gender";
        public static string Locale = "Locale";
        public static string LocaleName = "LocaleName";
        public static string SampleRateHertz = "SampleRateHertz";
        public static string VoiceType = "VoiceType";
        public static string Status = "Status";
        public static string WordsPerMinute = "WordsPerMinute";
        public static string StatusId = "StatusId";
        public static string CreatedDate = "CreatedDate";
        public static string UpdateDate = "UpdateDate";
        public static string UserID = "UserID";
        public static string VoiceImage = "VoiceImage";
        public static string Provider = "Provider";
        public static string AgeBracket = "AgeBracket";
        public static string Accent = "Accent";
        public static string AccentCode = "AccentCode";
        public static string ProviderId = "ProviderId";
        public static string Alias = "Alias";
        public static string LocaleCode = "LocaleCode";
        public static string UserAPIKey = "UserAPIKey";
        public static string RowNumber = "RowNumber";
        public static string VoiceID = "VoiceID";
        public static string IsAdded = "IsAdded";
        public static string VoiceMasterUserMapID = "VoiceMasterUserMapID";
        public static string VoiceMasterID = "VoiceMasterID";
        public static string Type = "Type";
        public static string Description = "Description";
        public static string VMUUID = "VMUUID";
        public static string CharLimit = "CharLimit";
        public static string SampleURL = "SampleURL";
        public static string TagName = "TagName";
        public static string BestForName = "BestForName";
        public static string StyleName = "StyleName";
        public static string StyleValue = "StyleValue";

        public static string ampVoiceProvider = "ampVoiceProvider";
        public static string ampvoiceUUID = "ampvoiceUUID";
        public static string ampDisplayName = "ampDisplayName";
        public static string ampvoiceGender = "ampvoiceGender";
        public static string ampvoicePhoto = "ampvoicePhoto";
        public static string ampvoicedescription = "ampvoicedescription";
        public static string ampvoicelocale = "ampvoicelocale";
        public static string ampvoicelocaleID = "ampvoicelocaleID";
        public static string ampvoiceaccentID = "ampvoiceaccentID";
        public static string ampvoiceaccent = "ampvoiceaccent";
        public static string ampshortName = "ampshortName";
        public static string ampvoiceagerange = "ampvoiceagerange";
        public static string ampvoiceType = "ampvoiceType";
        public static string ampSampleRateHertz = "ampSampleRateHertz";
        public static string ampStatus = "ampStatus";
        public static string ampWordsPerMinute = "ampWordsPerMinute";
        public static string ampCharacterLimit = "ampCharacterLimit";
        public static string ampvoicesampleMp3 = "ampvoicesampleMp3";
    }

    public class VoiceMasterStoredProcedures
    {
        #region Object StoredProcedure

        public static string VoiceMasterSAVE = "VoiceMaster_SAVE";
        public static string VoiceMasterGetRecordById = "VoiceMaster_GetRecordById";

        public static string GetVoiceMasterRecords = "VoiceMaster_GetRecords";
        public static string GetVoiceMasterRecordByValue = "VoiceMaster_GetRecordsBy";
        public static string GetVoiceMasterRecordByLanguageValue = "VoiceMaster_GetRecordsByLanguageValue";
        public static string GetVoiceMasterRecordByValueArray = "VoiceMaster_GetRecordByValueArray";
        public static string VoiceMasterUserMap_GetVoiceByUserID = "VoiceMasterUserMap_GetVoiceByUserID";
        public static string VoiceMasterGetRecords = "VoiceMaster_GetList";
        public static string VoiceMasterByUserIDGetRecords = "VoiceMasterByUserIDGetRecords";
        public static string VoiceMasterUserMap_UpdateByUserID = "VoiceMasterUserMap_UpdateByUserID";
        public static string VoiceMasterUserMapSAVE = "VoiceMasterUserMap_SAVE";
        public static string VoiceMaster_GetLanguage = "VoiceMaster_GetLanguage";
        public static string VoiceMaster_GetAccent = "VoiceMaster_GetAccent";
        public static string VoiceMaster_GetVoice = "VoiceMaster_GetVoice";
        public static string VoiceMaster_GetStyle = "VoiceMaster_GetStyle";
        public static string VoiceMasterGetRecordByAccent = "VoiceMasterGetRecord_ByAccent";
        public static string VoiceMasterGetRecordByShortName = "VoiceMasterGetRecord_ByShortName";

        #endregion Object StoredProcedure

        #region Collection StoredProcedure

        public static string VoiceMasterSearch = "VoiceMaster_Search";
        public static string VoiceMasterSearchByValue = "VoiceMaster_SearchByValue";
        public static string VoiceMasterSearchByValueArray = "VoiceMaster_SearchByValueArray";

        #endregion Collection StoredProcedure

        public static string IsExist = "";
        public static string GetCollectionForQuery = "";
        public static string SortingString = "SortOrder";
    }
}