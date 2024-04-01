using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Business.DataAccess.Constants
{
    public class VMBestForDBFields
    {

	
        public static string IU_Flag = "IU_Flag"; 


		

        public static string TableNameVal = "VMBestFor";
public static string  ID = "ID";
public static string  VoiceMasterID = "VoiceMasterID";
public static string  BestForMasterID = "BestForMasterID";
public static string  StatusId = "StatusId";
public static string  CreatedDate = "CreatedDate";
public static string  UpdateDate = "UpdateDate";

      
    }
    public class VMBestForStoredProcedures
    {

        #region Object StoredProcedure

		



		
        public static string VMBestForSAVE = "VMBestFor_SAVE";
        public static string VMBestForUpdateRecords = "VMBestFor_UpdateRecords";
        public static string VMBestForGetRecordById = "VMBestFor_GetRecordById";
        public static string VMBestForGetSelectedBestForVoiceMasterById = "VMBestFor_GetSelectedBestForVoiceMasterById";

        public static string GetVMBestForRecords = "VMBestFor_GetRecords";
        public static string GetVMBestForRecordByValue =  "VMBestFor_GetRecordByValue";
        public static string GetVMBestForRecordByValueArray = "VMBestFor_GetRecordByValueArray";

        

        #endregion

        #region Collection StoredProcedure

        public static string VMBestForSearch = "VMBestFor_Search";
        public static string VMBestForSearchByValue =  "VMBestFor_SearchByValue";
        public static string VMBestForSearchByValueArray = "VMBestFor_SearchByValueArray";
        #endregion
 
        public static string IsExist = "";
        public static string GetCollectionForQuery = "";
        public static string SortingString = "SortOrder";


      
    }
}
