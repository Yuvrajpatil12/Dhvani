namespace Core.Business.DataAccess.Constants
{
    public class PronunciationDBFields
    {
        public static string IU_Flag = "IU_Flag";

        public static string TableNameVal = "Pronunciation";
        public static string ID = "ID";
        public static string UserID = "UserID";
        public static string InitialText = "InitialText";
        public static string OriginalWordUrl = "OriginalWordUrl";
        public static string AlternateText = "AlternateText";
        public static string FormattedWordUrl = "FormattedWordUrl";
        public static string Locale = "Locale";
        public static string Accent = "Accent";
        public static string LocaleCode = "LocaleCode";
        public static string AccentCode = "AccentCode";
        public static string StatusId = "StatusId";
        public static string CreatedDate = "CreatedDate";
        public static string UpdateDate = "UpdateDate";
        public static string UserAPIKey = "UserAPIKey";
        public static string UUID = "UUID";
        public static string VMUUID = "VMUUID";
        public static string UserUUID = "UserUUID";
        public static string DisplayName = "DisplayName";
        public static string Gender = "Gender";
        public static string AgeBracket = "AgeBracket";
        public static string VoiceImage = "VoiceImage";
        public static string SampleUrl = "SampleUrl";
        public static string ShortName = "ShortName";
    }

    public class PronunciationStoredProcedures
    {
        #region Object StoredProcedure

        public static string PronunciationSAVE = "Pronunciation_SAVE";
        public static string Pronunciation_AdminSave = "Pronunciation_AdminSave";
        public static string PronunciationGetRecordById = "Pronunciation_GetRecordById";

        public static string GetPronunciationRecords = "Pronunciation_GetRecords";
        public static string GetPronunciationRecordByValue = "Pronunciation_GetRecordByValue";
        public static string GetPronunciationRecordByValueArray = "Pronunciation_GetRecordByValueArray";

        public static string PronunciationGetRecordByUserApiKey = "Pronunciation_GetRecordByUserApiKey";

        public static string Pronunciation_ByUserID = "Pronunciation_ByUserID";

        public static string Pronunciation_UpdateStatus = "Pronunciation_UpdateStatus";

        #endregion Object StoredProcedure

        #region Collection StoredProcedure

        public static string PronunciationSearch = "Pronunciation_Search";
        public static string PronunciationSearchByValue = "Pronunciation_SearchByValue";
        public static string PronunciationSearchByValueArray = "Pronunciation_SearchByValueArray";

        #endregion Collection StoredProcedure

        public static string IsExist = "";
        public static string GetCollectionForQuery = "";
        public static string SortingString = "SortOrder";
    }
}