namespace Core.Business.DataAccess.Constants
{
    public class LanguageMasterDBFields
    {
        public static string IU_Flag = "IU_Flag";

        public static string TableNameVal = "LanguageMaster";
        public static string ID = "ID";
        public static string Language = "Language";
        public static string LanguageCode = "LanguageCode";
        public static string StatusID = "StatusID";
        public static string CreatedDate = "CreatedDate";
    }

    public class LanguageMasterStoredProcedures
    {
        #region Object StoredProcedure

        public static string LanguageMasterSAVE = "LanguageMaster_SAVE";
        public static string LanguageMasterGetRecordById = "LanguageMaster_GetRecordById";

        public static string GetLanguageMasterRecords = "LanguageMaster_GetRecords";
        public static string GetLanguageMasterRecordByValue = "LanguageMaster_GetRecordByValue";
        public static string GetLanguageMasterRecordByValueArray = "LanguageMaster_GetRecordByValueArray";

        #endregion Object StoredProcedure

        #region Collection StoredProcedure

        public static string LanguageMasterSearch = "LanguageMaster_Search";
        public static string LanguageMasterSearchByValue = "LanguageMaster_SearchByValue";
        public static string LanguageMasterSearchByValueArray = "LanguageMaster_SearchByValueArray";

        #endregion Collection StoredProcedure

        public static string IsExist = "";
        public static string GetCollectionForQuery = "";
        public static string SortingString = "SortOrder";
    }
}