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
    public class VoiceMasterSettingDataMapper
    {
        private static readonly string _module = "Core.Business.DataAccess.Mapper.VoiceMasterSettingDataMapper";
        private VoiceMasterSetting objVoiceMasterSetting = null;

        public VoiceMasterSetting GetDetails(SqlDataReader sqlDataReader)
        {
            try
            {
                objVoiceMasterSetting = new VoiceMasterSetting();

                if (sqlDataReader.HasColumn(VoiceMasterSettingDBFields.ID))
                    objVoiceMasterSetting.ID = (sqlDataReader[VoiceMasterSettingDBFields.ID] != DBNull.Value ? Convert.ToInt32(sqlDataReader[VoiceMasterSettingDBFields.ID]) : 0);
                if (sqlDataReader.HasColumn(VoiceMasterSettingDBFields.UUID))
                    objVoiceMasterSetting.UUID = (sqlDataReader[VoiceMasterSettingDBFields.UUID] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterSettingDBFields.UUID]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterSettingDBFields.Description))
                    objVoiceMasterSetting.Description = (sqlDataReader[VoiceMasterSettingDBFields.Description] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterSettingDBFields.Description]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterSettingDBFields.VoiceImage))
                    objVoiceMasterSetting.VoiceImage = (sqlDataReader[VoiceMasterSettingDBFields.VoiceImage] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterSettingDBFields.VoiceImage]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterSettingDBFields.StatusId))
                    objVoiceMasterSetting.StatusId = (sqlDataReader[VoiceMasterSettingDBFields.StatusId] != DBNull.Value ? Convert.ToByte(sqlDataReader[VoiceMasterSettingDBFields.StatusId]) : (byte)0);
                if (sqlDataReader.HasColumn(VoiceMasterSettingDBFields.CreatedDate))
                    objVoiceMasterSetting.CreatedDate = (sqlDataReader[VoiceMasterSettingDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(sqlDataReader[VoiceMasterSettingDBFields.CreatedDate]) : DateTime.Now);
                if (sqlDataReader.HasColumn(VoiceMasterSettingDBFields.UpdatedDate))
                    objVoiceMasterSetting.UpdatedDate = (sqlDataReader[VoiceMasterSettingDBFields.UpdatedDate] != DBNull.Value ? Convert.ToDateTime(sqlDataReader[VoiceMasterSettingDBFields.UpdatedDate]) : DateTime.Now);
                if (sqlDataReader.HasColumn(VoiceMasterSettingDBFields.VoiceMasterID))
                    objVoiceMasterSetting.VoiceMasterID = (sqlDataReader[VoiceMasterSettingDBFields.VoiceMasterID] != DBNull.Value ? Convert.ToInt32(sqlDataReader[VoiceMasterSettingDBFields.VoiceMasterID]) : 0);
                if (sqlDataReader.HasColumn(VoiceMasterSettingDBFields.AgeBracket))
                    objVoiceMasterSetting.AgeBracket = (sqlDataReader[VoiceMasterSettingDBFields.AgeBracket] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterSettingDBFields.AgeBracket]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterSettingDBFields.VoiceProvider))
                    objVoiceMasterSetting.VoiceProvider = (sqlDataReader[VoiceMasterSettingDBFields.VoiceProvider] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterSettingDBFields.VoiceProvider]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterSettingDBFields.CharLimit))
                    objVoiceMasterSetting.CharLimit = (sqlDataReader[VoiceMasterSettingDBFields.CharLimit] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterSettingDBFields.CharLimit]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterSettingDBFields.SampleUrl))
                    objVoiceMasterSetting.SampleUrl = (sqlDataReader[VoiceMasterSettingDBFields.SampleUrl] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterSettingDBFields.SampleUrl]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterSettingDBFields.VMDisplayName))
                    objVoiceMasterSetting.VMDisplayName = (sqlDataReader[VoiceMasterSettingDBFields.VMDisplayName] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterSettingDBFields.VMDisplayName]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterSettingDBFields.VMLocalName))
                    objVoiceMasterSetting.VMLocalName = (sqlDataReader[VoiceMasterSettingDBFields.VMLocalName] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterSettingDBFields.VMLocalName]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterSettingDBFields.VMShortName))
                    objVoiceMasterSetting.VMShortName = (sqlDataReader[VoiceMasterSettingDBFields.VMShortName] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterSettingDBFields.VMShortName]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterSettingDBFields.VMLocale))
                    objVoiceMasterSetting.VMLocale = (sqlDataReader[VoiceMasterSettingDBFields.VMLocale] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterSettingDBFields.VMLocale]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterSettingDBFields.VMLocaleName))
                    objVoiceMasterSetting.VMLocaleName = (sqlDataReader[VoiceMasterSettingDBFields.VMLocaleName] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterSettingDBFields.VMLocaleName]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterSettingDBFields.VMSampleRateHertz))
                    objVoiceMasterSetting.VMSampleRateHertz = (sqlDataReader[VoiceMasterSettingDBFields.VMSampleRateHertz] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterSettingDBFields.VMSampleRateHertz]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterSettingDBFields.VMVoiceType))
                    objVoiceMasterSetting.VMVoiceType = (sqlDataReader[VoiceMasterSettingDBFields.VMVoiceType] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterSettingDBFields.VMVoiceType]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterSettingDBFields.VMStatus))
                    objVoiceMasterSetting.VMStatus = (sqlDataReader[VoiceMasterSettingDBFields.VMStatus] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterSettingDBFields.VMStatus]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterSettingDBFields.VMWordsPerMinute))
                    objVoiceMasterSetting.VMWordsPerMinute = (sqlDataReader[VoiceMasterSettingDBFields.VMWordsPerMinute] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterSettingDBFields.VMWordsPerMinute]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterSettingDBFields.VMName))
                    objVoiceMasterSetting.VMName = (sqlDataReader[VoiceMasterSettingDBFields.VMName] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterSettingDBFields.VMName]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterSettingDBFields.VMGender))
                    objVoiceMasterSetting.VMGender = (sqlDataReader[VoiceMasterSettingDBFields.VMGender] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterSettingDBFields.VMGender]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterSettingDBFields.VMStyleList))
                    objVoiceMasterSetting.VMStyleList = (sqlDataReader[VoiceMasterSettingDBFields.VMStyleList] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterSettingDBFields.VMStyleList]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterSettingDBFields.VMUUID))
                    objVoiceMasterSetting.VMUUID = (sqlDataReader[VoiceMasterSettingDBFields.VMUUID] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterSettingDBFields.VMUUID]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterSettingDBFields.VMLanguage))
                    objVoiceMasterSetting.VMLanguage = (sqlDataReader[VoiceMasterSettingDBFields.VMLanguage] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterSettingDBFields.VMLanguage]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterSettingDBFields.RowNumber))
                    objVoiceMasterSetting.RowNumber = (sqlDataReader[VoiceMasterSettingDBFields.RowNumber] != DBNull.Value ? Convert.ToInt64(sqlDataReader[UsersDBFields.RowNumber]) : 0);
                if (sqlDataReader.HasColumn(VoiceMasterSettingDBFields.VMAccent))
                    objVoiceMasterSetting.VMAccent = (sqlDataReader[VoiceMasterSettingDBFields.VMAccent] != DBNull.Value ? Convert.ToString(sqlDataReader[VoiceMasterSettingDBFields.VMAccent]) : string.Empty);
                if (sqlDataReader.HasColumn(VoiceMasterSettingDBFields.UserID))
                    objVoiceMasterSetting.UserID = (sqlDataReader[VoiceMasterSettingDBFields.UserID] != DBNull.Value ? Convert.ToInt32(sqlDataReader[VoiceMasterSettingDBFields.UserID]) : 0);
                if (sqlDataReader.HasColumn(VoiceMasterSettingDBFields.VoiceMasterUserMapID))
                    objVoiceMasterSetting.VoiceMasterUserMapID = (sqlDataReader[VoiceMasterSettingDBFields.VoiceMasterUserMapID] != DBNull.Value ? Convert.ToInt32(sqlDataReader[VoiceMasterSettingDBFields.VoiceMasterUserMapID]) : 0);
                if (sqlDataReader.HasColumn(VoiceMasterSettingDBFields.IsAdded))
                    objVoiceMasterSetting.IsAdded = (sqlDataReader[VoiceMasterSettingDBFields.IsAdded] != DBNull.Value ? Convert.ToInt32(sqlDataReader[VoiceMasterSettingDBFields.IsAdded]) : 0);

                // Assuming VoiceMasterSettingDBFields.TagNames is the column name containing comma-separated values
                if (sqlDataReader.HasColumn(VoiceMasterSettingDBFields.TagNames))
                {
                    string tagNamesCSV = sqlDataReader[VoiceMasterSettingDBFields.TagNames] != DBNull.Value ?
                        Convert.ToString(sqlDataReader[VoiceMasterSettingDBFields.TagNames]) : string.Empty;

                    // Split the comma-separated values into an array
                    string[] tagNamesArray = tagNamesCSV.Split(',');

                    // Join the array elements into a single string
                    string combinedTagNames = string.Join(", ", tagNamesArray);

                    // Assign the combined string to the TagName property
                    objVoiceMasterSetting.TagName = combinedTagNames;
                }

                if (sqlDataReader.HasColumn(VoiceMasterSettingDBFields.BestForNames))
                {
                    string bestForNamesCSV = sqlDataReader[VoiceMasterSettingDBFields.BestForNames] != DBNull.Value ?
                        Convert.ToString(sqlDataReader[VoiceMasterSettingDBFields.BestForNames]) : string.Empty;

                    // Split the comma-separated values into an array
                    string[] bestForNamesArray = bestForNamesCSV.Split(',');

                    // Join the array elements into a single string
                    string combinedbestForNames = string.Join(", ", bestForNamesArray);

                    // Assign the combined string to the TagName property
                    objVoiceMasterSetting.BestForName = combinedbestForNames;
                }
                if (sqlDataReader.HasColumn(VoiceMasterSettingDBFields.StyleListName))
                {
                    string styleListNameCSV = sqlDataReader[VoiceMasterSettingDBFields.StyleListName] != DBNull.Value ?
                        Convert.ToString(sqlDataReader[VoiceMasterSettingDBFields.StyleListName]) : string.Empty;

                    // Split the comma-separated values into an array
                    string[] styleListNameArray = styleListNameCSV.Split(',');

                    // Join the array elements into a single string
                    string combinedstyleListName = string.Join(", ", styleListNameArray);

                    // Assign the combined string to the TagName property
                    objVoiceMasterSetting.StyleName = combinedstyleListName;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(sqlDataReader)", ex.Source, ex.Message, ex);
            }
            return objVoiceMasterSetting;
        }

        public List<VoiceMasterSetting> GetDetailsList(SqlDataReader sqlDataReader)
        {
            List<VoiceMasterSetting> list = new List<VoiceMasterSetting>();
            try
            {
                while (sqlDataReader.Read())
                {
                    objVoiceMasterSetting = GetDetails(sqlDataReader);
                    list.Add(objVoiceMasterSetting);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetailsList(sqlDataReader)", ex.Source, ex.Message, ex);
            }
            return list;
        }

        public List<VoiceMasterSetting> GetDetails(DataSet dataSet)
        {
            List<VoiceMasterSetting> VoiceMasterSettings = new List<VoiceMasterSetting>();

            try
            {
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drow in dataSet.Tables[0].Rows)
                    {
                        objVoiceMasterSetting = new VoiceMasterSetting();

                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.ID))
                            objVoiceMasterSetting.ID = (drow[VoiceMasterSettingDBFields.ID] != DBNull.Value ? Convert.ToInt32(drow[VoiceMasterSettingDBFields.ID]) : 0);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.UUID))
                            objVoiceMasterSetting.UUID = (drow[VoiceMasterSettingDBFields.UUID] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.UUID]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.Description))
                            objVoiceMasterSetting.Description = (drow[VoiceMasterSettingDBFields.Description] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.Description]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VoiceImage))
                            objVoiceMasterSetting.VoiceImage = (drow[VoiceMasterSettingDBFields.VoiceImage] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VoiceImage]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.StatusId))
                            objVoiceMasterSetting.StatusId = (drow[VoiceMasterSettingDBFields.StatusId] != DBNull.Value ? Convert.ToByte(drow[VoiceMasterSettingDBFields.StatusId]) : (byte)0);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.CreatedDate))
                            objVoiceMasterSetting.CreatedDate = (drow[VoiceMasterSettingDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(drow[VoiceMasterSettingDBFields.CreatedDate]) : DateTime.Now);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.UpdatedDate))
                            objVoiceMasterSetting.UpdatedDate = (drow[VoiceMasterSettingDBFields.UpdatedDate] != DBNull.Value ? Convert.ToDateTime(drow[VoiceMasterSettingDBFields.UpdatedDate]) : DateTime.Now);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VoiceMasterID))
                            objVoiceMasterSetting.VoiceMasterID = (drow[VoiceMasterSettingDBFields.VoiceMasterID] != DBNull.Value ? Convert.ToInt32(drow[VoiceMasterSettingDBFields.VoiceMasterID]) : 0);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.AgeBracket))
                            objVoiceMasterSetting.AgeBracket = (drow[VoiceMasterSettingDBFields.AgeBracket] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.AgeBracket]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VoiceProvider))
                            objVoiceMasterSetting.VoiceProvider = (drow[VoiceMasterSettingDBFields.VoiceProvider] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VoiceProvider]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.CharLimit))
                            objVoiceMasterSetting.CharLimit = (drow[VoiceMasterSettingDBFields.CharLimit] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.CharLimit]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.SampleUrl))
                            objVoiceMasterSetting.SampleUrl = (drow[VoiceMasterSettingDBFields.SampleUrl] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.SampleUrl]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMDisplayName))
                            objVoiceMasterSetting.VMDisplayName = (drow[VoiceMasterSettingDBFields.VMDisplayName] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMDisplayName]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMLocalName))
                            objVoiceMasterSetting.VMLocalName = (drow[VoiceMasterSettingDBFields.VMLocalName] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMLocalName]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMShortName))
                            objVoiceMasterSetting.VMShortName = (drow[VoiceMasterSettingDBFields.VMShortName] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMShortName]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMLocale))
                            objVoiceMasterSetting.VMLocale = (drow[VoiceMasterSettingDBFields.VMLocale] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMLocale]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMLocaleName))
                            objVoiceMasterSetting.VMLocaleName = (drow[VoiceMasterSettingDBFields.VMLocaleName] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMLocaleName]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMSampleRateHertz))
                            objVoiceMasterSetting.VMSampleRateHertz = (drow[VoiceMasterSettingDBFields.VMSampleRateHertz] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMSampleRateHertz]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMVoiceType))
                            objVoiceMasterSetting.VMVoiceType = (drow[VoiceMasterSettingDBFields.VMVoiceType] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMVoiceType]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMStatus))
                            objVoiceMasterSetting.VMStatus = (drow[VoiceMasterSettingDBFields.VMStatus] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMStatus]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMWordsPerMinute))
                            objVoiceMasterSetting.VMWordsPerMinute = (drow[VoiceMasterSettingDBFields.VMWordsPerMinute] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMWordsPerMinute]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMName))
                            objVoiceMasterSetting.VMName = (drow[VoiceMasterSettingDBFields.VMName] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMName]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMGender))
                            objVoiceMasterSetting.VMGender = (drow[VoiceMasterSettingDBFields.VMGender] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMGender]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMStyleList))
                            objVoiceMasterSetting.VMStyleList = (drow[VoiceMasterSettingDBFields.VMStyleList] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMStyleList]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMUUID))
                            objVoiceMasterSetting.VMUUID = (drow[VoiceMasterSettingDBFields.VMUUID] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMUUID]) : string.Empty);

                        VoiceMasterSettings.Add(objVoiceMasterSetting);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(dataSet)", ex.Source, ex.Message, ex);
            }

            return VoiceMasterSettings;
        }

        public VoiceMasterSetting GetDetailsobj(DataSet dataSet)
        {
            VoiceMasterSetting objVoiceMasterSetting = new VoiceMasterSetting();

            try
            {
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drow in dataSet.Tables[0].Rows)
                    {
                        objVoiceMasterSetting = new VoiceMasterSetting();

                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.ID))
                            objVoiceMasterSetting.ID = (drow[VoiceMasterSettingDBFields.ID] != DBNull.Value ? Convert.ToInt32(drow[VoiceMasterSettingDBFields.ID]) : 0);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.UUID))
                            objVoiceMasterSetting.UUID = (drow[VoiceMasterSettingDBFields.UUID] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.UUID]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.Description))
                            objVoiceMasterSetting.Description = (drow[VoiceMasterSettingDBFields.Description] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.Description]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VoiceImage))
                            objVoiceMasterSetting.VoiceImage = (drow[VoiceMasterSettingDBFields.VoiceImage] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VoiceImage]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.StatusId))
                            objVoiceMasterSetting.StatusId = (drow[VoiceMasterSettingDBFields.StatusId] != DBNull.Value ? Convert.ToByte(drow[VoiceMasterSettingDBFields.StatusId]) : (byte)0);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.CreatedDate))
                            objVoiceMasterSetting.CreatedDate = (drow[VoiceMasterSettingDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(drow[VoiceMasterSettingDBFields.CreatedDate]) : DateTime.Now);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.UpdatedDate))
                            objVoiceMasterSetting.UpdatedDate = (drow[VoiceMasterSettingDBFields.UpdatedDate] != DBNull.Value ? Convert.ToDateTime(drow[VoiceMasterSettingDBFields.UpdatedDate]) : DateTime.Now);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VoiceMasterID))
                            objVoiceMasterSetting.VoiceMasterID = (drow[VoiceMasterSettingDBFields.VoiceMasterID] != DBNull.Value ? Convert.ToInt32(drow[VoiceMasterSettingDBFields.VoiceMasterID]) : 0);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.AgeBracket))
                            objVoiceMasterSetting.AgeBracket = (drow[VoiceMasterSettingDBFields.AgeBracket] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.AgeBracket]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VoiceProvider))
                            objVoiceMasterSetting.VoiceProvider = (drow[VoiceMasterSettingDBFields.VoiceProvider] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VoiceProvider]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.CharLimit))
                            objVoiceMasterSetting.CharLimit = (drow[VoiceMasterSettingDBFields.CharLimit] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.CharLimit]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.SampleUrl))
                            objVoiceMasterSetting.SampleUrl = (drow[VoiceMasterSettingDBFields.SampleUrl] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.SampleUrl]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMDisplayName))
                            objVoiceMasterSetting.VMDisplayName = (drow[VoiceMasterSettingDBFields.VMDisplayName] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMDisplayName]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMLocalName))
                            objVoiceMasterSetting.VMLocalName = (drow[VoiceMasterSettingDBFields.VMLocalName] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMLocalName]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMShortName))
                            objVoiceMasterSetting.VMShortName = (drow[VoiceMasterSettingDBFields.VMShortName] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMShortName]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMLocale))
                            objVoiceMasterSetting.VMLocale = (drow[VoiceMasterSettingDBFields.VMLocale] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMLocale]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMLocaleName))
                            objVoiceMasterSetting.VMLocaleName = (drow[VoiceMasterSettingDBFields.VMLocaleName] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMLocaleName]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMSampleRateHertz))
                            objVoiceMasterSetting.VMSampleRateHertz = (drow[VoiceMasterSettingDBFields.VMSampleRateHertz] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMSampleRateHertz]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMVoiceType))
                            objVoiceMasterSetting.VMVoiceType = (drow[VoiceMasterSettingDBFields.VMVoiceType] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMVoiceType]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMStatus))
                            objVoiceMasterSetting.VMStatus = (drow[VoiceMasterSettingDBFields.VMStatus] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMStatus]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMWordsPerMinute))
                            objVoiceMasterSetting.VMWordsPerMinute = (drow[VoiceMasterSettingDBFields.VMWordsPerMinute] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMWordsPerMinute]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMName))
                            objVoiceMasterSetting.VMName = (drow[VoiceMasterSettingDBFields.VMName] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMName]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMGender))
                            objVoiceMasterSetting.VMGender = (drow[VoiceMasterSettingDBFields.VMGender] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMGender]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMStyleList))
                            objVoiceMasterSetting.VMStyleList = (drow[VoiceMasterSettingDBFields.VMStyleList] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMStyleList]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMUUID))
                            objVoiceMasterSetting.VMUUID = (drow[VoiceMasterSettingDBFields.VMUUID] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMUUID]) : string.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(dataSet)", ex.Source, ex.Message, ex);
            }

            return objVoiceMasterSetting;
        }

        public VoiceMasterSetting GetDetails(DataTable dataTable)
        {
            VoiceMasterSetting objVoiceMasterSetting = new VoiceMasterSetting();

            try
            {
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow drow in dataTable.Rows)
                    {
                        objVoiceMasterSetting = new VoiceMasterSetting();

                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.ID))
                            objVoiceMasterSetting.ID = (drow[VoiceMasterSettingDBFields.ID] != DBNull.Value ? Convert.ToInt32(drow[VoiceMasterSettingDBFields.ID]) : 0);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.UUID))
                            objVoiceMasterSetting.UUID = (drow[VoiceMasterSettingDBFields.UUID] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.UUID]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.Description))
                            objVoiceMasterSetting.Description = (drow[VoiceMasterSettingDBFields.Description] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.Description]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VoiceImage))
                            objVoiceMasterSetting.VoiceImage = (drow[VoiceMasterSettingDBFields.VoiceImage] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VoiceImage]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.StatusId))
                            objVoiceMasterSetting.StatusId = (drow[VoiceMasterSettingDBFields.StatusId] != DBNull.Value ? Convert.ToByte(drow[VoiceMasterSettingDBFields.StatusId]) : (byte)0);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.CreatedDate))
                            objVoiceMasterSetting.CreatedDate = (drow[VoiceMasterSettingDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(drow[VoiceMasterSettingDBFields.CreatedDate]) : DateTime.Now);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.UpdatedDate))
                            objVoiceMasterSetting.UpdatedDate = (drow[VoiceMasterSettingDBFields.UpdatedDate] != DBNull.Value ? Convert.ToDateTime(drow[VoiceMasterSettingDBFields.UpdatedDate]) : DateTime.Now);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VoiceMasterID))
                            objVoiceMasterSetting.VoiceMasterID = (drow[VoiceMasterSettingDBFields.VoiceMasterID] != DBNull.Value ? Convert.ToInt32(drow[VoiceMasterSettingDBFields.VoiceMasterID]) : 0);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.AgeBracket))
                            objVoiceMasterSetting.AgeBracket = (drow[VoiceMasterSettingDBFields.AgeBracket] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.AgeBracket]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VoiceProvider))
                            objVoiceMasterSetting.VoiceProvider = (drow[VoiceMasterSettingDBFields.VoiceProvider] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VoiceProvider]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.CharLimit))
                            objVoiceMasterSetting.CharLimit = (drow[VoiceMasterSettingDBFields.CharLimit] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.CharLimit]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.SampleUrl))
                            objVoiceMasterSetting.SampleUrl = (drow[VoiceMasterSettingDBFields.SampleUrl] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.SampleUrl]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMDisplayName))
                            objVoiceMasterSetting.VMDisplayName = (drow[VoiceMasterSettingDBFields.VMDisplayName] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMDisplayName]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMLocalName))
                            objVoiceMasterSetting.VMLocalName = (drow[VoiceMasterSettingDBFields.VMLocalName] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMLocalName]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMShortName))
                            objVoiceMasterSetting.VMShortName = (drow[VoiceMasterSettingDBFields.VMShortName] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMShortName]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMLocale))
                            objVoiceMasterSetting.VMLocale = (drow[VoiceMasterSettingDBFields.VMLocale] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMLocale]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMLocaleName))
                            objVoiceMasterSetting.VMLocaleName = (drow[VoiceMasterSettingDBFields.VMLocaleName] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMLocaleName]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMSampleRateHertz))
                            objVoiceMasterSetting.VMSampleRateHertz = (drow[VoiceMasterSettingDBFields.VMSampleRateHertz] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMSampleRateHertz]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMVoiceType))
                            objVoiceMasterSetting.VMVoiceType = (drow[VoiceMasterSettingDBFields.VMVoiceType] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMVoiceType]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMStatus))
                            objVoiceMasterSetting.VMStatus = (drow[VoiceMasterSettingDBFields.VMStatus] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMStatus]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMWordsPerMinute))
                            objVoiceMasterSetting.VMWordsPerMinute = (drow[VoiceMasterSettingDBFields.VMWordsPerMinute] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMWordsPerMinute]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMName))
                            objVoiceMasterSetting.VMName = (drow[VoiceMasterSettingDBFields.VMName] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMName]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMGender))
                            objVoiceMasterSetting.VMGender = (drow[VoiceMasterSettingDBFields.VMGender] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMGender]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMStyleList))
                            objVoiceMasterSetting.VMStyleList = (drow[VoiceMasterSettingDBFields.VMStyleList] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMStyleList]) : string.Empty);
                        if (drow.Table.Columns.Contains(VoiceMasterSettingDBFields.VMUUID))
                            objVoiceMasterSetting.VMUUID = (drow[VoiceMasterSettingDBFields.VMUUID] != DBNull.Value ? Convert.ToString(drow[VoiceMasterSettingDBFields.VMUUID]) : string.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(DataTable)", ex.Source, ex.Message, ex);
            }

            return objVoiceMasterSetting;
        }
    }
}