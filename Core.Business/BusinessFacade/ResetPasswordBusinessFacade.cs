using Core.Business.DataAccess.DataAccessLayer;
using Core.Business.DataAccess.Wrapper;
using Core.Entity;
using Core.Utility.Common;

namespace Core.Business.BusinessFacade
{
    public class ResetPasswordBusinessFacade//:UniversalObject
    {
        private dynamic objdynamicWrapper;
        private ResetPasswordWrapperColletion objResetPasswordWrapperColletion = new ResetPasswordWrapperColletion();
        private ResetPasswordWrapper objResetPasswordWrapper = new ResetPasswordWrapper();
        private static readonly string _module = "Core.Business.BusinessFacade.ResetPasswordBusinessFacade";


        private ResetPasswordWrapper objResetPassWrapper = new ResetPasswordWrapper();

        public ResetPassword objWrapperClass = new ResetPassword();


        public ResetPasswordBusinessFacade()
        {
        }

        public ResetPasswordBusinessFacade(dynamic WrapperType)
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
            if (objdynamicWrapper.GetRecordById(Id))
            {
                return objdynamicWrapper.objWrapperClass;
            }
            return null;
        }

        #endregion

        #region Login Methods

        public bool GetResetPassDetails(ResetPassword obj)
        {
            try
            {
                objResetPassWrapper = new ResetPasswordWrapper();
                return objResetPasswordWrapper.GetResetPassDetails(obj);
                //return true;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetResetPassDetails(PASS_RESETID=" + obj.PassResetCode + ")", ex.Source, ex.Message);
                return false;
            }
        }

        #endregion

        #region Delete Methods

        public bool Delete(ResetPassword obj)
        {
            try
            {
                ResetPasswordWrapper objResetWrapper = new ResetPasswordWrapper();
                DataAccess.DataAccessLayer.DataAccess dalObject = new DataAccess.DataAccessLayer.DataAccess();
                DataAccess.DataAccessLayer.Transaction ObjTransaction = new DataAccess.DataAccessLayer.Transaction(dalObject);
                ObjTransaction.ConnectionString = dbClass.SqlConnectString();
                Dictionary<string, Command> ObjCommands = new Dictionary<string, Command>();
                int commandCounter = 0;
                objResetWrapper.objWrapperClass = obj;
                bool result = objResetWrapper.Delete(ref ObjCommands, ref commandCounter);
                ObjTransaction.AddCommandList(ObjCommands);
                if (ObjTransaction.ExecuteTransaction())
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "Delete(int Id)", ex.Source, ex.Message);
                return false;
            }
        }
        #endregion
        public bool Save(dynamic objEntity)
        {
            try
            {
                objResetPasswordWrapper.objWrapperClass = objEntity;
                DataAccess.DataAccessLayer.DataAccess dalObject = new DataAccess.DataAccessLayer.DataAccess();
                DataAccess.DataAccessLayer.Transaction TransObj = new DataAccess.DataAccessLayer.Transaction(dalObject);
                TransObj.ConnectionString = dbClass.SqlConnectString();
                Dictionary<string, Command> CommandsObj = new Dictionary<string, Command>();
                int commandCounter = 0;

                bool result = objResetPasswordWrapper.Save(ref CommandsObj, ref commandCounter);
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
    }
}