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
    public class VMTagsBusinessFacade//:UniversalObject
    {
        dynamic objdynamicWrapper; 
		VMTagsWrapperColletion objVMTagsWrapperColletion = new VMTagsWrapperColletion();
        VMTagsWrapper objVMTagsWrapper = new VMTagsWrapper();
		private static readonly string _module = "Core.Business.BusinessFacade.VMTagsBusinessFacade";
		public VMTagsBusinessFacade()
        {
            
        }
        public VMTagsBusinessFacade(dynamic WrapperType)
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
        public bool Save(dynamic objEntity)
        {
            try
            {

                objVMTagsWrapper.objWrapperClass = objEntity;
                DataAccess.DataAccessLayer.DataAccess dalObject = new DataAccess.DataAccessLayer.DataAccess();
                Transaction TransObj = new Transaction(dalObject);
                TransObj.ConnectionString = dbClass.SqlConnectString();
                Dictionary<string, Command> CommandsObj = new Dictionary<string, Command>();
                int commandCounter = 0;

                bool result = objVMTagsWrapper.Save(ref CommandsObj, ref commandCounter);
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
        public bool UpdateRecords(dynamic objEntity)
        {
            try
            {

                objVMTagsWrapper.objWrapperClass = objEntity;
                DataAccess.DataAccessLayer.DataAccess dalObject = new DataAccess.DataAccessLayer.DataAccess();
                Transaction TransObj = new Transaction(dalObject);
                TransObj.ConnectionString = dbClass.SqlConnectString();
                Dictionary<string, Command> CommandsObj = new Dictionary<string, Command>();
                int commandCounter = 0;

                bool result = objVMTagsWrapper.UpdateRecords(ref CommandsObj, ref commandCounter);
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

        public dynamic VmTagMasterGetRecordById(string VoiceMasterId)
        {

            dynamic _listTagMaster = null;

            try
            {
                _listTagMaster = objVMTagsWrapperColletion.VmTagMasterGetRecordById(VoiceMasterId);

                return _listTagMaster;
            }
            catch (Exception ex)
            {

                Log.WriteLog(_module, "TagMasterGetRecordById()", ex.Source, ex.Message, ex);
            }

            return null;
        }

        /// <summary>
        /// Get Selected Tag Master Record By Voice Master Id For Voice Edit click
        /// </summary>
        /// <param name="VoiceMasterId"></param>
        /// <returns></returns>
        public dynamic VMTagsGetSelectedTagsVoiceMasterById(string VoiceMasterId)
        {

            dynamic _listTagMaster = null;

            try
            {
                _listTagMaster = objVMTagsWrapperColletion.VMTagsGetSelectedTagsVoiceMasterById(VoiceMasterId);

                return _listTagMaster;
            }
            catch (Exception ex)
            {

                Log.WriteLog(_module, "VMTagsGetSelectedTagsVoiceMasterById()", ex.Source, ex.Message, ex);
            }

            return null;
        }



    }
}
