using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Business.DataAccess.Constants;
using System.Data.SqlClient;
using System.Data;
using Core.Entity;
using Core.Entity.Common;
using Core.Utility.Common;

namespace Core.Business.DataAccess.Mapper
{
    public class tblTransactionDataMapper
    {
        private static readonly string _module = "Core.Business.DataAccess.Mapper.tblTransactionDataMapper";
        private tblTransaction objtblTransaction = null;

        public tblTransaction GetDetails(SqlDataReader sqlDataReader)
        {
            try
            {
                objtblTransaction = new tblTransaction();

                if (sqlDataReader.HasColumn(tblTransactionDBFields.ID))
                    objtblTransaction.ID = (sqlDataReader[tblTransactionDBFields.ID] != DBNull.Value ? Convert.ToInt32(sqlDataReader[tblTransactionDBFields.ID]) : 0);
                if (sqlDataReader.HasColumn(tblTransactionDBFields.UUID))
                    objtblTransaction.UUID = (sqlDataReader[tblTransactionDBFields.UUID] != DBNull.Value ? Convert.ToString(sqlDataReader[tblTransactionDBFields.UUID]) : string.Empty);
                if (sqlDataReader.HasColumn(tblTransactionDBFields.VoiceText))
                    objtblTransaction.VoiceText = (sqlDataReader[tblTransactionDBFields.VoiceText] != DBNull.Value ? Convert.ToString(sqlDataReader[tblTransactionDBFields.VoiceText]) : string.Empty);
                if (sqlDataReader.HasColumn(tblTransactionDBFields.DisplayName))
                    objtblTransaction.DisplayName = (sqlDataReader[tblTransactionDBFields.DisplayName] != DBNull.Value ? Convert.ToString(sqlDataReader[tblTransactionDBFields.DisplayName]) : string.Empty);
                if (sqlDataReader.HasColumn(tblTransactionDBFields.LocaleName))
                    objtblTransaction.LocaleName = (sqlDataReader[tblTransactionDBFields.LocaleName] != DBNull.Value ? Convert.ToString(sqlDataReader[tblTransactionDBFields.LocaleName]) : string.Empty);
                if (sqlDataReader.HasColumn(tblTransactionDBFields.ShortName))
                    objtblTransaction.ShortName = (sqlDataReader[tblTransactionDBFields.ShortName] != DBNull.Value ? Convert.ToString(sqlDataReader[tblTransactionDBFields.ShortName]) : string.Empty);
                if (sqlDataReader.HasColumn(tblTransactionDBFields.SpeakingStyle))
                    objtblTransaction.SpeakingStyle = (sqlDataReader[tblTransactionDBFields.SpeakingStyle] != DBNull.Value ? Convert.ToString(sqlDataReader[tblTransactionDBFields.SpeakingStyle]) : string.Empty);
                if (sqlDataReader.HasColumn(tblTransactionDBFields.SpeakingSpeed))
                    objtblTransaction.SpeakingSpeed = (sqlDataReader[tblTransactionDBFields.SpeakingSpeed] != DBNull.Value ? Convert.ToString(sqlDataReader[tblTransactionDBFields.SpeakingSpeed]) : string.Empty);
                if (sqlDataReader.HasColumn(tblTransactionDBFields.Pitch))
                    objtblTransaction.Pitch = (sqlDataReader[tblTransactionDBFields.Pitch] != DBNull.Value ? Convert.ToString(sqlDataReader[tblTransactionDBFields.Pitch]) : string.Empty);
                if (sqlDataReader.HasColumn(tblTransactionDBFields.UserAPIKey))
                    objtblTransaction.UserAPIKey = (sqlDataReader[tblTransactionDBFields.UserAPIKey] != DBNull.Value ? Convert.ToString(sqlDataReader[tblTransactionDBFields.UserAPIKey]) : string.Empty);
                if (sqlDataReader.HasColumn(tblTransactionDBFields.MP3URL))
                    objtblTransaction.MP3URL = (sqlDataReader[tblTransactionDBFields.MP3URL] != DBNull.Value ? Convert.ToString(sqlDataReader[tblTransactionDBFields.MP3URL]) : string.Empty);
                if (sqlDataReader.HasColumn(tblTransactionDBFields.Mp3CreationDate))
                    objtblTransaction.Mp3CreationDate = (sqlDataReader[tblTransactionDBFields.Mp3CreationDate] != DBNull.Value ? Convert.ToString(sqlDataReader[tblTransactionDBFields.Mp3CreationDate]) : string.Empty);
                if (sqlDataReader.HasColumn(tblTransactionDBFields.RequestDate))
                    objtblTransaction.RequestDate = (sqlDataReader[tblTransactionDBFields.RequestDate] != DBNull.Value ? Convert.ToString(sqlDataReader[tblTransactionDBFields.RequestDate]) : string.Empty);
                if (sqlDataReader.HasColumn(tblTransactionDBFields.StatusId))
                    objtblTransaction.StatusId = (sqlDataReader[tblTransactionDBFields.StatusId] != DBNull.Value ? Convert.ToBoolean(sqlDataReader[tblTransactionDBFields.StatusId]) : false);
                if (sqlDataReader.HasColumn(tblTransactionDBFields.CreatedDate))
                    objtblTransaction.CreatedDate = (sqlDataReader[tblTransactionDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(sqlDataReader[tblTransactionDBFields.CreatedDate]) : DateTime.Now);
                if (sqlDataReader.HasColumn(tblTransactionDBFields.UpdatedDate))
                    objtblTransaction.UpdatedDate = (sqlDataReader[tblTransactionDBFields.UpdatedDate] != DBNull.Value ? Convert.ToDateTime(sqlDataReader[tblTransactionDBFields.UpdatedDate]) : DateTime.Now);
                if (sqlDataReader.HasColumn(tblTransactionDBFields.TransactionStatus))
                    objtblTransaction.TransactionStatus = (sqlDataReader[tblTransactionDBFields.TransactionStatus] != DBNull.Value ? Convert.ToString(sqlDataReader[tblTransactionDBFields.TransactionStatus]) : string.Empty);
                if (sqlDataReader.HasColumn(tblTransactionDBFields.RowNumber))
                    objtblTransaction.RowNumber = (sqlDataReader[tblTransactionDBFields.RowNumber] != DBNull.Value ? Convert.ToInt32(sqlDataReader[tblTransactionDBFields.RowNumber]) : 0);
                if (sqlDataReader.HasColumn(tblTransactionDBFields.LoginName))
                    objtblTransaction.LoginName = (sqlDataReader[tblTransactionDBFields.LoginName] != DBNull.Value ? Convert.ToString(sqlDataReader[tblTransactionDBFields.LoginName]) : string.Empty);
                if (sqlDataReader.HasColumn(tblTransactionDBFields.StatusMessage))
                    objtblTransaction.StatusMessage = (sqlDataReader[tblTransactionDBFields.StatusMessage] != DBNull.Value ? Convert.ToString(sqlDataReader[tblTransactionDBFields.StatusMessage]) : string.Empty);
                if (sqlDataReader.HasColumn(tblTransactionDBFields.Language))
                    objtblTransaction.Language = (sqlDataReader[tblTransactionDBFields.Language] != DBNull.Value ? Convert.ToString(sqlDataReader[tblTransactionDBFields.Language]) : string.Empty);
                if (sqlDataReader.HasColumn(tblTransactionDBFields.ArtistName))
                    objtblTransaction.ArtistName = (sqlDataReader[tblTransactionDBFields.ArtistName] != DBNull.Value ? Convert.ToString(sqlDataReader[tblTransactionDBFields.ArtistName]) : string.Empty);
                if (sqlDataReader.HasColumn(tblTransactionDBFields.ProviderName))
                    objtblTransaction.ProviderName = (sqlDataReader[tblTransactionDBFields.ProviderName] != DBNull.Value ? Convert.ToString(sqlDataReader[tblTransactionDBFields.ProviderName]) : string.Empty);
                if (sqlDataReader.HasColumn(tblTransactionDBFields.Accent))
                    objtblTransaction.Accent = (sqlDataReader[tblTransactionDBFields.Accent] != DBNull.Value ? Convert.ToString(sqlDataReader[tblTransactionDBFields.Accent]) : string.Empty);
                if (sqlDataReader.HasColumn(tblTransactionDBFields.TransactionType))
                    objtblTransaction.TransactionType = (sqlDataReader[tblTransactionDBFields.TransactionType] != DBNull.Value ? Convert.ToString(sqlDataReader[tblTransactionDBFields.TransactionType]) : string.Empty);
                if (sqlDataReader.HasColumn(tblTransactionDBFields.Duration))
                    objtblTransaction.Duration = (sqlDataReader[tblTransactionDBFields.Duration] != DBNull.Value ? Convert.ToString(sqlDataReader[tblTransactionDBFields.Duration]) : string.Empty);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(sqlDataReader)", ex.Source, ex.Message, ex);
            }
            return objtblTransaction;
        }

       
        public List<tblTransaction> GetDetailsList(SqlDataReader sqlDataReader)
        {
            List<tblTransaction> list = new List<tblTransaction>();
            try
            {
                while (sqlDataReader.Read())
                {
                    objtblTransaction = GetDetails(sqlDataReader);
                    list.Add(objtblTransaction);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetailsList(sqlDataReader)", ex.Source, ex.Message, ex);
            }
            return list;
        }

        public List<tblTransaction> GetDetails(DataSet dataSet)
        {
            List<tblTransaction> tblTransactions = new List<tblTransaction>();

            try
            {
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drow in dataSet.Tables[0].Rows)
                    {
                        objtblTransaction = new tblTransaction();

                        if (drow.Table.Columns.Contains(tblTransactionDBFields.ID))
                            objtblTransaction.ID = (drow[tblTransactionDBFields.ID] != DBNull.Value ? Convert.ToInt32(drow[tblTransactionDBFields.ID]) : 0);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.UUID))
                            objtblTransaction.UUID = (drow[tblTransactionDBFields.UUID] != DBNull.Value ? Convert.ToString(drow[tblTransactionDBFields.UUID]) : string.Empty);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.VoiceText))
                            objtblTransaction.VoiceText = (drow[tblTransactionDBFields.VoiceText] != DBNull.Value ? Convert.ToString(drow[tblTransactionDBFields.VoiceText]) : string.Empty);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.DisplayName))
                            objtblTransaction.DisplayName = (drow[tblTransactionDBFields.DisplayName] != DBNull.Value ? Convert.ToString(drow[tblTransactionDBFields.DisplayName]) : string.Empty);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.LocaleName))
                            objtblTransaction.LocaleName = (drow[tblTransactionDBFields.LocaleName] != DBNull.Value ? Convert.ToString(drow[tblTransactionDBFields.LocaleName]) : string.Empty);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.ShortName))
                            objtblTransaction.ShortName = (drow[tblTransactionDBFields.ShortName] != DBNull.Value ? Convert.ToString(drow[tblTransactionDBFields.ShortName]) : string.Empty);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.SpeakingStyle))
                            objtblTransaction.SpeakingStyle = (drow[tblTransactionDBFields.SpeakingStyle] != DBNull.Value ? Convert.ToString(drow[tblTransactionDBFields.SpeakingStyle]) : string.Empty);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.SpeakingSpeed))
                            objtblTransaction.SpeakingSpeed = (drow[tblTransactionDBFields.SpeakingSpeed] != DBNull.Value ? Convert.ToString(drow[tblTransactionDBFields.SpeakingSpeed]) : string.Empty);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.Pitch))
                            objtblTransaction.Pitch = (drow[tblTransactionDBFields.Pitch] != DBNull.Value ? Convert.ToString(drow[tblTransactionDBFields.Pitch]) : string.Empty);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.UserAPIKey))
                            objtblTransaction.UserAPIKey = (drow[tblTransactionDBFields.UserAPIKey] != DBNull.Value ? Convert.ToString(drow[tblTransactionDBFields.UserAPIKey]) : string.Empty);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.MP3URL))
                            objtblTransaction.MP3URL = (drow[tblTransactionDBFields.MP3URL] != DBNull.Value ? Convert.ToString(drow[tblTransactionDBFields.MP3URL]) : string.Empty);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.Mp3CreationDate))
                            objtblTransaction.Mp3CreationDate = (drow[tblTransactionDBFields.Mp3CreationDate] != DBNull.Value ? Convert.ToString(drow[tblTransactionDBFields.Mp3CreationDate]) : string.Empty);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.RequestDate))
                            objtblTransaction.RequestDate = (drow[tblTransactionDBFields.RequestDate] != DBNull.Value ? Convert.ToString(drow[tblTransactionDBFields.RequestDate]) : string.Empty);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.StatusId))
                            objtblTransaction.StatusId = (drow[tblTransactionDBFields.StatusId] != DBNull.Value ? Convert.ToBoolean(drow[tblTransactionDBFields.StatusId]) : false);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.CreatedDate))
                            objtblTransaction.CreatedDate = (drow[tblTransactionDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(drow[tblTransactionDBFields.CreatedDate]) : DateTime.Now);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.UpdatedDate))
                            objtblTransaction.UpdatedDate = (drow[tblTransactionDBFields.UpdatedDate] != DBNull.Value ? Convert.ToDateTime(drow[tblTransactionDBFields.UpdatedDate]) : DateTime.Now);


                        tblTransactions.Add(objtblTransaction);
                    }
                }

            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(dataSet)", ex.Source, ex.Message, ex);
            }

            return tblTransactions;
        }

        public tblTransaction GetDetailsobj(DataSet dataSet)
        {
            tblTransaction objtblTransaction = new tblTransaction();

            try
            {
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drow in dataSet.Tables[0].Rows)
                    {
                        objtblTransaction = new tblTransaction();

                        if (drow.Table.Columns.Contains(tblTransactionDBFields.ID))
                            objtblTransaction.ID = (drow[tblTransactionDBFields.ID] != DBNull.Value ? Convert.ToInt32(drow[tblTransactionDBFields.ID]) : 0);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.UUID))
                            objtblTransaction.UUID = (drow[tblTransactionDBFields.UUID] != DBNull.Value ? Convert.ToString(drow[tblTransactionDBFields.UUID]) : string.Empty);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.VoiceText))
                            objtblTransaction.VoiceText = (drow[tblTransactionDBFields.VoiceText] != DBNull.Value ? Convert.ToString(drow[tblTransactionDBFields.VoiceText]) : string.Empty);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.DisplayName))
                            objtblTransaction.DisplayName = (drow[tblTransactionDBFields.DisplayName] != DBNull.Value ? Convert.ToString(drow[tblTransactionDBFields.DisplayName]) : string.Empty);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.LocaleName))
                            objtblTransaction.LocaleName = (drow[tblTransactionDBFields.LocaleName] != DBNull.Value ? Convert.ToString(drow[tblTransactionDBFields.LocaleName]) : string.Empty);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.ShortName))
                            objtblTransaction.ShortName = (drow[tblTransactionDBFields.ShortName] != DBNull.Value ? Convert.ToString(drow[tblTransactionDBFields.ShortName]) : string.Empty);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.SpeakingStyle))
                            objtblTransaction.SpeakingStyle = (drow[tblTransactionDBFields.SpeakingStyle] != DBNull.Value ? Convert.ToString(drow[tblTransactionDBFields.SpeakingStyle]) : string.Empty);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.SpeakingSpeed))
                            objtblTransaction.SpeakingSpeed = (drow[tblTransactionDBFields.SpeakingSpeed] != DBNull.Value ? Convert.ToString(drow[tblTransactionDBFields.SpeakingSpeed]) : string.Empty);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.Pitch))
                            objtblTransaction.Pitch = (drow[tblTransactionDBFields.Pitch] != DBNull.Value ? Convert.ToString(drow[tblTransactionDBFields.Pitch]) : string.Empty);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.UserAPIKey))
                            objtblTransaction.UserAPIKey = (drow[tblTransactionDBFields.UserAPIKey] != DBNull.Value ? Convert.ToString(drow[tblTransactionDBFields.UserAPIKey]) : string.Empty);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.MP3URL))
                            objtblTransaction.MP3URL = (drow[tblTransactionDBFields.MP3URL] != DBNull.Value ? Convert.ToString(drow[tblTransactionDBFields.MP3URL]) : string.Empty);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.Mp3CreationDate))
                            objtblTransaction.Mp3CreationDate = (drow[tblTransactionDBFields.Mp3CreationDate] != DBNull.Value ? Convert.ToString(drow[tblTransactionDBFields.Mp3CreationDate]) : string.Empty);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.RequestDate))
                            objtblTransaction.RequestDate = (drow[tblTransactionDBFields.RequestDate] != DBNull.Value ? Convert.ToString(drow[tblTransactionDBFields.RequestDate]) : string.Empty);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.StatusId))
                            objtblTransaction.StatusId = (drow[tblTransactionDBFields.StatusId] != DBNull.Value ? Convert.ToBoolean(drow[tblTransactionDBFields.StatusId]) : false);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.CreatedDate))
                            objtblTransaction.CreatedDate = (drow[tblTransactionDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(drow[tblTransactionDBFields.CreatedDate]) : DateTime.Now);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.UpdatedDate))
                            objtblTransaction.UpdatedDate = (drow[tblTransactionDBFields.UpdatedDate] != DBNull.Value ? Convert.ToDateTime(drow[tblTransactionDBFields.UpdatedDate]) : DateTime.Now);


                    }
                }

            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(dataSet)", ex.Source, ex.Message, ex);
            }

            return objtblTransaction;
        }

        public tblTransaction GetDetails(DataTable dataTable)
        {
            tblTransaction objtblTransaction = new tblTransaction();

            try
            {
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow drow in dataTable.Rows)
                    {
                        objtblTransaction = new tblTransaction();

                        if (drow.Table.Columns.Contains(tblTransactionDBFields.ID))
                            objtblTransaction.ID = (drow[tblTransactionDBFields.ID] != DBNull.Value ? Convert.ToInt32(drow[tblTransactionDBFields.ID]) : 0);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.UUID))
                            objtblTransaction.UUID = (drow[tblTransactionDBFields.UUID] != DBNull.Value ? Convert.ToString(drow[tblTransactionDBFields.UUID]) : string.Empty);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.VoiceText))
                            objtblTransaction.VoiceText = (drow[tblTransactionDBFields.VoiceText] != DBNull.Value ? Convert.ToString(drow[tblTransactionDBFields.VoiceText]) : string.Empty);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.DisplayName))
                            objtblTransaction.DisplayName = (drow[tblTransactionDBFields.DisplayName] != DBNull.Value ? Convert.ToString(drow[tblTransactionDBFields.DisplayName]) : string.Empty);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.LocaleName))
                            objtblTransaction.LocaleName = (drow[tblTransactionDBFields.LocaleName] != DBNull.Value ? Convert.ToString(drow[tblTransactionDBFields.LocaleName]) : string.Empty);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.ShortName))
                            objtblTransaction.ShortName = (drow[tblTransactionDBFields.ShortName] != DBNull.Value ? Convert.ToString(drow[tblTransactionDBFields.ShortName]) : string.Empty);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.SpeakingStyle))
                            objtblTransaction.SpeakingStyle = (drow[tblTransactionDBFields.SpeakingStyle] != DBNull.Value ? Convert.ToString(drow[tblTransactionDBFields.SpeakingStyle]) : string.Empty);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.SpeakingSpeed))
                            objtblTransaction.SpeakingSpeed = (drow[tblTransactionDBFields.SpeakingSpeed] != DBNull.Value ? Convert.ToString(drow[tblTransactionDBFields.SpeakingSpeed]) : string.Empty);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.Pitch))
                            objtblTransaction.Pitch = (drow[tblTransactionDBFields.Pitch] != DBNull.Value ? Convert.ToString(drow[tblTransactionDBFields.Pitch]) : string.Empty);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.UserAPIKey))
                            objtblTransaction.UserAPIKey = (drow[tblTransactionDBFields.UserAPIKey] != DBNull.Value ? Convert.ToString(drow[tblTransactionDBFields.UserAPIKey]) : string.Empty);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.MP3URL))
                            objtblTransaction.MP3URL = (drow[tblTransactionDBFields.MP3URL] != DBNull.Value ? Convert.ToString(drow[tblTransactionDBFields.MP3URL]) : string.Empty);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.Mp3CreationDate))
                            objtblTransaction.Mp3CreationDate = (drow[tblTransactionDBFields.Mp3CreationDate] != DBNull.Value ? Convert.ToString(drow[tblTransactionDBFields.Mp3CreationDate]) : string.Empty);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.RequestDate))
                            objtblTransaction.RequestDate = (drow[tblTransactionDBFields.RequestDate] != DBNull.Value ? Convert.ToString(drow[tblTransactionDBFields.RequestDate]) : string.Empty);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.StatusId))
                            objtblTransaction.StatusId = (drow[tblTransactionDBFields.StatusId] != DBNull.Value ? Convert.ToBoolean(drow[tblTransactionDBFields.StatusId]) : false);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.CreatedDate))
                            objtblTransaction.CreatedDate = (drow[tblTransactionDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(drow[tblTransactionDBFields.CreatedDate]) : DateTime.Now);
                        if (drow.Table.Columns.Contains(tblTransactionDBFields.UpdatedDate))
                            objtblTransaction.UpdatedDate = (drow[tblTransactionDBFields.UpdatedDate] != DBNull.Value ? Convert.ToDateTime(drow[tblTransactionDBFields.UpdatedDate]) : DateTime.Now);


                    }
                }

            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(DataTable)", ex.Source, ex.Message, ex);
            }

            return objtblTransaction;
        }

    }
}
