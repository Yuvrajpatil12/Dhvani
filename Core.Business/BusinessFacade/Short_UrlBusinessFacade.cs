using Core.Business.DataAccess.DataAccessLayer;
using Core.Business.DataAccess.Wrapper;
using Core.Entity;
using Core.Utility.Common;

namespace Core.Business.BusinessFacade
{
    public class Short_UrlBusinessFacade//:UniversalObject
    {
        private dynamic objdynamicWrapper;
        private Short_UrlWrapperColletion objShort_UrlWrapperColletion = new Short_UrlWrapperColletion();
        private Short_UrlWrapper objShort_UrlWrapper = new Short_UrlWrapper();
        private static readonly string _module = "Core.Business.BusinessFacade.Short_UrlBusinessFacade";

        public Short_UrlBusinessFacade()
        {
        }

        public Short_UrlBusinessFacade(dynamic WrapperType)
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

        public string MakeShortURL(string strURL)
        {
            string shortAddress = strURL;
            try
            {
                Short_Url objShort_Url = new Short_Url();
                objShort_Url.URLString = GetShortURLFromPath(strURL);
                if (objShort_Url.URLString != strURL)
                    shortAddress = objShort_Url.URLString;
                else
                {
                    Save(objShort_Url);
                    shortAddress = ConstantsCommon.shortURL + objShort_Url.KeyValue;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "MakeShortURL(strURL : " + strURL + ")", ex.Source, ex.Message, ex);
            }
            return shortAddress;
        }

        public string GetShortURLFromPath(string strURL)
        {
            string shortAddress = strURL;
            try
            {
                Short_Url objShort_Url = new Short_Url();
                objShort_Url = GetShortURLKey(strURL);
                if (!string.IsNullOrWhiteSpace(objShort_Url.KeyValue))
                    shortAddress = ConstantsCommon.shortURL + objShort_Url.KeyValue;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetShortURLFromPath(strURL : " + strURL + ")", ex.Source, ex.Message, ex);
            }
            return shortAddress;
        }
        public Short_Url GetShortURLKey(string URLString)
        {
            if (objShort_UrlWrapper.GetShortURLKey(URLString))
            {
                return objShort_UrlWrapper.objWrapperClass;
            }
            return new Short_Url();
        }

        public bool Save(dynamic objEntity)
        {
            try
            {
                objShort_UrlWrapper.objWrapperClass = objEntity;
                DataAccess.DataAccessLayer.DataAccess dalObject = new DataAccess.DataAccessLayer.DataAccess();
                DataAccess.DataAccessLayer.Transaction TransObj = new DataAccess.DataAccessLayer.Transaction(dalObject);
                TransObj.ConnectionString = dbClass.SqlConnectString();
                Dictionary<string, Command> CommandsObj = new Dictionary<string, Command>();
                int commandCounter = 0;

                bool result = objShort_UrlWrapper.Save(ref CommandsObj, ref commandCounter);
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