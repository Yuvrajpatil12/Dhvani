using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Business.DataAccess.Constants
{
    public class BestForMasterDBFields
    {

	
        public static string IU_Flag = "IU_Flag"; 


		

        public static string TableNameVal = "BestForMaster";
public static string  ID = "ID";
public static string  BestForName = "BestForName";
public static string  StatusId = "StatusId";
public static string  CreatedDate = "CreatedDate";
public static string  UpdateDate = "UpdateDate";

      
    }
    public class BestForMasterStoredProcedures
    {

        #region Object StoredProcedure

		



		
        public static string BestForMasterSAVE = "BestForMaster_SAVE";
        public static string BestForMasterGetRecordById = "BestForMaster_GetRecordById";

        public static string GetBestForMasterRecords = "BestForMaster_GetRecords";
        public static string GetBestForMasterRecordByValue =  "BestForMaster_GetRecordByValue";
        public static string GetBestForMasterRecordByValueArray = "BestForMaster_GetRecordByValueArray";
         
        #endregion

        #region Collection StoredProcedure
		 
        public static string BestForMasterSearch = "BestForMaster_Search";
        public static string BestForMasterSearchByValue =  "BestForMaster_SearchByValue";
        public static string BestForMasterSearchByValueArray = "BestForMaster_SearchByValueArray";
        #endregion
 
        public static string IsExist = "";
        public static string GetCollectionForQuery = "";
        public static string SortingString = "SortOrder";


      
    }
}
