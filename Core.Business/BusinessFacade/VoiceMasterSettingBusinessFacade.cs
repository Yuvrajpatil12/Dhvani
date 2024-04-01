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
using System.Data.SqlClient;
using System.Data;

namespace Core.Business.BusinessFacade
{
    public class VoiceMasterSettingBusinessFacade//:UniversalObject
    {
        dynamic objdynamicWrapper; 
		VoiceMasterSettingWrapperColletion objVoiceMasterSettingWrapperColletion = new VoiceMasterSettingWrapperColletion();
        VoiceMasterSettingWrapper objVoiceMasterSettingWrapper = new VoiceMasterSettingWrapper();
		private static readonly string _module = "Core.Business.BusinessFacade.VoiceMasterSettingBusinessFacade";
		public VoiceMasterSettingBusinessFacade()
        {
            
        }
        public VoiceMasterSettingBusinessFacade(dynamic WrapperType)
        {
            objdynamicWrapper = WrapperType; 
        }
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
        public Int64 Save(dynamic objEntity)
        {
            try
            {

                objVoiceMasterSettingWrapper.objWrapperClass = objEntity;
                DataAccess.DataAccessLayer.DataAccess dalObject = new DataAccess.DataAccessLayer.DataAccess();
                Transaction TransObj = new Transaction(dalObject);
                TransObj.ConnectionString = dbClass.SqlConnectString();
                Dictionary<string, Command> CommandsObj = new Dictionary<string, Command>();
                int commandCounter = 0;

                bool result = objVoiceMasterSettingWrapper.Save(ref CommandsObj, ref commandCounter);
                TransObj.AddCommandList(CommandsObj);
                if (TransObj.ExecuteTransaction())
                {
                    Int64 ID = 0;
                    if (Int64.TryParse(TransObj.ReturnID, out ID) && ID > 0)
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
        #region Get Method 


        /// <summary>
        /// Retrieves the list of voice masters.
        /// </summary>
        /// <returns>The list of voice masters.</returns>
        public dynamic GetVoiceMasterList()
        {
            dynamic _getList = null;

            try
            {
                _getList = objVoiceMasterSettingWrapperColletion.GetVoiceMasterList();
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetVoiceMasterList()", ex.Source, ex.Message, ex);
            }

            return _getList;
        }
        public dynamic GetVoiceMasterListByUserID(Int64 userID)
        {
            dynamic _getList = null;

            try
            {
                _getList = objVoiceMasterSettingWrapperColletion.GetVoiceMasterListByUserID(userID);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetVoiceMasterList()", ex.Source, ex.Message, ex);
            }

            return _getList;
        }

        // get voice master base on userid get the data from the database
        public dynamic GetVoiceMasterById(string userId)
        {
            dynamic _getList = null;

            try
            {
                _getList = objVoiceMasterSettingWrapperColletion.GetVoiceMasterById(userId);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetVoiceMasterById()", ex.Source, ex.Message, ex);
            }

            return _getList;
        }
        #endregion




    }
}
