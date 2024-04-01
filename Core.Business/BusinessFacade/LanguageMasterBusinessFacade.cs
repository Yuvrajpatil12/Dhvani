using Core.Business.DataAccess.DataAccessLayer;
using Core.Business.DataAccess.Wrapper;

namespace Core.Business.BusinessFacade
{
    public class LanguageMasterBusinessFacade//:UniversalObject
    {
        private dynamic objdynamicWrapper;
        private LanguageMasterWrapperColletion objLanguageMasterWrapperColletion = new LanguageMasterWrapperColletion();
        private LanguageMasterWrapper objLanguageMasterWrapper = new LanguageMasterWrapper();
        private static readonly string _module = "Core.Business.BusinessFacade.LanguageMasterBusinessFacade";

        public LanguageMasterBusinessFacade()
        {
        }

        public LanguageMasterBusinessFacade(dynamic WrapperType)
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

        public dynamic GetAllRecordsList()
        {
            string[,] Sort = new string[1, 2];
            if (objLanguageMasterWrapperColletion.GetRecords(false, Sort))
            {
                return objLanguageMasterWrapperColletion.Items;
            }
            return null;
        }
    }
}