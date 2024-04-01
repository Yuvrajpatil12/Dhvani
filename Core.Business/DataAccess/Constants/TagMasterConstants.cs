using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Business.DataAccess.Constants
{
    public class TagMasterDBFields
    {

	
        public static string IU_Flag = "IU_Flag"; 


		

        public static string TableNameVal = "TagMaster";
public static string  ID = "ID";
public static string  TagName = "TagName";
public static string  StatusId = "StatusId";
public static string  CreatedDate = "CreatedDate";
public static string  UpdateDate = "UpdateDate";

      
    }
    public class TagMasterStoredProcedures
    {

        #region Object StoredProcedure

		



		
        public static string TagMasterSAVE = "TagMaster_SAVE";
        public static string TagMasterGetRecordById = "TagMaster_GetRecordById";

        public static string GetTagMasterRecords = "TagMaster_GetRecords";
        public static string GetTagMasterRecordByValue =  "TagMaster_GetRecordByValue";
        public static string GetTagMasterRecordByValueArray = "TagMaster_GetRecordByValueArray";
         
        #endregion

        #region Collection StoredProcedure
		 
        public static string TagMasterSearch = "TagMaster_Search";
        public static string TagMasterSearchByValue =  "TagMaster_SearchByValue";
        public static string TagMasterSearchByValueArray = "TagMaster_SearchByValueArray";
        #endregion
 
        public static string IsExist = "";
        public static string GetCollectionForQuery = "";
        public static string SortingString = "SortOrder";


      
    }
}
