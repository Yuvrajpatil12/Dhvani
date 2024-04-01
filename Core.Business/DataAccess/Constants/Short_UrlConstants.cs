namespace Core.Business.DataAccess.Constants
{
    public class Short_UrlDBFields
    {
        public static string IU_Flag = "IU_Flag";

        public static string TableNameVal = "Short_Url";
        public static string ID = "ID";
        public static string KeyValue = "KeyValue";
        public static string URLString = "URLString";
        public static string StatusID = "StatusID";
        public static string CreatedDate = "CreatedDate";
        public static string UpdatedDate = "UpdatedDate";
    }

    public class Short_UrlStoredProcedures
    {
        #region Object StoredProcedure

        public static string Short_UrlSAVE = "Short_Url_SAVE";
        public static string Short_UrlGetRecordById = "Short_Url_GetRecordById";

        public static string GetShort_UrlRecords = "Short_Url_GetRecords";
        public static string GetShort_UrlRecordByValue = "Short_Url_GetRecordByValue";
        public static string GetShort_UrlRecordByValueArray = "Short_Url_GetRecordByValueArray";
        public static string Get_SHORTURL_Key = "Get_SHORTURL_Key";

        #endregion Object StoredProcedure

        #region Collection StoredProcedure

        public static string Short_UrlSearch = "Short_Url_Search";
        public static string Short_UrlSearchByValue = "Short_Url_SearchByValue";
        public static string Short_UrlSearchByValueArray = "Short_Url_SearchByValueArray";

        #endregion Collection StoredProcedure

        public static string IsExist = "";
        public static string GetCollectionForQuery = "";
        public static string SortingString = "SortOrder";
    }
}