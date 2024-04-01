using Core.Business.DataAccess.Constants;
using Core.Business.DataAccess.DataAccessLayer;
using Core.Business.DataAccess.DataAccessLayer.General;
using Core.Business.DataAccess.Mapper;
using Core.Entity;
using Core.Utility.Common;
using System.Data;
using System.Data.SqlClient;

namespace Core.Business.DataAccess.Wrapper
{
    public class VoiceMasterWrapper : UniversalObject
    {
        private readonly string _module = "Core.Business.DataAccess.Wrapper.VoiceMaster";
        private SqlConnection Connection;

        #region UniversalObject Interface Members

        public bool ObjectChanged { get; set; }

        public VoiceMaster objWrapperClass = new VoiceMaster();
        private VoiceMasterDataMapper objVoiceMasterDataMapper = new VoiceMasterDataMapper();

        #region GetRecords methods

        public bool GetRecordById(int id)
        {
            SqlDataReader sqlDataReader = null;
            try
            {
                if (this.Connection == null)
                    this.Connection = dbClass.GetSqlConnection();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = VoiceMasterStoredProcedures.VoiceMasterGetRecordById;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 0;
                sqlCommand.Connection = this.Connection;
                sqlCommand.Parameters.AddWithValue(VoiceMasterDBFields.ID, id);
                sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    objWrapperClass = objVoiceMasterDataMapper.GetDetails(sqlDataReader);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetRecordById(" + id + ")", ex.Source, ex.Message, ex);
                return false;
            }
            finally
            {
                if (sqlDataReader != null)
                    sqlDataReader.Close();
                dbClass.CloseSqlConnection(ref this.Connection);
            }
        }

        public bool GetRecordByValue(string fieldName, string value)
        {
            SqlDataReader sqlDataReader = null;
            try
            {
                if (this.Connection == null)
                    this.Connection = dbClass.GetSqlConnection();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = VoiceMasterStoredProcedures.GetVoiceMasterRecordByValue;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 0;
                sqlCommand.Connection = this.Connection;
                sqlCommand.Parameters.AddWithValue(fieldName, value);
                sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    objWrapperClass = objVoiceMasterDataMapper.GetDetails(sqlDataReader);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetRecordByValue(" + fieldName + "," + value + ")", ex.Source, ex.Message, ex);
                return false;
            }
            finally
            {
                if (sqlDataReader != null)
                    sqlDataReader.Close();
                dbClass.CloseSqlConnection(ref this.Connection);
            }
        }

        public bool GetRecordByValue(string[] fieldNames, string[] values)
        {
            SqlDataReader sqlDataReader = null;
            try
            {
                if (this.Connection == null)
                    this.Connection = dbClass.GetSqlConnection();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = VoiceMasterStoredProcedures.GetVoiceMasterRecordByValueArray;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 0;
                sqlCommand.Connection = this.Connection;
                for (int i = 0; i < fieldNames.Length; i++)
                {
                    sqlCommand.Parameters.AddWithValue(fieldNames[i], values[i]);
                }

                sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    objWrapperClass = objVoiceMasterDataMapper.GetDetails(sqlDataReader);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetRecordByValue(" + string.Join(",", fieldNames) + "," + string.Join(",", values) + ")", ex.Source, ex.Message, ex);
                return false;
            }
            finally
            {
                if (sqlDataReader != null)
                    sqlDataReader.Close();
                dbClass.CloseSqlConnection(ref this.Connection);
            }
        }

        #endregion GetRecords methods

