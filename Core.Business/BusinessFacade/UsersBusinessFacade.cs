using Core.Business.DataAccess.DataAccessLayer;
using Core.Business.DataAccess.Wrapper;
using Core.Entity;
using Core.Entity.Common;
using Core.Entity.Enums;
using Core.Resources;
using Core.Utility.Common;

namespace Core.Business.BusinessFacade
{
    public class UsersBusinessFacade//:UniversalObject
    {
        private dynamic objdynamicWrapper;
        private UsersWrapperColletion objUsersWrapperColletion = new UsersWrapperColletion();
        private UsersWrapper objUsersWrapper = new UsersWrapper();
        private static readonly string _module = "Core.Business.BusinessFacade.UsersBusinessFacade";


        private Users objEntity = new Users();
        private JsonMessage _jsonMessage = null;

        public UsersBusinessFacade()
        {
        }

        public UsersBusinessFacade(dynamic WrapperType)
        {
            objdynamicWrapper = WrapperType;
        }


        #region Not Use Get Methods
        public dynamic GetRecordsList()
        {
            string[,] Sort = new string[1, 2];
            if (objdynamicWrapper.GetRecords(false, Sort))
            {
                return objdynamicWrapper.Items;
            }
            return null;
        }

        public dynamic GetRecordsListByValue(string Field, String Values)
        {
            string[,] Sort = new string[1, 2];

            if (objdynamicWrapper.GetRecords(false, Sort, true, Field, Values))
            {
                return objdynamicWrapper.Items;
            }
            return null;
        }

        public dynamic GetRecordByValue(string Field, string Values)
        {
            string[,] Sort = new string[1, 2];

            if (objdynamicWrapper.GetRecordByValue(Field, Values))
            {
                return objdynamicWrapper.objUsers;
            }
            return null;
        }

        public dynamic GetRecords(int Id)
        {
            if (objUsersWrapper.GetRecordById(Id))
            {
                return objUsersWrapper.objWrapperClass;
            }
            return null;
        }
        #endregion

        #region Login Methods

        public Users Authenticate(string username, string password, string LoginMode)
        {
            try
            {
                return objUsersWrapper.Authenticate(username, password, LoginMode);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "Authenticate(username=" + username + ", password=" + password + ",LoginMode:" + LoginMode + ")", ex.Source, ex.Message, ex);
            }
            return null;
        }


        public Users GetUserDetailsByUsername(string username)
        {
            objEntity = new Users();
            try
            {
                if (!string.IsNullOrEmpty(username))
                {
                    objUsersWrapper = new UsersWrapper();
                    objEntity = objUsersWrapper.IsUserIdExist(username,0);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetUserDetailsByUsername(username=" + username + ")", ex.Source, ex.Message, ex);
            }

            return objEntity;
        }
        #endregion

        #region Reset Password

        public bool UpdatePassword(Users objUsers)
        {
            try
            {
                objUsersWrapper = new UsersWrapper();
                objUsersWrapper.objWrapperClass = objUsers;
                DataAccess.DataAccessLayer.DataAccess dalObject = new DataAccess.DataAccessLayer.DataAccess();
                DataAccess.DataAccessLayer.Transaction TransObj = new DataAccess.DataAccessLayer.Transaction(dalObject);
                TransObj.ConnectionString = dbClass.SqlConnectString();
                Dictionary<string, Command> CommandsObj = new Dictionary<string, Command>();
                int commandCounter = 0;

                bool result = objUsersWrapper.UpdatePassword(ref CommandsObj, ref commandCounter);
                TransObj.AddCommandList(CommandsObj);
                if (TransObj.ExecuteTransaction())
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "UpdatePassword()", ex.Source, ex.Message, ex);
                return false;
            }
            finally { }
        }

        #endregion

        #region Save Methods

        public Int64 Save(dynamic objEntity)
        {
            try
            {
                objUsersWrapper.objWrapperClass = objEntity;
                DataAccess.DataAccessLayer.DataAccess dalObject = new DataAccess.DataAccessLayer.DataAccess();
                DataAccess.DataAccessLayer.Transaction TransObj = new DataAccess.DataAccessLayer.Transaction(dalObject);
                TransObj.ConnectionString = dbClass.SqlConnectString();
                Dictionary<string, Command> CommandsObj = new Dictionary<string, Command>();
                int commandCounter = 0;

                bool result = objUsersWrapper.Save(ref CommandsObj, ref commandCounter);
                TransObj.AddCommandList(CommandsObj);
                if (TransObj.ExecuteTransaction())
                {
                    Int64 ID = 0;
                    if (long.TryParse(TransObj.ReturnID, out ID) && ID > 0)
                    {
                        objEntity.ID = ID;
                    }
                    return objEntity.ID;
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally { }
        }

        #endregion

        #region Disable Methods

        #endregion

        #region User Validation

        public JsonMessage IsUserIdExist(string UserName, long ID)
        {
            bool returnValue = false;
            try
            {
                if (!string.IsNullOrEmpty(UserName))
                {
                    objUsersWrapper = new UsersWrapper();
                    objEntity = objUsersWrapper.IsUserIdExist(UserName, ID);

                    if (objEntity != null && objEntity.ID > 0)
                        returnValue = true;

                    if (returnValue)
                        _jsonMessage = new JsonMessage(true, Resource.lbl_success, Resource.lbl_success, KeyEnums.JsonMessageType.SUCCESS, objEntity);
                    else
                        _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_emilIdInUse, KeyEnums.JsonMessageType.ERROR, objEntity);
                }
            }
            catch (Exception ex)
            {
                _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_internalServerErrorOccurred, KeyEnums.JsonMessageType.ERROR, "", "Exception", ex.Message);
                Log.WriteLog(_module, "IsEmailIdExist(email=" + UserName + ")", ex.Source, ex.Message, ex);
            }
            return _jsonMessage;
        }

