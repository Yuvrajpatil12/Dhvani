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
    public class BestForMasterBusinessFacade//:UniversalObject
    {
        dynamic objdynamicWrapper; 
		BestForMasterWrapperColletion objBestForMasterWrapperColletion = new BestForMasterWrapperColletion();
        BestForMasterWrapper objBestForMasterWrapper = new BestForMasterWrapper();
		private static readonly string _module = "Core.Business.BusinessFacade.BestForMasterBusinessFacade";
		public BestForMasterBusinessFacade()
        {
            
        }
        public BestForMasterBusinessFacade(dynamic WrapperType)
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

                objdynamicWrapper.objWrapperClass = objEntity;
                DataAccess.DataAccessLayer.DataAccess dalObject = new DataAccess.DataAccessLayer.DataAccess();
                Transaction TransObj = new Transaction(dalObject);
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

        public dynamic GetBestForMasterList()
        {
            dynamic _listBestForMaster = null;

            try
            {
                _listBestForMaster = objBestForMasterWrapperColletion.GetBestForMasterList();

                return _listBestForMaster;
            }
            catch (Exception ex)
            {

                Log.WriteLog(_module, "GetBestForMasterList()", ex.Source, ex.Message, ex);
            }

            return null;
        }

       



    }
}
