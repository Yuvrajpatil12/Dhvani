using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Business.DataAccess.Constants
{
    public class tblTransactionDBFields
    {


        public static string IU_Flag = "IU_Flag";




        public static string TableNameVal = "tblTransaction";
        public static string ID = "ID";
        public static string UserId = "UserId";
        public static string UUID = "UUID";
        public static string VoiceText = "VoiceText";
        public static string DisplayName = "DisplayName";
        public static string LocaleName = "LocaleName";
        public static string ShortName = "ShortName";
        public static string SpeakingStyle = "SpeakingStyle";
        public static string SpeakingSpeed = "SpeakingSpeed";
        public static string Pitch = "Pitch";
        public static string UserAPIKey = "UserAPIKey";
        public static string MP3URL = "MP3URL";
        public static string Mp3CreationDate = "Mp3CreationDate";
        public static string RequestDate = "RequestDate";
        public static string StatusId = "StatusId";
        public static string CreatedDate = "CreatedDate";
        public static string UpdatedDate = "UpdatedDate";
        public static string TransactionStatus = "TransactionStatus";
        public static string LoginName = "LoginName";
        public static string RowNumber = "RowNumber";
        public static string CallBackUrl = "CallBackUrl";
        public static string StatusMessage = "StatusMessage";
        public static string Language = "Language";
        public static string ArtistName = "ArtistName";
        public static string ProviderName = "ProviderName";
        public static string Accent = "Accent";
        public static string TransactionType = "TransactionType";
        public static string Duration = "Duration";

    }
    public class tblTransactionStoredProcedures
    {

        #region Object StoredProcedure

            




        public static string tblTransactionSAVE = "tblTransaction_SAVE";
        public static string tblTransactionGetRecordById = "tblTransaction_GetRecordById";

        public static string GettblTransactionRecords = "tblTransaction_GetRecords";
        public static string GettblTransactionRecordByValue = "tblTransaction_GetRecordByValue";
        public static string GettblTransactionRecordByValueArray = "tblTransaction_GetRecordByValueArray";
        public static string tblTransaction_GetTTSStatus = "GetTTSStatus";

        #endregion

        #region Collection StoredProcedure

        public static string tblTransactionSearch = "tblTransaction_Search";
        public static string tblTransactionSearchByValue = "tblTransaction_SearchByValue";
        public static string tblTransactionSearchByValueArray = "tblTransaction_SearchByValueArray";
        #endregion

        public static string IsExist = "";
        public static string GetCollectionForQuery = "";
        public static string SortingString = "SortOrder";



    }
}
