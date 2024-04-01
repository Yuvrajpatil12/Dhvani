namespace Core.Business.DataAccess.Constants
{
    public class AccessMemberDBFields
    {
        public static string IU_Flag = "IU_Flag";

        public static string TableNameVal = "AccessMember";
        public static string ID = "ID";
        public static string UserID = "UserID";
        public static string Url = "Url";
        public static string ReferrerURL = "ReferrerURL";
        public static string port = "port";
        public static string Host = "Host";
        public static string REMOTE_HOST = "REMOTE_HOST";
        public static string REMOTE_ADDR_IP = "REMOTE_ADDR_IP";
        public static string Useragent = "Useragent";
        public static string BrowserType = "BrowserType";
        public static string BrowserVersion = "BrowserVersion";
        public static string Platform = "Platform";
        public static string ClickedBy = "ClickedBy";
        public static string DeviceName = "DeviceName";
        public static string OperatingSystem = "OperatingSystem";
        public static string DeviceType = "DeviceType";
        public static string DeviceModel = "DeviceModel";
        public static string Build = "Build";
        public static string Version = "Version";
        public static string RoleID = "RoleID";
        public static string StatusID = "StatusID";
        public static string CreatedDate = "CreatedDate";
    }

    public class AccessMemberStoredProcedures
    {
        #region Object StoredProcedure

        public static string AccessMemberSAVE = "AccessMember_SAVE";
        public static string AccessMemberGetRecordById = "AccessMember_GetRecordById";

        public static string GetAccessMemberRecords = "AccessMember_GetRecords";
        public static string GetAccessMemberRecordByValue = "AccessMember_GetRecordByValue";
        public static string GetAccessMemberRecordByValueArray = "AccessMember_GetRecordByValueArray";

        #endregion Object StoredProcedure

        #region Collection StoredProcedure

        public static string AccessMemberSearch = "AccessMember_Search";
        public static string AccessMemberSearchByValue = "AccessMember_SearchByValue";
        public static string AccessMemberSearchByValueArray = "AccessMember_SearchByValueArray";

        #endregion Collection StoredProcedure

        public static string IsExist = "";
        public static string GetCollectionForQuery = "";
        public static string SortingString = "SortOrder";
    }
}