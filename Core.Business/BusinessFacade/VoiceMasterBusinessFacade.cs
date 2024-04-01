using Core.Business.DataAccess.DataAccessLayer;
using Core.Business.DataAccess.Wrapper;
using Core.Entity.Common;
using Core.Entity.Enums;
using Core.Resources;
using Core.Utility.Common;

namespace Core.Business.BusinessFacade
{
    public class VoiceMasterBusinessFacade//:UniversalObject
    {
        private dynamic objdynamicWrapper;
        private VoiceMasterWrapperColletion objVoiceMasterWrapperColletion = new VoiceMasterWrapperColletion();
        private VoiceMasterWrapper objVoiceMasterWrapper = new VoiceMasterWrapper();
        private static readonly string _module = "Core.Business.BusinessFacade.VoiceMasterBusinessFacade";
        private JsonMessage _jsonMessage = null;

        public VoiceMasterBusinessFacade()
        {
        }

        public VoiceMasterBusinessFacade(dynamic WrapperType)
        {
            objdynamicWrapper = WrapperType;
        }

        #region System Get Methods

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
            if (objdynamicWrapper.GetRecordById(Id))
            {
                return objdynamicWrapper.objWrapperClass;
            }
            return null;
        }

        public dynamic VoiceMaster_GetRecordByAccent(string Locale)
        {
            dynamic _getList = null;

            try
            {
                _getList = objVoiceMasterWrapperColletion.VoiceMasterGetRecordByAccent(Locale);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetVoiceMasterList()", ex.Source, ex.Message, ex);
            }

            return _getList;
        }

        #endregion System Get Methods

        public bool Save(dynamic objEntity)
        {
            try
            {
                objdynamicWrapper.objWrapperClass = objEntity;
                DataAccess.DataAccessLayer.DataAccess dalObject = new DataAccess.DataAccessLayer.DataAccess();
                DataAccess.DataAccessLayer.Transaction TransObj = new DataAccess.DataAccessLayer.Transaction(dalObject);
                TransObj.ConnectionString = dbClass.SqlConnectString();
                Dictionary<string, Command> CommandsObj = new Dictionary<string, Command>();
                int commandCounter = 0;

                bool result = objdynamicWrapper.Save(ref CommandsObj, ref commandCounter);
                TransObj.AddCommandList(CommandsObj);
                if (TransObj.ExecuteTransaction())
                {
                    long ID = 0;
                    if (long.TryParse(TransObj.ReturnID, out ID) && ID > 0)
                    {
                        objEntity.ID = ID;
                    }
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally { }
        }

        public bool SaveVoiceToUser(dynamic objEntity)
        {
            try
            {
                objVoiceMasterWrapper.objWrapperClass = objEntity;
                DataAccess.DataAccessLayer.DataAccess dalObject = new DataAccess.DataAccessLayer.DataAccess();
                DataAccess.DataAccessLayer.Transaction TransObj = new DataAccess.DataAccessLayer.Transaction(dalObject);
                TransObj.ConnectionString = dbClass.SqlConnectString();
                Dictionary<string, Command> CommandsObj = new Dictionary<string, Command>();
                int commandCounter = 0;

                bool result = objVoiceMasterWrapper.SaveVoiceToUser(ref CommandsObj, ref commandCounter);
                TransObj.AddCommandList(CommandsObj);
                if (TransObj.ExecuteTransaction())
                {
                    long ID = 0;
                    if (long.TryParse(TransObj.ReturnID, out ID) && ID > 0)
                    {
                        objEntity.ID = ID;
                    }
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally { }
        }

        public dynamic VoiceMasterGetList(Int64 UserID)
        {
            dynamic _getList = null;

            try
            {
                _getList = objVoiceMasterWrapperColletion.VoiceMasterGetList(UserID);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetVoiceMasterList()", ex.Source, ex.Message, ex);
            }

            return _getList;
        }

        public dynamic VoiceMaster_GetLanguage(string type, Int64 userid)
        {
            dynamic _getList = null;

            try
            {
                _getList = objVoiceMasterWrapperColletion.VoiceMaster_GetLanguage(type, userid);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetVoiceMasterList()", ex.Source, ex.Message, ex);
            }

            return _getList;
        }

        public dynamic VoiceMaster_GetAccent(string language, Int64 userid)
        {
            dynamic _getList = null;

            try
            {
                _getList = objVoiceMasterWrapperColletion.VoiceMaster_GetAccent(language, userid);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetVoiceMasterList()", ex.Source, ex.Message, ex);
            }

            return _getList;
        }

        public dynamic VoiceMaster_GetVoice(string language, Int64 userid)
        {
            dynamic _getList = null;

            try
            {
                _getList = objVoiceMasterWrapperColletion.VoiceMaster_GetVoice(language, userid);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetVoiceMasterList()", ex.Source, ex.Message, ex);
            }

            return _getList;
        }

        public dynamic VoiceMaster_GetStyle(string language)
        {
            dynamic _getList = null;

            try
            {
                _getList = objVoiceMasterWrapperColletion.VoiceMaster_GetStyle(language);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetVoiceMasterList()", ex.Source, ex.Message, ex);
            }

            return _getList;
        }

        public dynamic VoiceMasterGetListByUserID(Int64 UserID)
        {
            dynamic _getList = null;

            try
            {
                _getList = objVoiceMasterWrapperColletion.VoiceMasterGetListByUserID(UserID);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetVoiceMasterList()", ex.Source, ex.Message, ex);
            }

            return _getList;
        }

        /// <summary>
        /// Return the ai voice data
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public dynamic GetDropDownValues()
        {
            dynamic _getUser = null;

            try
            {
                _getUser = objVoiceMasterWrapperColletion.GetDropDownValues();
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDropDownValues()", ex.Source, ex.Message, ex);
            }

            return _getUser;
        }

        public dynamic GetLanguageRelatedValues(string language)
        {
            dynamic _getUser = null;

            try
            {
                _getUser = objVoiceMasterWrapperColletion.GetLanguageRelatedValues(language);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetLanguageRelatedValues()", ex.Source, ex.Message, ex);
            }

            return _getUser;
        }

        public dynamic GetVoiceRecord(string userAPIKey)
        {
            dynamic getVoice = null;

            try
            {
                getVoice = objVoiceMasterWrapperColletion.GetVoiceRecord(userAPIKey);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetVoiceRecord()", ex.Source, ex.Message, ex);
            }

            return getVoice;
        }

        public JsonMessage UpdateVoice(Int64 UserID)
        {
            try
            {
                VoiceMasterWrapper objWrapper = new VoiceMasterWrapper();
                objWrapper.UpdateVoice(UserID);

                _jsonMessage = new JsonMessage(true, Resource.lbl_success, "", KeyEnums.JsonMessageType.SUCCESS, "", "");
            }
            catch (Exception ex)
            {
                _jsonMessage = new JsonMessage(false, Resource.lbl_error, Resource.lbl_internalServerErrorOccurred, KeyEnums.JsonMessageType.ERROR, "", "0", null);
                Log.WriteLog(_module, "ChangeStatus(UserID=" + UserID + ")", ex.Source, ex.Message, ex);
            }

            return _jsonMessage;
        }

        public dynamic GetVoiceByShortName(string shortName, Int64 userid)
        {
            dynamic getVoice = null;

            try
            {
                getVoice = objVoiceMasterWrapper.GetVoiceByShortName(shortName, userid);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetVoiceByShortName()", ex.Source, ex.Message, ex);
            }

            return getVoice;
        }
    }
}