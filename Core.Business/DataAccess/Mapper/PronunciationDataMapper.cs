using Core.Business.DataAccess.Constants;
using Core.Entity;
using Core.Utility.Common;
using System.Data;
using System.Data.SqlClient;

namespace Core.Business.DataAccess.Mapper
{
    public class PronunciationDataMapper
    {
        private static readonly string _module = "Core.Business.DataAccess.Mapper.PronunciationDataMapper";
        private Pronunciation objPronunciation = null;

        public Pronunciation GetDetails(SqlDataReader sqlDataReader)
        {
            try
            {
                objPronunciation = new Pronunciation();

                if (sqlDataReader.HasColumn(PronunciationDBFields.ID))
                    objPronunciation.ID = (sqlDataReader[PronunciationDBFields.ID] != DBNull.Value ? Convert.ToInt32(sqlDataReader[PronunciationDBFields.ID]) : 0);
                if (sqlDataReader.HasColumn(PronunciationDBFields.UserID))
                    objPronunciation.UserID = (sqlDataReader[PronunciationDBFields.UserID] != DBNull.Value ? Convert.ToInt32(sqlDataReader[PronunciationDBFields.UserID]) : 0);
                if (sqlDataReader.HasColumn(PronunciationDBFields.InitialText))
                    objPronunciation.InitialText = (sqlDataReader[PronunciationDBFields.InitialText] != DBNull.Value ? Convert.ToString(sqlDataReader[PronunciationDBFields.InitialText]) : string.Empty);
                if (sqlDataReader.HasColumn(PronunciationDBFields.Accent))
                    objPronunciation.Accent = (sqlDataReader[PronunciationDBFields.Accent] != DBNull.Value ? Convert.ToString(sqlDataReader[PronunciationDBFields.Accent]) : string.Empty);
                if (sqlDataReader.HasColumn(PronunciationDBFields.AlternateText))
                    objPronunciation.AlternateText = (sqlDataReader[PronunciationDBFields.AlternateText] != DBNull.Value ? Convert.ToString(sqlDataReader[PronunciationDBFields.AlternateText]) : string.Empty);
                if (sqlDataReader.HasColumn(PronunciationDBFields.AccentCode))
                    objPronunciation.AccentCode = (sqlDataReader[PronunciationDBFields.AccentCode] != DBNull.Value ? Convert.ToString(sqlDataReader[PronunciationDBFields.AccentCode]) : string.Empty);
                if (sqlDataReader.HasColumn(PronunciationDBFields.Locale))
                    objPronunciation.Locale = (sqlDataReader[PronunciationDBFields.Locale] != DBNull.Value ? Convert.ToString(sqlDataReader[PronunciationDBFields.Locale]) : string.Empty);
                if (sqlDataReader.HasColumn(PronunciationDBFields.LocaleCode))
                    objPronunciation.LocaleCode = (sqlDataReader[PronunciationDBFields.LocaleCode] != DBNull.Value ? Convert.ToString(sqlDataReader[PronunciationDBFields.LocaleCode]) : string.Empty);
                if (sqlDataReader.HasColumn(PronunciationDBFields.StatusId))
                    objPronunciation.StatusId = (sqlDataReader[PronunciationDBFields.StatusId] != DBNull.Value ? Convert.ToByte(sqlDataReader[PronunciationDBFields.StatusId]) : (byte)0);
                if (sqlDataReader.HasColumn(PronunciationDBFields.CreatedDate))
                    objPronunciation.CreatedDate = (sqlDataReader[PronunciationDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(sqlDataReader[PronunciationDBFields.CreatedDate]) : DateTime.Now);
                if (sqlDataReader.HasColumn(PronunciationDBFields.UpdateDate))
                    objPronunciation.UpdateDate = (sqlDataReader[PronunciationDBFields.UpdateDate] != DBNull.Value ? Convert.ToDateTime(sqlDataReader[PronunciationDBFields.UpdateDate]) : DateTime.Now);
                if (sqlDataReader.HasColumn(PronunciationDBFields.UUID))
                    objPronunciation.UUID = (sqlDataReader[PronunciationDBFields.UUID] != DBNull.Value ? Convert.ToString(sqlDataReader[PronunciationDBFields.UUID]) : string.Empty);
                if (sqlDataReader.HasColumn(PronunciationDBFields.VMUUID))
                    objPronunciation.VMUUID = (sqlDataReader[PronunciationDBFields.VMUUID] != DBNull.Value ? Convert.ToString(sqlDataReader[PronunciationDBFields.VMUUID]) : string.Empty);
                if (sqlDataReader.HasColumn(PronunciationDBFields.DisplayName))
                    objPronunciation.DisplayName = (sqlDataReader[PronunciationDBFields.DisplayName] != DBNull.Value ? Convert.ToString(sqlDataReader[PronunciationDBFields.DisplayName]) : string.Empty);

                if (sqlDataReader.HasColumn(PronunciationDBFields.Gender))
                    objPronunciation.Gender = (sqlDataReader[PronunciationDBFields.Gender] != DBNull.Value ? Convert.ToString(sqlDataReader[PronunciationDBFields.Gender]) : string.Empty);
                if (sqlDataReader.HasColumn(PronunciationDBFields.SampleUrl))
                    objPronunciation.SampleUrl = (sqlDataReader[PronunciationDBFields.SampleUrl] != DBNull.Value ? Convert.ToString(sqlDataReader[PronunciationDBFields.SampleUrl]) : string.Empty);
                if (sqlDataReader.HasColumn(PronunciationDBFields.AgeBracket))
                    objPronunciation.AgeBracket = (sqlDataReader[PronunciationDBFields.AgeBracket] != DBNull.Value ? Convert.ToString(sqlDataReader[PronunciationDBFields.AgeBracket]) : string.Empty);

                if (sqlDataReader.HasColumn(PronunciationDBFields.VoiceImage))
                    objPronunciation.VoiceImage = (sqlDataReader[PronunciationDBFields.VoiceImage] != DBNull.Value ? Convert.ToString(sqlDataReader[PronunciationDBFields.VoiceImage]) : string.Empty);


                if (sqlDataReader.HasColumn(PronunciationDBFields.ShortName))
                    objPronunciation.ShortName = (sqlDataReader[PronunciationDBFields.ShortName] != DBNull.Value ? Convert.ToString(sqlDataReader[PronunciationDBFields.ShortName]) : string.Empty);

            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(sqlDataReader)", ex.Source, ex.Message, ex);
            }
            return objPronunciation;
        }

        public List<Pronunciation> GetDetailsList(SqlDataReader sqlDataReader)
        {
            List<Pronunciation> list = new List<Pronunciation>();
            try
            {
                while (sqlDataReader.Read())
                {
                    objPronunciation = GetDetails(sqlDataReader);
                    list.Add(objPronunciation);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetailsList(sqlDataReader)", ex.Source, ex.Message, ex);
            }
            return list;
        }

        public List<Pronunciation> GetDetails(DataSet dataSet)
        {
            List<Pronunciation> Pronunciations = new List<Pronunciation>();

            try
            {
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drow in dataSet.Tables[0].Rows)
                    {
                        objPronunciation = new Pronunciation();

                        if (drow.Table.Columns.Contains(PronunciationDBFields.ID))
                            objPronunciation.ID = (drow[PronunciationDBFields.ID] != DBNull.Value ? Convert.ToInt32(drow[PronunciationDBFields.ID]) : 0);
                        if (drow.Table.Columns.Contains(PronunciationDBFields.UserID))
                            objPronunciation.UserID = (drow[PronunciationDBFields.UserID] != DBNull.Value ? Convert.ToInt32(drow[PronunciationDBFields.UserID]) : 0);
                        if (drow.Table.Columns.Contains(PronunciationDBFields.InitialText))
                            objPronunciation.InitialText = (drow[PronunciationDBFields.InitialText] != DBNull.Value ? Convert.ToString(drow[PronunciationDBFields.InitialText]) : string.Empty);
                        if (drow.Table.Columns.Contains(PronunciationDBFields.AlternateText))
                            objPronunciation.AlternateText = (drow[PronunciationDBFields.AlternateText] != DBNull.Value ? Convert.ToString(drow[PronunciationDBFields.AlternateText]) : string.Empty);
                        if (drow.Table.Columns.Contains(PronunciationDBFields.Locale))
                            objPronunciation.Locale = (drow[PronunciationDBFields.Locale] != DBNull.Value ? Convert.ToString(drow[PronunciationDBFields.Locale]) : string.Empty);
                        if (drow.Table.Columns.Contains(PronunciationDBFields.LocaleCode))
                            objPronunciation.LocaleCode = (drow[PronunciationDBFields.LocaleCode] != DBNull.Value ? Convert.ToString(drow[PronunciationDBFields.LocaleCode]) : string.Empty);
                        if (drow.Table.Columns.Contains(PronunciationDBFields.StatusId))
                            objPronunciation.StatusId = (drow[PronunciationDBFields.StatusId] != DBNull.Value ? Convert.ToByte(drow[PronunciationDBFields.StatusId]) : (byte)0);
                        if (drow.Table.Columns.Contains(PronunciationDBFields.CreatedDate))
                            objPronunciation.CreatedDate = (drow[PronunciationDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(drow[PronunciationDBFields.CreatedDate]) : DateTime.Now);
                        if (drow.Table.Columns.Contains(PronunciationDBFields.UpdateDate))
                            objPronunciation.UpdateDate = (drow[PronunciationDBFields.UpdateDate] != DBNull.Value ? Convert.ToDateTime(drow[PronunciationDBFields.UpdateDate]) : DateTime.Now);

                        Pronunciations.Add(objPronunciation);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(dataSet)", ex.Source, ex.Message, ex);
            }

            return Pronunciations;
        }

        public Pronunciation GetDetailsobj(DataSet dataSet)
        {
            Pronunciation objPronunciation = new Pronunciation();

            try
            {
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drow in dataSet.Tables[0].Rows)
                    {
                        objPronunciation = new Pronunciation();

                        if (drow.Table.Columns.Contains(PronunciationDBFields.ID))
                            objPronunciation.ID = (drow[PronunciationDBFields.ID] != DBNull.Value ? Convert.ToInt32(drow[PronunciationDBFields.ID]) : 0);
                        if (drow.Table.Columns.Contains(PronunciationDBFields.UserID))
                            objPronunciation.UserID = (drow[PronunciationDBFields.UserID] != DBNull.Value ? Convert.ToInt32(drow[PronunciationDBFields.UserID]) : 0);
                        if (drow.Table.Columns.Contains(PronunciationDBFields.InitialText))
                            objPronunciation.InitialText = (drow[PronunciationDBFields.InitialText] != DBNull.Value ? Convert.ToString(drow[PronunciationDBFields.InitialText]) : string.Empty);
                        if (drow.Table.Columns.Contains(PronunciationDBFields.AlternateText))
                            objPronunciation.AlternateText = (drow[PronunciationDBFields.AlternateText] != DBNull.Value ? Convert.ToString(drow[PronunciationDBFields.AlternateText]) : string.Empty);
                        if (drow.Table.Columns.Contains(PronunciationDBFields.Locale))
                            objPronunciation.Locale = (drow[PronunciationDBFields.Locale] != DBNull.Value ? Convert.ToString(drow[PronunciationDBFields.Locale]) : string.Empty);
                        if (drow.Table.Columns.Contains(PronunciationDBFields.LocaleCode))
                            objPronunciation.LocaleCode = (drow[PronunciationDBFields.LocaleCode] != DBNull.Value ? Convert.ToString(drow[PronunciationDBFields.LocaleCode]) : string.Empty);
                        if (drow.Table.Columns.Contains(PronunciationDBFields.StatusId))
                            objPronunciation.StatusId = (drow[PronunciationDBFields.StatusId] != DBNull.Value ? Convert.ToByte(drow[PronunciationDBFields.StatusId]) : (byte)0);
                        if (drow.Table.Columns.Contains(PronunciationDBFields.CreatedDate))
                            objPronunciation.CreatedDate = (drow[PronunciationDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(drow[PronunciationDBFields.CreatedDate]) : DateTime.Now);
                        if (drow.Table.Columns.Contains(PronunciationDBFields.UpdateDate))
                            objPronunciation.UpdateDate = (drow[PronunciationDBFields.UpdateDate] != DBNull.Value ? Convert.ToDateTime(drow[PronunciationDBFields.UpdateDate]) : DateTime.Now);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(dataSet)", ex.Source, ex.Message, ex);
            }

            return objPronunciation;
        }

        public Pronunciation GetDetails(DataTable dataTable)
        {
            Pronunciation objPronunciation = new Pronunciation();

            try
            {
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow drow in dataTable.Rows)
                    {
                        objPronunciation = new Pronunciation();

                        if (drow.Table.Columns.Contains(PronunciationDBFields.ID))
                            objPronunciation.ID = (drow[PronunciationDBFields.ID] != DBNull.Value ? Convert.ToInt32(drow[PronunciationDBFields.ID]) : 0);
                        if (drow.Table.Columns.Contains(PronunciationDBFields.UserID))
                            objPronunciation.UserID = (drow[PronunciationDBFields.UserID] != DBNull.Value ? Convert.ToInt32(drow[PronunciationDBFields.UserID]) : 0);
                        if (drow.Table.Columns.Contains(PronunciationDBFields.InitialText))
                            objPronunciation.InitialText = (drow[PronunciationDBFields.InitialText] != DBNull.Value ? Convert.ToString(drow[PronunciationDBFields.InitialText]) : string.Empty);
                        if (drow.Table.Columns.Contains(PronunciationDBFields.AlternateText))
                            objPronunciation.AlternateText = (drow[PronunciationDBFields.AlternateText] != DBNull.Value ? Convert.ToString(drow[PronunciationDBFields.AlternateText]) : string.Empty);
                        if (drow.Table.Columns.Contains(PronunciationDBFields.Locale))
                            objPronunciation.Locale = (drow[PronunciationDBFields.Locale] != DBNull.Value ? Convert.ToString(drow[PronunciationDBFields.Locale]) : string.Empty);
                        if (drow.Table.Columns.Contains(PronunciationDBFields.LocaleCode))
                            objPronunciation.LocaleCode = (drow[PronunciationDBFields.LocaleCode] != DBNull.Value ? Convert.ToString(drow[PronunciationDBFields.LocaleCode]) : string.Empty);
                        if (drow.Table.Columns.Contains(PronunciationDBFields.StatusId))
                            objPronunciation.StatusId = (drow[PronunciationDBFields.StatusId] != DBNull.Value ? Convert.ToByte(drow[PronunciationDBFields.StatusId]) : (byte)0);
                        if (drow.Table.Columns.Contains(PronunciationDBFields.CreatedDate))
                            objPronunciation.CreatedDate = (drow[PronunciationDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(drow[PronunciationDBFields.CreatedDate]) : DateTime.Now);
                        if (drow.Table.Columns.Contains(PronunciationDBFields.UpdateDate))
                            objPronunciation.UpdateDate = (drow[PronunciationDBFields.UpdateDate] != DBNull.Value ? Convert.ToDateTime(drow[PronunciationDBFields.UpdateDate]) : DateTime.Now);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(DataTable)", ex.Source, ex.Message, ex);
            }

            return objPronunciation;
        }
    }
}