        // check user name exists or not in edit click
      
        public JsonMessage IsUsernameExistClick(string username)
        {
            bool returnValue = false;
            try
            {
                if (!string.IsNullOrEmpty(username))
                {
                    objUsersWrapper = new UsersWrapper();
                    objEntity = objUsersWrapper.IsUsernameExistClick(username);

                    if (objEntity != null && objEntity.ID > 0)
                        returnValue = true;

                    if (returnValue)
                        _jsonMessage = new JsonMessage(false, Resource.lbl_error, "Username already exists", KeyEnums.JsonMessageType.ERROR, objEntity);
                    else
                        _jsonMessage = new JsonMessage(true, Resource.lbl_success, Resource.lbl_success, KeyEnums.JsonMessageType.SUCCESS);
                }
            }
            catch (Exception ex)
            {
                _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_internalServerErrorOccurred, KeyEnums.JsonMessageType.ERROR, "", "Exception", ex.Message);
                Log.WriteLog(_module, "IsUsernameExistEditClick(username=" + username + ")", ex.Source, ex.Message, ex);
            }
            return _jsonMessage;
        }

        public JsonMessage IsUsernameExistEditClick(Int64 id,string userName)
        {
            bool returnValue = false;
            try
            {
                if (id != 0)
                {
                    objUsersWrapper = new UsersWrapper();
                    objEntity = objUsersWrapper.IsUsernameExistEditClick(id, userName);

                    if (objEntity != null && objEntity.ID > 0)
                        returnValue = true;

                    if (returnValue)
                        _jsonMessage = new JsonMessage(false, Resource.lbl_error, "Username already exists", KeyEnums.JsonMessageType.ERROR, objEntity);
                    else
                        _jsonMessage = new JsonMessage(true, Resource.lbl_success, Resource.lbl_success, KeyEnums.JsonMessageType.SUCCESS);
                }
            }
            catch (Exception ex)
            {
                _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_internalServerErrorOccurred, KeyEnums.JsonMessageType.ERROR, "", "Exception", ex.Message);
                Log.WriteLog(_module, "IsUsernameExistEditClick(id=" + id + ")", ex.Source, ex.Message, ex);
            }
            return _jsonMessage;
        }


        #endregion

        #region Get Methods

        public dynamic getUsersList( Int64 userId)
        {

            dynamic _getList = null;

            try
            {
                _getList = objUsersWrapperColletion.getUsersList(userId);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "getUsersList()", ex.Source, ex.Message, ex);
            }

            return _getList;
        }

        public dynamic getUserInfo(string userId)
        {

            dynamic _getUser = null;

            try
            {
                _getUser = objUsersWrapperColletion.getUserInfo(userId);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "getUsersList()", ex.Source, ex.Message, ex);
            }

            return _getUser;
        }

   
        /// <summary>
        /// Return the user data from API Key
        /// </summary>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public Users GetUserIdFromAPIKey(string  apiKey)
        {

            Users _getUser = new Users();

            try
            {
                _getUser = objUsersWrapperColletion.GetUserIdFromAPIKey(apiKey);
                return _getUser;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetUserIdFromAPIKey()", ex.Source, ex.Message, ex);
            }

            return new Users();
        }

       


        #endregion

        #region Change Status

        public JsonMessage ChangeStatus(Int64 ID, string StatusID)
        {
            try
            {
                UsersWrapper objWrapper = new UsersWrapper();
                objWrapper.ChangeStatus(ID, StatusID);
                string strMessage = "";
                if (StatusID == "0")
                    strMessage = Resource.lbl_accountDisabled;
                else if (StatusID == "1")
                    strMessage = "Enabled";
                else if (StatusID == "2")
                    strMessage = Resource.lbl_accountDeleted;
                _jsonMessage = new JsonMessage(true, Resource.lbl_success, strMessage, KeyEnums.JsonMessageType.SUCCESS, "", "");
            }
            catch (Exception ex)
            {
                _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_internalServerErrorOccurred, KeyEnums.JsonMessageType.ERROR, "", "0", null);
                Log.WriteLog(_module, "ChangeStatus(ID=" + ID + ",StatusID=" + StatusID + ")", ex.Source, ex.Message, ex);
            }

            return _jsonMessage;
        }

        #endregion
    }
}