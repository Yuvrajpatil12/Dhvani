namespace Core.Business.DataAccess.Constants
{
    public class ResetPasswordDBFields
    {
        public static string IU_Flag = "IU_Flag";

        public static string TableNameVal = "ResetPassword";
        public static string ID = "ID";
        public static string UserID = "UserID";
        public static string PassResetCode = "PassResetCode";
        public static string StatusId = "StatusId";
        public static string CreatedDate = "CreatedDate";
        public static string UpdatedDate = "UpdatedDate";
        public static string Pass_ResetID = "Pass_ResetID";
    }

    public class ResetPasswordStoredProcedures
    {
        #region Object StoredProcedure


        public static string ResetPasswordSAVE = "ResetPassword_SAVE";
        public static string ResetPasswordGetRecordById = "ResetPassword_GetRecordById";

        public static string GetResetPasswordRecords = "ResetPassword_GetRecords";
        public static string GetResetPasswordRecordByValue = "ResetPassword_GetRecordByValue";
        public static string GetResetPasswordRecordByValueArray = "ResetPassword_GetRecordByValueArray";

        public static string RESETPASS_TokenCheck = "RESETPASS_TokenCheck";

        #endregion Object StoredProcedure

        #region Collection StoredProcedure

        public static string ResetPasswordSearch = "ResetPassword_Search";
        public static string ResetPasswordSearchByValue = "ResetPassword_SearchByValue";
        public static string ResetPasswordSearchByValueArray = "ResetPassword_SearchByValueArray";

        #endregion Collection StoredProcedure

        public static string IsExist = "";
        public static string GetCollectionForQuery = "";
        public static string SortingString = "SortOrder";
    }
}