        public bool Save(ref Dictionary<string, Command> commandList, ref int commandCounter)
        {
            try
            {
                if (objWrapperClass.ID > 0)
                {
                    Update(ref commandList, ref commandCounter);
                }
                else
                {
                    Command command = new Command(VoiceMasterStoredProcedures.VoiceMasterSAVE, CommandType.StoredProcedure);
                    command.AddParameter(VoiceMasterDBFields.IU_Flag, "I", DataAccessLayer.DataAccess.DataType.Char, 0, ParameterDirection.Input);
                    command.AddParameter(VoiceMasterDBFields.ID, objWrapperClass.ID, DataAccessLayer.DataAccess.DataType.Number, 0, ParameterDirection.Input);
                    command.AddParameter(VoiceMasterDBFields.Name, objWrapperClass.Name, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                    command.AddParameter(VoiceMasterDBFields.DisplayName, objWrapperClass.DisplayName, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                    command.AddParameter(VoiceMasterDBFields.LocalName, objWrapperClass.LocalName, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                    command.AddParameter(VoiceMasterDBFields.ShortName, objWrapperClass.ShortName, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                    command.AddParameter(VoiceMasterDBFields.Gender, objWrapperClass.Gender, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                    command.AddParameter(VoiceMasterDBFields.Locale, objWrapperClass.Locale, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                    command.AddParameter(VoiceMasterDBFields.LocaleName, objWrapperClass.LocaleName, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                    command.AddParameter(VoiceMasterDBFields.SampleRateHertz, objWrapperClass.SampleRateHertz, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                    command.AddParameter(VoiceMasterDBFields.VoiceType, objWrapperClass.VoiceType, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                    command.AddParameter(VoiceMasterDBFields.Status, objWrapperClass.Status, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                    command.AddParameter(VoiceMasterDBFields.WordsPerMinute, objWrapperClass.WordsPerMinute, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                    command.AddParameter(VoiceMasterDBFields.StatusId, objWrapperClass.StatusId, DataAccessLayer.DataAccess.DataType.Varchar2, 0, ParameterDirection.Input);
                    command.AddParameter(VoiceMasterDBFields.CreatedDate, objWrapperClass.CreatedDate, DataAccessLayer.DataAccess.DataType.DateTime, 0, ParameterDirection.Input);
                    command.AddParameter(VoiceMasterDBFields.UpdateDate, objWrapperClass.UpdateDate, DataAccessLayer.DataAccess.DataType.DateTime, 0, ParameterDirection.Input);

                    command.Name = VoiceMasterStoredProcedures.VoiceMasterSAVE + commandCounter.ToString();
                    commandCounter++;
                    commandList.Add(command.Name, command);
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "Save", ex.Source, ex.Message, ex);
                return false;
            }
            finally
            {
            }
        }

        public bool Update(ref Dictionary<string, Command> commandList, ref int commandCounter)
        {
            try
            {
                Command command = new Command(VoiceMasterStoredProcedures.VoiceMasterSAVE, CommandType.StoredProcedure);

                command.AddParameter(VoiceMasterDBFields.IU_Flag, "U", DataAccessLayer.DataAccess.DataType.Char, 0, ParameterDirection.Input);
                command.AddParameter(VoiceMasterDBFields.ID, objWrapperClass.ID, DataAccessLayer.DataAccess.DataType.Number, 0, ParameterDirection.Input);
                command.AddParameter(VoiceMasterDBFields.Name, objWrapperClass.Name, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                command.AddParameter(VoiceMasterDBFields.DisplayName, objWrapperClass.DisplayName, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                command.AddParameter(VoiceMasterDBFields.LocalName, objWrapperClass.LocalName, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                command.AddParameter(VoiceMasterDBFields.ShortName, objWrapperClass.ShortName, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                command.AddParameter(VoiceMasterDBFields.Gender, objWrapperClass.Gender, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                command.AddParameter(VoiceMasterDBFields.Locale, objWrapperClass.Locale, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                command.AddParameter(VoiceMasterDBFields.LocaleName, objWrapperClass.LocaleName, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                command.AddParameter(VoiceMasterDBFields.SampleRateHertz, objWrapperClass.SampleRateHertz, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                command.AddParameter(VoiceMasterDBFields.VoiceType, objWrapperClass.VoiceType, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                command.AddParameter(VoiceMasterDBFields.Status, objWrapperClass.Status, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                command.AddParameter(VoiceMasterDBFields.WordsPerMinute, objWrapperClass.WordsPerMinute, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                command.AddParameter(VoiceMasterDBFields.StatusId, objWrapperClass.StatusId, DataAccessLayer.DataAccess.DataType.Varchar2, 0, ParameterDirection.Input);
                command.AddParameter(VoiceMasterDBFields.CreatedDate, objWrapperClass.CreatedDate, DataAccessLayer.DataAccess.DataType.DateTime, 0, ParameterDirection.Input);
                command.AddParameter(VoiceMasterDBFields.UpdateDate, objWrapperClass.UpdateDate, DataAccessLayer.DataAccess.DataType.DateTime, 0, ParameterDirection.Input);

                command.Name = VoiceMasterStoredProcedures.VoiceMasterSAVE + commandCounter.ToString();
                commandCounter++;
                commandList.Add(command.Name, command);

                return true;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "Update", ex.Source, ex.Message, ex);
                return false;
            }
            finally
            {
            }
        }

        public bool SaveVoiceToUser(ref Dictionary<string, Command> commandList, ref int commandCounter)
        {
            try
            {
                if (objWrapperClass.ID > 0)
                {
                    Update(ref commandList, ref commandCounter);
                }
                else
                {
                    Command command = new Command(VoiceMasterStoredProcedures.VoiceMasterUserMapSAVE, CommandType.StoredProcedure);
                    //command.AddParameter(VoiceMasterDBFields.IU_Flag, "I", DataAccessLayer.DataAccess.DataType.Char, 0, ParameterDirection.Input);

                    command.AddParameter(VoiceMasterDBFields.VoiceMasterUserMapID, objWrapperClass.VoiceMasterUserMapID, DataAccessLayer.DataAccess.DataType.Number, 0, ParameterDirection.Input);
                    command.AddParameter(VoiceMasterDBFields.VoiceID, objWrapperClass.VoiceID, DataAccessLayer.DataAccess.DataType.Number, 0, ParameterDirection.Input);
                    command.AddParameter(VoiceMasterDBFields.UserID, objWrapperClass.UserID, DataAccessLayer.DataAccess.DataType.Number, 0, ParameterDirection.Input);
                    command.AddParameter(VoiceMasterDBFields.Type, objWrapperClass.Type, DataAccessLayer.DataAccess.DataType.Number, 0, ParameterDirection.Input);

                    command.Name = VoiceMasterStoredProcedures.VoiceMasterUserMapSAVE + commandCounter.ToString();
                    commandCounter++;
                    commandList.Add(command.Name, command);
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "Save", ex.Source, ex.Message, ex);
                return false;
            }
            finally
            {
            }
        }

        public bool Delete(ref Dictionary<string, Command> commandList, ref int commandCounter)
        {
            try
            {
                Command command = new Command("SP_MASTERS_Delete", CommandType.StoredProcedure);
                command.AddParameter("@TableName", "VoiceMaster", DataAccessLayer.DataAccess.DataType.Varchar, 0, ParameterDirection.Input);
                command.AddParameter("@PrimaryKeyColumn", "ID", DataAccessLayer.DataAccess.DataType.Number, 0, ParameterDirection.Input);
                command.AddParameter("@IDs", objWrapperClass.ID, DataAccessLayer.DataAccess.DataType.Number, 0, ParameterDirection.Input);
                command.Name = "DeleteVoiceMaster" + commandCounter.ToString();
                commandCounter++;
                commandList.Add(command.Name, command);

                return true;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "Delete", ex.Source, ex.Message, ex);
                return false;
            }
            finally
            {
            }
        }

        public byte UpdateVoice(Int64 UserID)
        {
            byte retVal = 0;
            try
            {
                if (this.Connection == null)
                    this.Connection = dbClass.GetSqlConnection();

                SqlCommand sqlCommand = new SqlCommand(VoiceMasterStoredProcedures.VoiceMasterUserMap_UpdateByUserID, this.Connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue(VoiceMasterDBFields.UserID, UserID);
                //sqlCommand.Parameters.AddWithValue(UsersDBFields.StatusId, StatusID);

                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "ChangeStatus(UserID=" + UserID + ")", ex.Source, ex.Message, ex);
            }
            finally
            {
                dbClass.CloseSqlConnection(ref this.Connection);
            }

            return retVal;
        }

        public VoiceMaster GetVoiceByShortName(string shortName, Int64 userid)
        {
            SqlDataReader sqlDataReader = null;
            VoiceMasterDataMapper objDataMapper = new VoiceMasterDataMapper();

            try
            {
                if (this.Connection == null)
                    this.Connection = dbClass.GetSqlConnection();

                SqlCommand sqlCommand = new SqlCommand(VoiceMasterStoredProcedures.VoiceMasterGetRecordByShortName, this.Connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue(VoiceMasterDBFields.ShortName, shortName);
                sqlCommand.Parameters.AddWithValue(VoiceMasterDBFields.UserID, userid);
                sqlDataReader = sqlCommand.ExecuteReader();
                VoiceMaster voiceMaster = new VoiceMaster();
                voiceMaster = objDataMapper.GetDetail(sqlDataReader);

                return voiceMaster;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetLanguageRelatedValues()", ex.Source, ex.Message, ex);
            }
            finally
            {
                dbClass.CloseSqlConnection(ref this.Connection);
            }

            return new VoiceMaster();
        }

        public bool SaveCopy()
        {
            throw new NotImplementedException();
        }

        public bool Move()
        {
            throw new NotImplementedException();
        }

        public bool Print()
        {
            throw new NotImplementedException();
        }

        #endregion UniversalObject Interface Members
    }

    public class VoiceMasterWrapperColletion : UniversalCollection
    {
        private readonly string _module = "VoiceMaster";
        private SqlConnection Connection;
        private List<VoiceMaster> _Items = new List<VoiceMaster>();

        public List<VoiceMaster> Items
        { get { return this._Items; } set { this._Items = value; } }

        private DataSet _DtsDataset = null;
        private string _SortingString = "";

        #region UniversalCollection Interface Members Implementation

        #region GetRecords methods

        public bool GetRecords(bool createDataSet, string[,] sortFields)
        {
            if (createDataSet)
                return GetDataSetForQuery(VoiceMasterStoredProcedures.GetVoiceMasterRecords);
            else
                return GetCollectionForQuery(VoiceMasterStoredProcedures.GetVoiceMasterRecords);
        }

        public bool GetRecords(bool createDataSet, string[,] sortFields, bool isParameter)
        {
            if (sortFields != null)
            {
                if (sortFields.Length > 0)
                {
                    _SortingString += "order by ";
                    for (int i = 0; i <= sortFields.GetUpperBound(0); i++)
                    {
                        _SortingString += "" + sortFields[i, 0] + " " + sortFields[i, 1] + ",";
                    }
                    _SortingString = _SortingString.Substring(0, _SortingString.Length - 1);
                }
            }

            SqlParameterCollection sqlParameterCollection = null;
            SqlParameter ObjSqlParameter = new SqlParameter();
            ObjSqlParameter.ParameterName = VoiceMasterStoredProcedures.SortingString;
            ObjSqlParameter.Value = _SortingString;
            sqlParameterCollection.Add(ObjSqlParameter);

            if (createDataSet)
                return GetDataSetForQueryParameter(VoiceMasterStoredProcedures.GetVoiceMasterRecords, sqlParameterCollection);
            else
                return GetDataSetForQueryParameter(VoiceMasterStoredProcedures.GetVoiceMasterRecords, sqlParameterCollection);
        }

        public bool GetRecords(bool createDataSet, string[,] sortFields, bool isParameter, string fieldName, string fieldValue)
        {
            if (sortFields != null)
            {
                if (sortFields.Length > 0)
                {
                    _SortingString += "order by ";
                    for (int i = 0; i <= sortFields.GetUpperBound(0); i++)
                    {
                        _SortingString += "" + sortFields[i, 0] + " " + sortFields[i, 1] + ",";
                    }
                    _SortingString = _SortingString.Substring(0, _SortingString.Length - 1);
                }
            }

            string[] Fieldsname = new string[1];
            string[] Values = new string[1];
            Fieldsname[0] = fieldName;
            Values[0] = fieldValue;

            return GetCollectionForQueryWithParameters(VoiceMasterStoredProcedures.GetVoiceMasterRecordByValue, Fieldsname, Values);
        }

        private bool GetCollectionForQueryWithParameters(string sqlQuery, string[] fieldNames, string[] values)
        {
            SqlDataReader _Dtr = null;
            try
            {
                if (this.Connection == null)
                    this.Connection = dbClass.GetSqlConnection();

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sqlQuery;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 0;
                sqlCommand.Connection = this.Connection;

                if (fieldNames != null)
                {
                    if (fieldNames.Length > 0)
                    {
                        for (int i = 0; i < fieldNames.Length; i++)
                        {
                            SqlParameter sqlParameter = new SqlParameter();
                            sqlParameter.ParameterName = fieldNames[i];
                            sqlParameter.Value = values[i];
                            sqlCommand.Parameters.Add(sqlParameter);
                        }
                    }
                }

                _Dtr = sqlCommand.ExecuteReader();
                while (_Dtr.Read())
                {
                    VoiceMasterDataMapper objDataMapper = new VoiceMasterDataMapper();
                    this.Items.Add(objDataMapper.GetDetails(_Dtr));
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetCollectionForQueryWithParameters(" + sqlQuery + ")", ex.Source, ex.Message, ex);
                return false;
            }
            finally
            {
                if (_Dtr != null)
                    _Dtr.Close();
                dbClass.CloseSqlConnection(ref this.Connection);
            }
        }

        public bool GetRecords(bool createDataSet, string[,] sortFields, bool isParameter, string[] fieldName, string[] fieldValue)
        {
            SqlParameterCollection sqlParameterCollection = null;
            if (fieldName != null)
            {
                if (fieldName.Length > 0)
                {
                    for (int i = 0; i < fieldName.Length; i++)
                    {
                        SqlParameter sqlParameter = new SqlParameter();
                        sqlParameter.ParameterName = fieldName[i];
                        sqlParameter.Value = fieldValue[i];
                        sqlParameterCollection.Add(sqlParameter);
                    }
                }
            }
            if (sortFields != null)
            {
                if (sortFields.Length > 0)
                {
                    _SortingString += "order by ";
                    for (int i = 0; i <= sortFields.GetUpperBound(0); i++)
                    {
                        _SortingString += "" + sortFields[i, 0] + " " + sortFields[i, 1] + ",";
                    }
                    _SortingString = _SortingString.Substring(0, _SortingString.Length - 1);
                }

                SqlParameter ObjSqlParameter = new SqlParameter();
                ObjSqlParameter.ParameterName = VoiceMasterStoredProcedures.SortingString;
                ObjSqlParameter.Value = _SortingString;
                sqlParameterCollection.Add(ObjSqlParameter);
            }

            if (createDataSet)
                return GetDataSetForQueryParameter(VoiceMasterStoredProcedures.GetVoiceMasterRecords, sqlParameterCollection);
            else
                return GetDataSetForQueryParameter(VoiceMasterStoredProcedures.GetVoiceMasterRecords, sqlParameterCollection);
        }

        #endregion GetRecords methods

        #region Seach Method

        public bool Search(string searchString, string[,] sortString)
        {
            throw new NotImplementedException();
        }

        public bool Search(string fieldName, string fieldValue, string[,] sortString)
        {
            try
            {
                SqlParameterCollection sqlParameterCollection = null;
                SqlParameter ObjSqlParameter = new SqlParameter();
                ObjSqlParameter.ParameterName = fieldName;
                ObjSqlParameter.Value = fieldValue;
                sqlParameterCollection.Add(ObjSqlParameter);

                GetCollectionForQueryWithParameter(VoiceMasterStoredProcedures.VoiceMasterSearch, sqlParameterCollection);
                return true;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "Search(" + fieldName + "," + fieldValue + ")", ex.Source, ex.Message, ex);
                return false;
            }
            finally
            {
                dbClass.CloseSqlConnection(ref this.Connection);
            }
        }

        public bool Search(string searchString, bool createDataSet, string[,] sortFields)
        {
            throw new NotImplementedException();
        }

        public bool Search(string fieldName, string fieldValue, bool createDataSet, string[,] sortFields)
        {
            try
            {
                SqlParameterCollection sqlParameterCollection = null;

                SqlParameter sqlParameter = new SqlParameter();
                sqlParameter.ParameterName = fieldName;
                sqlParameter.Value = fieldValue;
                sqlParameterCollection.Add(sqlParameter);

                SqlParameter ObjSqlParameter = new SqlParameter();
                ObjSqlParameter.ParameterName = VoiceMasterStoredProcedures.SortingString;
                ObjSqlParameter.Value = _SortingString;
                sqlParameterCollection.Add(ObjSqlParameter);

                if (createDataSet)
                    return GetDataSetForQueryParameter(VoiceMasterStoredProcedures.VoiceMasterSearchByValue, sqlParameterCollection);
                else
                    return GetDataSetForQueryParameter(VoiceMasterStoredProcedures.VoiceMasterSearchByValue, sqlParameterCollection);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "Search(" + fieldName + "," + fieldValue + ")", ex.Source, ex.Message, ex);
                return false;
            }
            finally
            {
                dbClass.CloseSqlConnection(ref this.Connection);
            }
        }

        public bool Search(string[] fieldName, string[] fieldValue, bool createDataSet, string[,] sortFields)
        {
            try
            {
                SqlParameterCollection sqlParameterCollection = null;
                if (fieldName != null)
                {
                    if (fieldName.Length > 0)
                    {
                        for (int i = 0; i < fieldName.Length; i++)
                        {
                            SqlParameter sqlParameter = new SqlParameter();
                            sqlParameter.ParameterName = fieldName[i];
                            sqlParameter.Value = fieldValue[i];
                            sqlParameterCollection.Add(sqlParameter);
                        }
                    }
                }
                if (sortFields != null)
                {
                    if (sortFields.Length > 0)
                    {
                        _SortingString += "order by ";
                        for (int i = 0; i <= sortFields.GetUpperBound(0); i++)
                        {
                            _SortingString += "" + sortFields[i, 0] + " " + sortFields[i, 1] + ",";
                        }
                        _SortingString = _SortingString.Substring(0, _SortingString.Length - 1);
                    }

                    SqlParameter ObjSqlParameter = new SqlParameter();
                    ObjSqlParameter.ParameterName = VoiceMasterStoredProcedures.SortingString;
                    ObjSqlParameter.Value = _SortingString;
                    sqlParameterCollection.Add(ObjSqlParameter);
                }

                if (createDataSet)
                    return GetDataSetForQueryParameter(VoiceMasterStoredProcedures.VoiceMasterSearchByValueArray, sqlParameterCollection);
                else
                    return GetDataSetForQueryParameter(VoiceMasterStoredProcedures.VoiceMasterSearchByValueArray, sqlParameterCollection);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "Search(" + fieldName + "," + fieldValue + ")", ex.Source, ex.Message, ex);
                return false;
            }
            finally
            {
                dbClass.CloseSqlConnection(ref this.Connection);
            }
        }

        #endregion Seach Method

        #region ExecuteQuery Methods

        private bool GetDataSetForQuery(string sqlQuery)
        {
            try
            {
                DataSet _DtsUsers = new DataSet("VoiceMaster");
                SqlDataAdapter _Adpusers = new SqlDataAdapter(sqlQuery, this.Connection);
                _Adpusers.Fill(_DtsUsers);
                this._DtsDataset = _DtsUsers;
                return true;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDataSetForQuery(" + sqlQuery + ")", ex.Source, ex.Message, ex);
                return false;
            }
            finally { }
        }

        private bool GetDataSetForQueryParameter(string sqlQuery, SqlParameterCollection ObjSqlParameter)
        {
            try
            {
                DataSet _DtsUsers = new DataSet("VoiceMaster");
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, this.Connection);
                sqlCommand.CommandText = sqlQuery;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 0;
                sqlCommand.Parameters.Add(ObjSqlParameter);
                SqlDataAdapter _Adpusers = new SqlDataAdapter();
                _Adpusers.SelectCommand = sqlCommand;
                _Adpusers.Fill(this._DtsDataset);
                return true;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDataSetForQuery(" + sqlQuery + ")", ex.Source, ex.Message, ex);
                return false;
            }
            finally { }
        }

        private bool GetCollectionForQuery(string sqlQuery)
        {
            SqlDataReader _Dtr = null;
            try
            {
                if (this.Connection == null)
                    this.Connection = dbClass.GetSqlConnection();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sqlQuery;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 0;
                sqlCommand.Connection = this.Connection;

                _Dtr = sqlCommand.ExecuteReader();
                while (_Dtr.Read())
                {
                    VoiceMasterDataMapper objVoiceMasterDataMapper = new VoiceMasterDataMapper();
                    this.Items.Add(objVoiceMasterDataMapper.GetDetails(_Dtr));
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetCollectionForQuery(" + sqlQuery + ")", ex.Source, ex.Message, ex);
                return false;
            }
            finally
            {
                if (_Dtr != null)
                    _Dtr.Close();
                dbClass.CloseSqlConnection(ref this.Connection);
            }
        }

        private bool GetCollectionForQueryWithParameter(string sqlQuery, SqlParameterCollection ObjSqlParameter)
        {
            SqlDataReader _Dtr = null;
            try
            {
                if (this.Connection == null)
                    this.Connection = dbClass.GetSqlConnection();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sqlQuery;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 0;
                sqlCommand.Parameters.Add(ObjSqlParameter);
                sqlCommand.Connection = this.Connection;
                _Dtr = sqlCommand.ExecuteReader();
                while (_Dtr.Read())
                {
                    VoiceMasterDataMapper objVoiceMasterDataMapper = new VoiceMasterDataMapper();
                    this.Items.Add(objVoiceMasterDataMapper.GetDetails(_Dtr));
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetCollectionForQuery(" + sqlQuery + ")", ex.Source, ex.Message, ex);
                return false;
            }
            finally
            {
                if (_Dtr != null)
                    _Dtr.Close();
                dbClass.CloseSqlConnection(ref this.Connection);
            }
        }

        #endregion ExecuteQuery Methods

        public bool Save(ref Dictionary<string, Command> commandList, ref int commandCounter)
        {
            try
            {
                VoiceMasterWrapper objVoiceMasterWrapper = new VoiceMasterWrapper();
                for (int i = 0; i < this.Items.Count; i++)
                {
                    if (this.Items[i].ObjectChanged == true)
                    {
                        Dictionary<string, Command> subCommands = new Dictionary<string, Command>();
                        objVoiceMasterWrapper.Save(ref subCommands, ref commandCounter);
                        foreach (KeyValuePair<string, Command> commandPair in subCommands)
                        {
                            commandList.Add(commandPair.Key, commandPair.Value);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "Save", ex.Source, ex.Message, ex);
                return false;
            }
            finally
            {
            }
        }

        public bool Delete(string ids, ref Dictionary<string, Command> commandList, ref int commandCounter)
        {
            try
            {
                Command command = new Command("SP_MASTERS_DELETE", CommandType.StoredProcedure);
                command.AddParameter("@TableName", VoiceMasterDBFields.TableNameVal, DataAccessLayer.DataAccess.DataType.Varchar, 0, ParameterDirection.Input);
                command.AddParameter("@PrimaryKeyColumn", VoiceMasterDBFields.ID, DataAccessLayer.DataAccess.DataType.Varchar, 0, ParameterDirection.Input);
                command.AddParameter("@IDs", ids, DataAccessLayer.DataAccess.DataType.Varchar, 0, ParameterDirection.Input);
                command.Name = "Delete" + VoiceMasterDBFields.TableNameVal + commandCounter.ToString();
                commandCounter++;
                commandList.Add(command.Name, command);

                return true;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "Delete", ex.Source, ex.Message, ex);
                return false;
            }
            finally
            {
            }
        }

        object UniversalCollection.GetRecordById(int id)
        {
            throw new NotImplementedException();
        }

        object UniversalCollection.GetRecordByValue(string fieldName, string value)
        {
            throw new NotImplementedException();
        }

        #endregion UniversalCollection Interface Members Implementation

        #region Get Methods

        public dynamic VoiceMasterGetList(Int64 UserID)
        {
            SqlDataReader sqlDataReader = null;
            VoiceMasterDataMapper objDataMapper = new VoiceMasterDataMapper();

            try
            {
                if (this.Connection == null)
                    this.Connection = dbClass.GetSqlConnection();

                SqlCommand sqlCommand = new SqlCommand(VoiceMasterStoredProcedures.VoiceMasterGetRecords, this.Connection);
                sqlCommand.Parameters.AddWithValue(VoiceMasterDBFields.UserID, UserID);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlDataReader = sqlCommand.ExecuteReader();

                _Items = objDataMapper.GetDetailsList(sqlDataReader);

                return _Items;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetVoiceMasterList()", ex.Source, ex.Message, ex);
            }
            finally
            {
                dbClass.CloseSqlConnection(ref this.Connection);
            }

            return _Items;
        }

        public dynamic VoiceMaster_GetLanguage(string type, Int64 userid)
        {
            SqlDataReader sqlDataReader = null;
            VoiceMasterDataMapper objDataMapper = new VoiceMasterDataMapper();

            try
            {
                if (this.Connection == null)
                    this.Connection = dbClass.GetSqlConnection();

                SqlCommand sqlCommand = new SqlCommand(VoiceMasterStoredProcedures.VoiceMaster_GetLanguage, this.Connection);
                sqlCommand.Parameters.AddWithValue(VoiceMasterDBFields.Type, type);
                sqlCommand.Parameters.AddWithValue(VoiceMasterDBFields.UserID, userid);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlDataReader = sqlCommand.ExecuteReader();

                _Items = objDataMapper.GetDetailsList(sqlDataReader);

                return _Items;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetVoiceMasterList()", ex.Source, ex.Message, ex);
            }
            finally
            {
                dbClass.CloseSqlConnection(ref this.Connection);
            }

            return _Items;
        }

        public dynamic VoiceMaster_GetAccent(string language, Int64 userid)
        {
            SqlDataReader sqlDataReader = null;
            VoiceMasterDataMapper objDataMapper = new VoiceMasterDataMapper();

            try
            {
                if (this.Connection == null)
                    this.Connection = dbClass.GetSqlConnection();

                SqlCommand sqlCommand = new SqlCommand(VoiceMasterStoredProcedures.VoiceMaster_GetAccent, this.Connection);
                sqlCommand.Parameters.AddWithValue(VoiceMasterDBFields.Locale, language);
                sqlCommand.Parameters.AddWithValue(VoiceMasterDBFields.UserID, userid);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlDataReader = sqlCommand.ExecuteReader();

                _Items = objDataMapper.GetDetailsList(sqlDataReader);

                return _Items;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetVoiceMasterList()", ex.Source, ex.Message, ex);
            }
            finally
            {
                dbClass.CloseSqlConnection(ref this.Connection);
            }

            return _Items;
        }

        public dynamic VoiceMaster_GetVoice(string language, Int64 userid)
        {
            SqlDataReader sqlDataReader = null;
            VoiceMasterDataMapper objDataMapper = new VoiceMasterDataMapper();

            try
            {
                if (this.Connection == null)
                    this.Connection = dbClass.GetSqlConnection();

                SqlCommand sqlCommand = new SqlCommand(VoiceMasterStoredProcedures.VoiceMaster_GetVoice, this.Connection);
                sqlCommand.Parameters.AddWithValue(VoiceMasterDBFields.Locale, language);
                sqlCommand.Parameters.AddWithValue(VoiceMasterDBFields.UserID, userid);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlDataReader = sqlCommand.ExecuteReader();

                _Items = objDataMapper.GetDetailsList(sqlDataReader);

                return _Items;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetVoiceMasterList()", ex.Source, ex.Message, ex);
            }
            finally
            {
                dbClass.CloseSqlConnection(ref this.Connection);
            }

            return _Items;
        }

        public dynamic VoiceMaster_GetStyle(string language)
        {
            SqlDataReader sqlDataReader = null;
            VoiceMasterDataMapper objDataMapper = new VoiceMasterDataMapper();

            try
            {
                if (this.Connection == null)
                    this.Connection = dbClass.GetSqlConnection();

                SqlCommand sqlCommand = new SqlCommand(VoiceMasterStoredProcedures.VoiceMaster_GetStyle, this.Connection);
                sqlCommand.Parameters.AddWithValue(VoiceMasterDBFields.Locale, language);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlDataReader = sqlCommand.ExecuteReader();

                _Items = objDataMapper.GetDetailsList(sqlDataReader);

                return _Items;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetVoiceMasterList()", ex.Source, ex.Message, ex);
            }
            finally
            {
                dbClass.CloseSqlConnection(ref this.Connection);
            }

            return _Items;
        }

        public dynamic VoiceMasterGetListByUserID(Int64 UserID)
        {
            SqlDataReader sqlDataReader = null;
            VoiceMasterDataMapper objDataMapper = new VoiceMasterDataMapper();

            try
            {
                if (this.Connection == null)
                    this.Connection = dbClass.GetSqlConnection();

                SqlCommand sqlCommand = new SqlCommand(VoiceMasterStoredProcedures.VoiceMasterByUserIDGetRecords, this.Connection);
                sqlCommand.Parameters.AddWithValue(VoiceMasterDBFields.UserID, UserID);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlDataReader = sqlCommand.ExecuteReader();

                _Items = objDataMapper.GetDetailsList(sqlDataReader);

                return _Items;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetVoiceMasterList()", ex.Source, ex.Message, ex);
            }
            finally
            {
                dbClass.CloseSqlConnection(ref this.Connection);
            }

            return _Items;
        }

        public dynamic GetDropDownValues()
        {
            SqlDataReader sqlDataReader = null;
            VoiceMasterDataMapper objDataMapper = new VoiceMasterDataMapper();

            try
            {
                if (this.Connection == null)
                    this.Connection = dbClass.GetSqlConnection();

                SqlCommand sqlCommand = new SqlCommand(VoiceMasterStoredProcedures.GetVoiceMasterRecordByValue, this.Connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlDataReader = sqlCommand.ExecuteReader();

                _Items = objDataMapper.GetDetailsList(sqlDataReader);

                return _Items;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDropDownValues()", ex.Source, ex.Message, ex);
            }
            finally
            {
                dbClass.CloseSqlConnection(ref this.Connection);
            }

            return _Items;
        }

        public dynamic GetLanguageRelatedValues(string language)
        {
            SqlDataReader sqlDataReader = null;
            VoiceMasterDataMapper objDataMapper = new VoiceMasterDataMapper();

            try
            {
                if (this.Connection == null)
                    this.Connection = dbClass.GetSqlConnection();

                SqlCommand sqlCommand = new SqlCommand(VoiceMasterStoredProcedures.GetVoiceMasterRecordByLanguageValue, this.Connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue(VoiceMasterDBFields.Locale, language);
                sqlDataReader = sqlCommand.ExecuteReader();

                _Items = objDataMapper.GetDetailsList(sqlDataReader);

                return _Items;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetLanguageRelatedValues()", ex.Source, ex.Message, ex);
            }
            finally
            {
                dbClass.CloseSqlConnection(ref this.Connection);
            }

            return _Items;
        }

        public dynamic GetVoiceRecord(string userAPIKey)
        {
            SqlDataReader sqlDataReader = null;
            VoiceMasterDataMapper objDataMapper = new VoiceMasterDataMapper();

            try
            {
                if (this.Connection == null)
                    this.Connection = dbClass.GetSqlConnection();

                SqlCommand sqlCommand = new SqlCommand(VoiceMasterStoredProcedures.VoiceMasterUserMap_GetVoiceByUserID, this.Connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue(VoiceMasterDBFields.UserAPIKey, userAPIKey);
                sqlDataReader = sqlCommand.ExecuteReader();

                _Items = objDataMapper.GetDetailsList(sqlDataReader);

                List<VoiceMaster> voiceMasterTags = new List<VoiceMaster>();
                if (sqlDataReader.NextResult())
                {
                    voiceMasterTags = objDataMapper.GetDetailsList(sqlDataReader);
                }

                List<VoiceMaster> voiceMasterBestFor = new List<VoiceMaster>();
                if (sqlDataReader.NextResult())
                {
                    voiceMasterBestFor = objDataMapper.GetDetailsList(sqlDataReader);
                }
                List<VoiceMaster> voiceMasterStyleList = new List<VoiceMaster>();
                if (sqlDataReader.NextResult())
                {
                    voiceMasterStyleList = objDataMapper.GetDetailsList(sqlDataReader);
                }

                var result1 = from f in voiceMasterTags
                              select new
                              {
                                  f.ID,
                                  f.TagName,
                                  f.VoiceMasterID,
                              };
                var result2 = from f in voiceMasterBestFor
                              select new
                              {
                                  f.ID,
                                  f.BestForName,
                                  f.VoiceMasterID,
                              };
                var result3 = from f in voiceMasterStyleList
                              select new
                              {
                                  f.ID,
                                  f.StyleName,
                                  f.VoiceMasterID,
                              };

                //var result = from f in _Items
                //             join s in result1 on f.VoiceMasterID equals s.VoiceMasterID into g1
                //             join t in result2 on f.VoiceMasterID equals t.VoiceMasterID into g2
                //             join b in result3 on f.VoiceMasterID equals b.VoiceMasterID into g3
                //             select new
                //             {
                //                 f.ampVoiceProvider,
                //                 f.ampDisplayName,
                //                 f.ampvoiceUUID,
                //                 f.ampvoiceGender,
                //                 f.ampvoicePhoto,
                //                 f.ampvoicesampleMp3,
                //                 f.ampvoicedescription,
                //                 f.ampvoicelocale,
                //                 f.ampvoicelocaleID,
                //                 f.ampvoiceaccent,
                //                 f.ampvoiceaccentID,
                //                 f.ampvoiceagerange,
                //                 f.ampvoiceType,
                //                 f.ampSampleRateHertz,
                //                 f.ampStatus,
                //                 f.ampWordsPerMinute,
                //                 f.ampCharacterLimit,
                //                 amptags = g1.Any() ? string.Join(", ", g1.Select(n => n.TagName)) : null,
                //                 ampStyleList = g2.Any() ? string.Join(", ", g2.Select(n => n.BestForName)) : null,
                //                 ampbestFor = g3.Any() ? string.Join(", ", g3.Select(n => n.StyleName)) : null
                //             };
                var result = (from f in _Items
                              join s in result1 on f.VoiceMasterID equals s.VoiceMasterID into g1
                              join t in result2 on f.VoiceMasterID equals t.VoiceMasterID into g2
                              join b in result3 on f.VoiceMasterID equals b.VoiceMasterID into g3
                              select new
                              {
                                  f.ampVoiceProvider,
                                  f.ampDisplayName,
                                  f.ampvoiceUUID,
                                  f.ampvoiceGender,
                                  f.ampvoicePhoto,
                                  f.ampvoicesampleMp3,
                                  f.ampvoicedescription,
                                  f.ampvoicelocale,
                                  f.ampvoicelocaleID,
                                  f.ampvoiceaccent,
                                  f.ampvoiceaccentID,
                                  f.ampshortName,
                                  f.ampvoiceagerange,
                                  f.ampvoiceType,
                                  f.ampSampleRateHertz,
                                  f.ampStatus,
                                  f.ampWordsPerMinute,
                                  f.ampCharacterLimit,
                                  amptagsArray = g1.Any() ? g1.Select(n => n.TagName).ToArray() : new string[0],
                                  ampStyleListArray = g2.Any() ? g2.Select(n => n.BestForName).ToArray() : new string[0],
                                  ampbestForArray = g3.Any() ? g3.Select(n => n.StyleName).ToArray() : new string[0]
                              }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetVoiceRecord()", ex.Source, ex.Message, ex);
            }
            finally
            {
                dbClass.CloseSqlConnection(ref this.Connection);
            }

            return _Items;
        }

        public dynamic UpdateMappedVoice(Int64 UserID)
        {
            SqlDataReader sqlDataReader = null;
            VoiceMasterDataMapper objDataMapper = new VoiceMasterDataMapper();

            try
            {
                if (this.Connection == null)
                    this.Connection = dbClass.GetSqlConnection();

                SqlCommand sqlCommand = new SqlCommand(VoiceMasterStoredProcedures.VoiceMasterUserMap_UpdateByUserID, this.Connection);
                sqlCommand.Parameters.AddWithValue(VoiceMasterDBFields.UserID, UserID);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlDataReader = sqlCommand.ExecuteReader();

                _Items = objDataMapper.GetDetailsList(sqlDataReader);

                return _Items;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetVoiceMasterList()", ex.Source, ex.Message, ex);
            }
            finally
            {
                dbClass.CloseSqlConnection(ref this.Connection);
            }

            return _Items;
        }

        public dynamic VoiceMasterGetRecordByAccent(string Locale)
        {
            SqlDataReader sqlDataReader = null;
            VoiceMasterDataMapper objDataMapper = new VoiceMasterDataMapper();

            try
            {
                if (this.Connection == null)
                    this.Connection = dbClass.GetSqlConnection();

                SqlCommand sqlCommand = new SqlCommand(VoiceMasterStoredProcedures.VoiceMasterGetRecordByAccent, this.Connection);
                sqlCommand.Parameters.AddWithValue(VoiceMasterDBFields.Locale, Locale);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlDataReader = sqlCommand.ExecuteReader();

                _Items = objDataMapper.GetDetailsList(sqlDataReader);

                return _Items;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "VoiceMasterGetRecordByAccent()", ex.Source, ex.Message, ex);
            }
            finally
            {
                dbClass.CloseSqlConnection(ref this.Connection);
            }

            return _Items;
        }

        #endregion Get Methods
    }
}