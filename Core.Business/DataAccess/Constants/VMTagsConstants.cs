using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Business.DataAccess.Constants
{
    public class VMTagsDBFields
    {


        public static string IU_Flag = "IU_Flag";




        public static string TableNameVal = "VMTags";
        public static string ID = "ID";
        public static string VoiceMasterID = "VoiceMasterID";
        public static string TagMasterID = "TagMasterID";
        public static string StatusId = "StatusId";
        public static string CreatedDate = "CreatedDate";
        public static string UpdateDate = "UpdateDate";


    }
    public class VMTagsStoredProcedures
    {

        #region Object StoredProcedure






        public static string VMTagsSAVE = "VMTags_SAVE";
        public static string VMTagsUpdateRecordsByVoiceMaterId = "VMTags_UpdateRecordsByVoiceMaterId";
        public static string VMTagsGetRecordById = "VMTags_GetRecordById";
        public static string VMTagsGetSelectedTagsVoiceMasterById = "VMTags_GetSelectedTagsVoiceMasterById";

        public static string GetVMTagsRecords = "VMTags_GetRecords";
        public static string GetVMTagsRecordByValue = "VMTags_GetRecordByValue";
        public static string GetVMTagsRecordByValueArray = "VMTags_GetRecordByValueArray";

        #endregion

        #region Collection StoredProcedure

        public static string VMTagsSearch = "VMTags_Search";
        public static string VMTagsSearchByValue = "VMTags_SearchByValue";
        public static string VMTagsSearchByValueArray = "VMTags_SearchByValueArray";
        #endregion

        public static string IsExist = "";
        public static string GetCollectionForQuery = "";
        public static string SortingString = "SortOrder";



    }
}
