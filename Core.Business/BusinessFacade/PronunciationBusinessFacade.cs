using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Business.DataAccess.Wrapper;
using Core.Business.DataAccess.Mapper;
using Core.Business.DataAccess.DataAccessLayer.General;
using Core.Business.DataAccess.DataAccessLayer;
using Core.Entity;
using Core.Business.DataAccess.Constants;
using Core.Utility;
using Core.Entity.Common;
using Core.Utility.Common;
using Core.Entity.Enums;
using Core.Resources;

namespace Core.Business.BusinessFacade
{
    public class PronunciationBusinessFacade//:UniversalObject
    {
        private dynamic objdynamicWrapper;
        private JsonMessage _jsonMessage = null;
        private PronunciationWrapperColletion objPronunciationWrapperColletion = new PronunciationWrapperColletion();
        private PronunciationWrapper objPronunciationWrapper = new PronunciationWrapper();
        private static readonly string _module = "Core.Business.BusinessFacade.PronunciationBusinessFacade";

        public PronunciationBusinessFacade()
        {
        }

        public PronunciationBusinessFacade(dynamic WrapperType)
        {
            objdynamicWrapper = WrapperType;
        }

        #region save

        public Int64 Save(dynamic objEntity)
        {
            try
            {
                objPronunciationWrapper.objWrapperClass = objEntity;
                DataAccess.DataAccessLayer.DataAccess dalObject = new DataAccess.DataAccessLayer.DataAccess();
                Transaction TransObj = new Transaction(dalObject);
                TransObj.ConnectionString = dbClass.SqlConnectString();
                Dictionary<string, Command> CommandsObj = new Dictionary<string, Command>();
                int commandCounter = 0;

                bool result = objPronunciationWrapper.Save(ref CommandsObj, ref commandCounter);
                TransObj.AddCommandList(CommandsObj);
                if (TransObj.ExecuteTransaction())
                {
                    Int64 ID = 0;
                    if (long.TryParse(TransObj.ReturnID, out ID) && ID > 0)
                    {
                        objEntity.ID = ID;
                    }
                    return ID;
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

        public bool SaveAdmin(dynamic objEntity)
        {
            try
            {
                objPronunciationWrapper.objWrapperClass = objEntity;
                DataAccess.DataAccessLayer.DataAccess dalObject = new DataAccess.DataAccessLayer.DataAccess();
                Transaction TransObj = new Transaction(dalObject);
                TransObj.ConnectionString = dbClass.SqlConnectString();
                Dictionary<string, Command> CommandsObj = new Dictionary<string, Command>();
                int commandCounter = 0;

                bool result = objPronunciationWrapper.SaveAdmin(ref CommandsObj, ref commandCounter);
                TransObj.AddCommandList(CommandsObj);
                if (TransObj.ExecuteTransaction())
                {
                    Int64 ID = 0;
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

        #endregion save

        #region GetMethod

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

        public dynamic GetPronunciation(string userAPIKey)
        {
            dynamic getPronunciation = null;

            try
            {
                getPronunciation = objPronunciationWrapperColletion.GetPronunciation(userAPIKey);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetLanguageRelatedValues()", ex.Source, ex.Message, ex);
            }

            return getPronunciation;
        }

        public List<Pronunciation> GetPronunciationByUserID(string UserID, string voiceRegion, string voiceName)
        {
            List<Pronunciation> lstEntity = new List<Pronunciation>();
            try
            {
                lstEntity = objPronunciationWrapperColletion.GetPronunciationByUserID(UserID, voiceRegion, voiceName);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetPronunciationByUserID(UserID:" + UserID + ",voiceRegion:" + voiceRegion + ",voiceName:" + voiceName + ")", ex.Source, ex.Message, ex);
            }
            return lstEntity;
        }

        #endregion GetMethod

        public dynamic GetPronunciationList(Int64 UserID)

        {
            dynamic getPronunciation = null;

            try
            {
                getPronunciation = objPronunciationWrapperColletion.GetPronunciationList(UserID);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetPronunciationList()", ex.Source, ex.Message, ex);
            }

            return getPronunciation;
        }

        public dynamic GetPronunciationById(Int64 ID)
        {
            dynamic getPronunciation = null;

            try
            {
                getPronunciation = objPronunciationWrapperColletion.GetPronunciationById(ID);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetPronunciationList()", ex.Source, ex.Message, ex);
            }

            return getPronunciation;
        }

        public JsonMessage ChangeStatus(Int64 ID, string StatusID)
        {
            try
            {
                PronunciationWrapper objWrapper = new PronunciationWrapper();
                objWrapper.ChangeStatus(ID, StatusID);
                string strMessage = "";
                if (StatusID == "0")
                    strMessage = "Record Deleted";
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
    }
}