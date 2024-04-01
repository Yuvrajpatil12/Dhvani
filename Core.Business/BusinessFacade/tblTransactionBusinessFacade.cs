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

namespace Core.Business.BusinessFacade
{
    public class tblTransactionBusinessFacade//:UniversalObject
    {
        private dynamic objdynamicWrapper;
        private tblTransactionWrapperColletion objtblTransactionWrapperColletion = new tblTransactionWrapperColletion();
        private tblTransactionWrapper objtblTransactionWrapper = new tblTransactionWrapper();
        private static readonly string _module = "Core.Business.BusinessFacade.tblTransactionBusinessFacade";

        public tblTransactionBusinessFacade()
        {
        }

        public tblTransactionBusinessFacade(dynamic WrapperType)
        {
            objdynamicWrapper = WrapperType;
        }


        #region System Get List 

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


        #endregion

        #region Save

        public Int64 Save(dynamic objEntity)
        {
            try
            {
                objtblTransactionWrapper.objWrapperClass = objEntity;
                DataAccess.DataAccessLayer.DataAccess dalObject = new DataAccess.DataAccessLayer.DataAccess();
                Transaction TransObj = new Transaction(dalObject);
                TransObj.ConnectionString = dbClass.SqlConnectString();
                Dictionary<string, Command> CommandsObj = new Dictionary<string, Command>();
                int commandCounter = 0;

                bool result = objtblTransactionWrapper.Save(ref CommandsObj, ref commandCounter);
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


        #endregion

        #region Get List

        public dynamic GetTTSStatus(string UUID, string userAPIKey)

        {
            dynamic getTransactioinRecord = null;

            try
            {
                getTransactioinRecord = objtblTransactionWrapperColletion.GetTTSStatus(UUID, userAPIKey);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetLanguageRelatedValues()", ex.Source, ex.Message, ex);
            }

            return getTransactioinRecord;
        }


     
        /// <summary>
        /// This method is return all transation realted details for superadmin 
        /// </summary>
        /// <param name="transationId"></param>
        /// <returns> Transation list return</returns>
        public dynamic getTransactionList(Int64 userId)
        {

            dynamic _getList = null;

            try
            {
                _getList = objtblTransactionWrapperColletion.getTransactionList(userId);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "getTransactionList()", ex.Source, ex.Message, ex);
            }

            return _getList;
        }

        #endregion




    }
}