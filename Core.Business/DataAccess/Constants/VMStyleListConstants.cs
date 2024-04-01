using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Business.DataAccess.Constants
{
    public class VMStyleListDBFields
    {

	
        public static string IU_Flag = "IU_Flag"; 


		

        public static string TableNameVal = "VMStyleList";
public static string  ID = "ID";
public static string  VoiceMasterID = "VoiceMasterID";
public static string  StyleName = "StyleName";
public static string  StatusId = "StatusId";
public static string  CreatedDate = "CreatedDate";
public static string  UpdateDate = "UpdateDate";

      
    }
    public class VMStyleListStoredProcedures
    {

        #region Object StoredProcedure

		



		
        public static string VMStyleListSAVE = "VMStyleList_SAVE";
        public static string VMStyleListGetRecordById = "VMStyleList_GetRecordById";

        public static string GetVMStyleListRecords = "VMStyleList_GetRecords";
        public static string GetVMStyleListRecordByValue =  "VMStyleList_GetRecordByValue";
        public static string GetVMStyleListRecordByValueArray = "VMStyleList_GetRecordByValueArray";
         
        #endregion

        #region Collection StoredProcedure
		 
        public static string VMStyleListSearch = "VMStyleList_Search";
        public static string VMStyleListSearchByValue =  "VMStyleList_SearchByValue";
        public static string VMStyleListSearchByValueArray = "VMStyleList_SearchByValueArray";
        #endregion
 
        public static string IsExist = "";
        public static string GetCollectionForQuery = "";
        public static string SortingString = "SortOrder";


      
    }
}
