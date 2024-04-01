using Core.Business.DataAccess.Constants;
using Core.Entity;
using Core.Utility.Common;
using System.Data;
using System.Data.SqlClient;

namespace Core.Business.DataAccess.Mapper
{
    public class VoiceMasterDataMapper
    {
        private static readonly string _module = "Core.Business.DataAccess.Mapper.VoiceMasterDataMapper";
        private VoiceMaster objVoiceMaster = null;

        public VoiceMaster GetDetails(SqlDataReader sqlDataReader)
        {
            try
            {
                objVoiceMaster = new VoiceMaster();

                if (sqlDataReader.HasColumn(VoiceMasterDBFields.ID))
                    objVoiceMaster.ID = (sqlDataReader[VoiceMasterDBFields.ID] != DBNull.Value ? Convert.ToInt32(sqlDataReader[VoiceMasterDBFields.ID]) : 0);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.Name))
                    objVoiceMaster.Name = (sqlDataReader[VoiceMasterDBFields.Name] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.Name]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.DisplayName))
                    objVoiceMaster.DisplayName = (sqlDataReader[VoiceMasterDBFields.DisplayName] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.DisplayName]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.LocalName))
                    objVoiceMaster.LocalName = (sqlDataReader[VoiceMasterDBFields.LocalName] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.LocalName]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.ShortName))
                    objVoiceMaster.ShortName = (sqlDataReader[VoiceMasterDBFields.ShortName] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.ShortName]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.Gender))
                    objVoiceMaster.Gender = (sqlDataReader[VoiceMasterDBFields.Gender] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.Gender]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.Locale))
                    objVoiceMaster.Locale = (sqlDataReader[VoiceMasterDBFields.Locale] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.Locale]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.LocaleName))
                    objVoiceMaster.LocaleName = (sqlDataReader[VoiceMasterDBFields.LocaleName] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.LocaleName]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.SampleRateHertz))
                    objVoiceMaster.SampleRateHertz = (sqlDataReader[VoiceMasterDBFields.SampleRateHertz] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.SampleRateHertz]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.VoiceType))
                    objVoiceMaster.VoiceType = (sqlDataReader[VoiceMasterDBFields.VoiceType] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.VoiceType]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.Status))
                    objVoiceMaster.Status = (sqlDataReader[VoiceMasterDBFields.Status] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.Status]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.WordsPerMinute))
                    objVoiceMaster.WordsPerMinute = (sqlDataReader[VoiceMasterDBFields.WordsPerMinute] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.WordsPerMinute]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.StatusId))
                    objVoiceMaster.StatusId = (sqlDataReader[VoiceMasterDBFields.StatusId] != DBNull.Value ? Convert.ToByte(sqlDataReader[VoiceMasterDBFields.StatusId]) : (byte)0);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.CreatedDate))
                    objVoiceMaster.CreatedDate = (sqlDataReader[VoiceMasterDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(sqlDataReader[VoiceMasterDBFields.CreatedDate]) : DateTime.Now);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.UpdateDate))
                    objVoiceMaster.UpdateDate = (sqlDataReader[VoiceMasterDBFields.UpdateDate] != DBNull.Value ? Convert.ToDateTime(sqlDataReader[VoiceMasterDBFields.UpdateDate]) : DateTime.Now);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.Accent))
                    objVoiceMaster.Accent = (sqlDataReader[VoiceMasterDBFields.Accent] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.Accent]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.AccentCode))
                    objVoiceMaster.AccentCode = (sqlDataReader[VoiceMasterDBFields.AccentCode] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.AccentCode]) : string.Empty);

                if (sqlDataReader.HasColumn(VoiceMasterDBFields.AgeBracket))
                    objVoiceMaster.AgeBracket = (sqlDataReader[VoiceMasterDBFields.AgeBracket] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.AgeBracket]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.Provider))
                    objVoiceMaster.Provider = (sqlDataReader[VoiceMasterDBFields.Provider] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.Provider]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.VoiceImage))
                    objVoiceMaster.VoiceImage = (sqlDataReader[VoiceMasterDBFields.VoiceImage] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.VoiceImage]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.Alias))
                    objVoiceMaster.Alias = (sqlDataReader[VoiceMasterDBFields.Alias] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.Alias]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.Locale))
                    objVoiceMaster.Locale = (sqlDataReader[VoiceMasterDBFields.Locale] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.Locale]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.LocaleCode))
                    objVoiceMaster.LocaleCode = (sqlDataReader[VoiceMasterDBFields.LocaleCode] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.LocaleCode]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.RowNumber))
                    objVoiceMaster.RowNumber = (sqlDataReader[VoiceMasterDBFields.RowNumber] != DBNull.Value ? Convert.ToInt32(sqlDataReader[VoiceMasterDBFields.RowNumber]) : 0);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.CreatedDate))
                    objVoiceMaster.CreatedDate = (sqlDataReader[VoiceMasterDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(sqlDataReader[VoiceMasterDBFields.CreatedDate]) : DateTime.Now);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.IsAdded))
                    objVoiceMaster.IsAdded = (sqlDataReader[VoiceMasterDBFields.IsAdded] != DBNull.Value ? Convert.ToInt32(sqlDataReader[VoiceMasterDBFields.IsAdded]) : 0);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.VoiceMasterUserMapID))
                    objVoiceMaster.VoiceMasterUserMapID = (sqlDataReader[VoiceMasterDBFields.VoiceMasterUserMapID] != DBNull.Value ? Convert.ToInt32(sqlDataReader[VoiceMasterDBFields.VoiceMasterUserMapID]) : 0);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.VoiceMasterID))
                    objVoiceMaster.VoiceMasterID = (sqlDataReader[VoiceMasterDBFields.VoiceMasterID] != DBNull.Value ? Convert.ToInt32(sqlDataReader[VoiceMasterDBFields.VoiceMasterID]) : 0);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.Description))
                    objVoiceMaster.Description = (sqlDataReader[VoiceMasterDBFields.Description] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.Description]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.VMUUID))
                    objVoiceMaster.VMUUID = (sqlDataReader[VoiceMasterDBFields.VMUUID] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.VMUUID]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.CharLimit))
                    objVoiceMaster.CharLimit = (sqlDataReader[VoiceMasterDBFields.CharLimit] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.CharLimit]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.SampleURL))
                    objVoiceMaster.SampleURL = (sqlDataReader[VoiceMasterDBFields.CharLimit] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.SampleURL]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.VoiceMasterID))
                    objVoiceMaster.VoiceMasterID = (sqlDataReader[VoiceMasterDBFields.VoiceMasterID] != DBNull.Value ? Convert.ToInt32(sqlDataReader[VoiceMasterDBFields.VoiceMasterID]) : 0);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.TagName))
                    objVoiceMaster.TagName = (sqlDataReader[VoiceMasterDBFields.TagName] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.TagName]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.BestForName))
                    objVoiceMaster.BestForName = (sqlDataReader[VoiceMasterDBFields.BestForName] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.BestForName]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.StyleName))
                    objVoiceMaster.StyleName = (sqlDataReader[VoiceMasterDBFields.StyleName] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.StyleName]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.StyleValue))
                    objVoiceMaster.StyleValue = (sqlDataReader[VoiceMasterDBFields.StyleValue] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.StyleValue]) : string.Empty);

                if (sqlDataReader.HasColumn(VoiceMasterDBFields.ampVoiceProvider))
                    objVoiceMaster.ampVoiceProvider = (sqlDataReader[VoiceMasterDBFields.ampVoiceProvider] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.ampVoiceProvider]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.ampvoiceUUID))
                    objVoiceMaster.ampvoiceUUID = (sqlDataReader[VoiceMasterDBFields.ampvoiceUUID] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.ampvoiceUUID]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.ampDisplayName))
                    objVoiceMaster.ampDisplayName = (sqlDataReader[VoiceMasterDBFields.ampDisplayName] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.ampDisplayName]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.ampvoiceGender))
                    objVoiceMaster.ampvoiceGender = (sqlDataReader[VoiceMasterDBFields.ampvoiceGender] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.ampvoiceGender]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.ampvoicePhoto))
                    objVoiceMaster.ampvoicePhoto = (sqlDataReader[VoiceMasterDBFields.ampvoicePhoto] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.ampvoicePhoto]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.ampvoicedescription))
                    objVoiceMaster.ampvoicedescription = (sqlDataReader[VoiceMasterDBFields.ampvoicedescription] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.ampvoicedescription]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.ampvoiceaccent))
                    objVoiceMaster.ampvoiceaccent = (sqlDataReader[VoiceMasterDBFields.ampvoiceaccent] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.ampvoiceaccent]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.ampvoicelocale))
                    objVoiceMaster.ampvoicelocale = (sqlDataReader[VoiceMasterDBFields.ampvoicelocale] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.ampvoicelocale]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.ampvoicelocaleID))
                    objVoiceMaster.ampvoicelocaleID = (sqlDataReader[VoiceMasterDBFields.ampvoicelocaleID] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.ampvoicelocaleID]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.ampvoiceaccentID))
                    objVoiceMaster.ampvoiceaccentID = (sqlDataReader[VoiceMasterDBFields.ampvoiceaccentID] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.ampvoiceaccentID]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.ampshortName))
                    objVoiceMaster.ampshortName = (sqlDataReader[VoiceMasterDBFields.ampshortName] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.ampshortName]) : string.Empty);

                if (sqlDataReader.HasColumn(VoiceMasterDBFields.ampvoiceagerange))
                    objVoiceMaster.ampvoiceagerange = (sqlDataReader[VoiceMasterDBFields.ampvoiceagerange] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.ampvoiceagerange]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.ampvoiceType))
                    objVoiceMaster.ampvoiceType = (sqlDataReader[VoiceMasterDBFields.ampvoiceType] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.ampvoiceType]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.ampSampleRateHertz))
                    objVoiceMaster.ampSampleRateHertz = (sqlDataReader[VoiceMasterDBFields.ampSampleRateHertz] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.ampSampleRateHertz]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.ampStatus))
                    objVoiceMaster.ampStatus = (sqlDataReader[VoiceMasterDBFields.ampStatus] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.ampStatus]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.ampWordsPerMinute))
                    objVoiceMaster.ampWordsPerMinute = (sqlDataReader[VoiceMasterDBFields.ampWordsPerMinute] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.ampWordsPerMinute]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.ampCharacterLimit))
                    objVoiceMaster.ampCharacterLimit = (sqlDataReader[VoiceMasterDBFields.ampCharacterLimit] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.ampCharacterLimit]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterDBFields.ampvoicesampleMp3))
                    objVoiceMaster.ampvoicesampleMp3 = (sqlDataReader[VoiceMasterDBFields.ampvoicesampleMp3] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterDBFields.ampvoicesampleMp3]) : string.Empty);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(sqlDataReader)", ex.Source, ex.Message, ex);
            }
            return objVoiceMaster;
        }

        public List<VoiceMaster> GetDetailsList(SqlDataReader sqlDataReader)
        {
            List<VoiceMaster> list = new List<VoiceMaster>();
            try
            {
                while (sqlDataReader.Read())
                {
                    objVoiceMaster = GetDetails(sqlDataReader);
                    list.Add(objVoiceMaster);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetailsList(sqlDataReader)", ex.Source, ex.Message, ex);
            }
            return list;
        }

        public VoiceMaster GetDetail(SqlDataReader sqlDataReader)
        {
            //  VoiceMaster voiceMaster = new VoiceMaster();
            try
            {
                if (sqlDataReader.Read())
                {
                    objVoiceMaster = GetDetails(sqlDataReader);
                    // voiceMaster.Add(objVoiceMaster);
                }
                return objVoiceMaster;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetailsList(sqlDataReader)", ex.Source, ex.Message, ex);
            }
            return new VoiceMaster();
        }

        public List<VoiceMaster> GetDetails(DataSet dataSet)
        {
            List<VoiceMaster> VoiceMasters = new List<VoiceMaster>();

            try
            {
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drow in dataSet.Tables[0].Rows)
                    {
                        objVoiceMaster = new VoiceMaster();

                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.ID))
                            objVoiceMaster.ID = (drow[VoiceMasterDBFields.ID] != DBNull.Value ? Convert.ToInt32(drow[VoiceMasterDBFields.ID]) : 0);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.Name))
                            objVoiceMaster.Name = (drow[VoiceMasterDBFields.Name] != DBNull.Value ? Convert.ToString(drow[VoiceMasterDBFields.Name]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.DisplayName))
                            objVoiceMaster.DisplayName = (drow[VoiceMasterDBFields.DisplayName] != DBNull.Value ? Convert.ToString(drow[VoiceMasterDBFields.DisplayName]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.LocalName))
                            objVoiceMaster.LocalName = (drow[VoiceMasterDBFields.LocalName] != DBNull.Value ? Convert.ToString(drow[VoiceMasterDBFields.LocalName]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.ShortName))
                            objVoiceMaster.ShortName = (drow[VoiceMasterDBFields.ShortName] != DBNull.Value ? Convert.ToString(drow[VoiceMasterDBFields.ShortName]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.Gender))
                            objVoiceMaster.Gender = (drow[VoiceMasterDBFields.Gender] != DBNull.Value ? Convert.ToString(drow[VoiceMasterDBFields.Gender]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.Locale))
                            objVoiceMaster.Locale = (drow[VoiceMasterDBFields.Locale] != DBNull.Value ? Convert.ToString(drow[VoiceMasterDBFields.Locale]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.LocaleName))
                            objVoiceMaster.LocaleName = (drow[VoiceMasterDBFields.LocaleName] != DBNull.Value ? Convert.ToString(drow[VoiceMasterDBFields.LocaleName]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.SampleRateHertz))
                            objVoiceMaster.SampleRateHertz = (drow[VoiceMasterDBFields.SampleRateHertz] != DBNull.Value ? Convert.ToString(drow[VoiceMasterDBFields.SampleRateHertz]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.VoiceType))
                            objVoiceMaster.VoiceType = (drow[VoiceMasterDBFields.VoiceType] != DBNull.Value ? Convert.ToString(drow[VoiceMasterDBFields.VoiceType]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.Status))
                            objVoiceMaster.Status = (drow[VoiceMasterDBFields.Status] != DBNull.Value ? Convert.ToString(drow[VoiceMasterDBFields.Status]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.WordsPerMinute))
                            objVoiceMaster.WordsPerMinute = (drow[VoiceMasterDBFields.WordsPerMinute] != DBNull.Value ? Convert.ToString(drow[VoiceMasterDBFields.WordsPerMinute]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.StatusId))
                            objVoiceMaster.StatusId = (drow[VoiceMasterDBFields.StatusId] != DBNull.Value ? Convert.ToByte(drow[VoiceMasterDBFields.StatusId]) : (byte)0);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.CreatedDate))
                            objVoiceMaster.CreatedDate = (drow[VoiceMasterDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(drow[VoiceMasterDBFields.CreatedDate]) : DateTime.Now);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.UpdateDate))
                            objVoiceMaster.UpdateDate = (drow[VoiceMasterDBFields.UpdateDate] != DBNull.Value ? Convert.ToDateTime(drow[VoiceMasterDBFields.UpdateDate]) : DateTime.Now);

                        VoiceMasters.Add(objVoiceMaster);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(dataSet)", ex.Source, ex.Message, ex);
            }

            return VoiceMasters;
        }

        public VoiceMaster GetDetailsobj(DataSet dataSet)
        {
            VoiceMaster objVoiceMaster = new VoiceMaster();

            try
            {
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drow in dataSet.Tables[0].Rows)
                    {
                        objVoiceMaster = new VoiceMaster();

                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.ID))
                            objVoiceMaster.ID = (drow[VoiceMasterDBFields.ID] != DBNull.Value ? Convert.ToInt32(drow[VoiceMasterDBFields.ID]) : 0);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.Name))
                            objVoiceMaster.Name = (drow[VoiceMasterDBFields.Name] != DBNull.Value ? Convert.ToString(drow[VoiceMasterDBFields.Name]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.DisplayName))
                            objVoiceMaster.DisplayName = (drow[VoiceMasterDBFields.DisplayName] != DBNull.Value ? Convert.ToString(drow[VoiceMasterDBFields.DisplayName]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.LocalName))
                            objVoiceMaster.LocalName = (drow[VoiceMasterDBFields.LocalName] != DBNull.Value ? Convert.ToString(drow[VoiceMasterDBFields.LocalName]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.ShortName))
                            objVoiceMaster.ShortName = (drow[VoiceMasterDBFields.ShortName] != DBNull.Value ? Convert.ToString(drow[VoiceMasterDBFields.ShortName]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.Gender))
                            objVoiceMaster.Gender = (drow[VoiceMasterDBFields.Gender] != DBNull.Value ? Convert.ToString(drow[VoiceMasterDBFields.Gender]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.Locale))
                            objVoiceMaster.Locale = (drow[VoiceMasterDBFields.Locale] != DBNull.Value ? Convert.ToString(drow[VoiceMasterDBFields.Locale]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.LocaleName))
                            objVoiceMaster.LocaleName = (drow[VoiceMasterDBFields.LocaleName] != DBNull.Value ? Convert.ToString(drow[VoiceMasterDBFields.LocaleName]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.SampleRateHertz))
                            objVoiceMaster.SampleRateHertz = (drow[VoiceMasterDBFields.SampleRateHertz] != DBNull.Value ? Convert.ToString(drow[VoiceMasterDBFields.SampleRateHertz]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.VoiceType))
                            objVoiceMaster.VoiceType = (drow[VoiceMasterDBFields.VoiceType] != DBNull.Value ? Convert.ToString(drow[VoiceMasterDBFields.VoiceType]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.Status))
                            objVoiceMaster.Status = (drow[VoiceMasterDBFields.Status] != DBNull.Value ? Convert.ToString(drow[VoiceMasterDBFields.Status]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.WordsPerMinute))
                            objVoiceMaster.WordsPerMinute = (drow[VoiceMasterDBFields.WordsPerMinute] != DBNull.Value ? Convert.ToString(drow[VoiceMasterDBFields.WordsPerMinute]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.StatusId))
                            objVoiceMaster.StatusId = (drow[VoiceMasterDBFields.StatusId] != DBNull.Value ? Convert.ToByte(drow[VoiceMasterDBFields.StatusId]) : (byte)0);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.CreatedDate))
                            objVoiceMaster.CreatedDate = (drow[VoiceMasterDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(drow[VoiceMasterDBFields.CreatedDate]) : DateTime.Now);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.UpdateDate))
                            objVoiceMaster.UpdateDate = (drow[VoiceMasterDBFields.UpdateDate] != DBNull.Value ? Convert.ToDateTime(drow[VoiceMasterDBFields.UpdateDate]) : DateTime.Now);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(dataSet)", ex.Source, ex.Message, ex);
            }

            return objVoiceMaster;
        }

        public VoiceMaster GetDetails(DataTable dataTable)
        {
            VoiceMaster objVoiceMaster = new VoiceMaster();

            try
            {
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow drow in dataTable.Rows)
                    {
                        objVoiceMaster = new VoiceMaster();

                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.ID))
                            objVoiceMaster.ID = (drow[VoiceMasterDBFields.ID] != DBNull.Value ? Convert.ToInt32(drow[VoiceMasterDBFields.ID]) : 0);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.Name))
                            objVoiceMaster.Name = (drow[VoiceMasterDBFields.Name] != DBNull.Value ? Convert.ToString(drow[VoiceMasterDBFields.Name]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.DisplayName))
                            objVoiceMaster.DisplayName = (drow[VoiceMasterDBFields.DisplayName] != DBNull.Value ? Convert.ToString(drow[VoiceMasterDBFields.DisplayName]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.LocalName))
                            objVoiceMaster.LocalName = (drow[VoiceMasterDBFields.LocalName] != DBNull.Value ? Convert.ToString(drow[VoiceMasterDBFields.LocalName]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.ShortName))
                            objVoiceMaster.ShortName = (drow[VoiceMasterDBFields.ShortName] != DBNull.Value ? Convert.ToString(drow[VoiceMasterDBFields.ShortName]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.Gender))
                            objVoiceMaster.Gender = (drow[VoiceMasterDBFields.Gender] != DBNull.Value ? Convert.ToString(drow[VoiceMasterDBFields.Gender]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.Locale))
                            objVoiceMaster.Locale = (drow[VoiceMasterDBFields.Locale] != DBNull.Value ? Convert.ToString(drow[VoiceMasterDBFields.Locale]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.LocaleName))
                            objVoiceMaster.LocaleName = (drow[VoiceMasterDBFields.LocaleName] != DBNull.Value ? Convert.ToString(drow[VoiceMasterDBFields.LocaleName]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.SampleRateHertz))
                            objVoiceMaster.SampleRateHertz = (drow[VoiceMasterDBFields.SampleRateHertz] != DBNull.Value ? Convert.ToString(drow[VoiceMasterDBFields.SampleRateHertz]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.VoiceType))
                            objVoiceMaster.VoiceType = (drow[VoiceMasterDBFields.VoiceType] != DBNull.Value ? Convert.ToString(drow[VoiceMasterDBFields.VoiceType]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.Status))
                            objVoiceMaster.Status = (drow[VoiceMasterDBFields.Status] != DBNull.Value ? Convert.ToString(drow[VoiceMasterDBFields.Status]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.WordsPerMinute))
                            objVoiceMaster.WordsPerMinute = (drow[VoiceMasterDBFields.WordsPerMinute] != DBNull.Value ? Convert.ToString(drow[VoiceMasterDBFields.WordsPerMinute]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.StatusId))
                            objVoiceMaster.StatusId = (drow[VoiceMasterDBFields.StatusId] != DBNull.Value ? Convert.ToByte(drow[VoiceMasterDBFields.StatusId]) : (byte)0);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.CreatedDate))
                            objVoiceMaster.CreatedDate = (drow[VoiceMasterDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(drow[VoiceMasterDBFields.CreatedDate]) : DateTime.Now);
                        if (drow.Table.Columns.Contains(VoiceMasterDBFields.UpdateDate))
                            objVoiceMaster.UpdateDate = (drow[VoiceMasterDBFields.UpdateDate] != DBNull.Value ? Convert.ToDateTime(drow[VoiceMasterDBFields.UpdateDate]) : DateTime.Now);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(DataTable)", ex.Source, ex.Message, ex);
            }

            return objVoiceMaster;
        }
    }
}