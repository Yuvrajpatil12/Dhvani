namespace Core.Business.DataAccess.Constants
{
    public class UsersDBFields
    {
        public static string IU_Flag = "IU_Flag";

        public static string TableNameVal = "Users";
        public static string RowNumber = "RowNumber";
        public static string ID = "ID";
        public static string UserName = "UserName";
        public static string Password = "Password";
        public static string FirstName = "FirstName";
        public static string LastName = "LastName";
        public static string AlternateEmail = "AlternateEmail";
        public static string ProfilePicture = "ProfilePicture";
        public static string UUID = "UUID";
        public static string RoleId = "RoleId";
        public static string ParentId = "ParentId";
        public static string LanguageId = "LanguageId";
        public static string IsEmailVerified = "IsEmailVerified";
        public static string EmailVerficationCode = "EmailVerficationCode";
        public static string StatusId = "StatusId";
        public static string CreatedDate = "CreatedDate";
        public static string UpdateDate = "UpdateDate";
        public static string APIKey = "APIKey";
    }

    public class UsersStoredProcedures
    {
        #region Object StoredProcedure

        #region Common StoredProcedure

        public static string Users_GetUserDetails = "Users_GetUserDetails";
        #endregion

        #region Login 

        public static string Users_Login = "Users_Login";


        #endregion

        #region Reset Password

        public static string Password_Update = "Password_Update";

        #endregion

        #region Save StoredProcedure

        public static string UsersSAVE = "Users_SAVE";

        #endregion
        

        #region Get StoredProcedure

        public static string UsersGetPasswordExist = "Users_GetUserPasswordExist";
        public static string GetUsersRecords = "Users_GetRecords";
        public static string Users_GetRecordsForUserList = "Users_GetRecordsForUserList";
        public static string UsersGetRecordById = "Users_GetRecordById";
        public static string UsersGetRecordsByKey = "Users_GetRecordsByKey";

        #endregion

        #region User Validation

        public static string Users_IsUserExists = "Users_IsUserExists";
        public static string User_IsUserExistCheckId = "User_IsUserExistCheckId";
        public static string IsUsernameExistEditClick = "IsUsernameExistEdit";
        #endregion

        #region Disable & Delete StoredProcedure
        public static string Users_UpdateStatus = "Users_UpdateStatus";

        #endregion



        #endregion Object StoredProcedure

        #region Collection StoredProcedure

        public static string UsersSearch = "Users_Search";
        public static string UsersSearchByValue = "Users_SearchByValue";
        public static string UsersSearchByValueArray = "Users_SearchByValueArray";


        
        
        public static string GetUsersRecordByValue = "Users_GetRecordByValue";
        public static string GetUsersRecordByValueArray = "Users_GetRecordByValueArray";

        #endregion Collection StoredProcedure

        public static string IsExist = "";
        public static string GetCollectionForQuery = "";
        public static string SortingString = "SortOrder";
    }
}