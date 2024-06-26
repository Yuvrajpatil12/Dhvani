using Core.Business.DataAccess.Constants;
using Core.Business.DataAccess.DataAccessLayer;
using Core.Business.DataAccess.DataAccessLayer.General;
using Core.Business.DataAccess.Mapper;
using Core.Entity;
using Core.Entity.Common;
using Core.Entity.Enums;
using Core.Resources;
using Core.Utility.Common;
using System.Data;
using System.Data.SqlClient;

namespace Core.Business.DataAccess.Wrapper
{
    public class UsersWrapper : UniversalObject
    {
        private readonly string _module = "Core.Business.DataAccess.Wrapper.Users";
        private SqlConnection Connection;

        #region UniversalObject Interface Members

        public bool ObjectChanged { get; set; }

        public Users objWrapperClass = new Users();
        private UsersDataMapper objUsersDataMapper = new UsersDataMapper();



        #region GetRecords methods

        


        public bool GetRecordById(int id)
        {
            SqlDataReader sqlDataReader = null;
            try
            {
                if (this.Connection == null)
                    this.Connection = dbClass.GetSqlConnection();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = UsersStoredProcedures.UsersGetRecordById;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 0;
                sqlCommand.Connection = this.Connection;
                sqlCommand.Parameters.AddWithValue(UsersDBFields.ID, id);
                
                sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    objWrapperClass = objUsersDataMapper.GetDetails(sqlDataReader);
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
                sqlCommand.CommandText = UsersStoredProcedures.GetUsersRecordByValue;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 0;
                sqlCommand.Connection = this.Connection;
                sqlCommand.Parameters.AddWithValue(fieldName, value);
                sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    objWrapperClass = objUsersDataMapper.GetDetails(sqlDataReader);
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
                sqlCommand.CommandText = UsersStoredProcedures.GetUsersRecordByValueArray;
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
                    objWrapperClass = objUsersDataMapper.GetDetails(sqlDataReader);
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

        #region Change Status


        public byte ChangeStatus(Int64 ID, string StatusID)
        {
            byte retVal = 0;
            try
            {
                if (this.Connection == null)
                    this.Connection = dbClass.GetSqlConnection();

                SqlCommand sqlCommand = new SqlCommand(UsersStoredProcedures.Users_UpdateStatus, this.Connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue(UsersDBFields.ID, ID);
                sqlCommand.Parameters.AddWithValue(UsersDBFields.StatusId, StatusID);

                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "ChangeStatus(ID=" + ID + ", StatusID=" + StatusID + ")", ex.Source, ex.Message, ex);
            }
            finally
            {
                dbClass.CloseSqlConnection(ref this.Connection);
            }

            return retVal;
        }


        #endregion

        #region Login Method

        public Users Authenticate(string username, string password, string LoginMode)
        {
            SqlDataReader sqlDataReader = null;
            UsersDataMapper objUsersDataMapper = new UsersDataMapper();
            try
            {
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                {
                    if (this.Connection == null)
                        this.Connection = dbClass.GetSqlConnection();

                    SqlCommand sqlCommand = new SqlCommand(UsersStoredProcedures.Users_Login, this.Connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue(UsersDBFields.UserName, username);
                    sqlCommand.Parameters.AddWithValue(UsersDBFields.Password, password);

                    sqlDataReader = sqlCommand.ExecuteReader();

                    if (sqlDataReader.Read())
                    {
                        objWrapperClass = objUsersDataMapper.GetDetails(sqlDataReader);
                        return objWrapperClass;
                    }
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "Authenticate(username=" + username + "Password=" + password + ")", ex.Source, ex.Message, ex);
            }
            finally
            {
                dbClass.CloseSqlConnection(ref this.Connection);
            }

            return objWrapperClass;
        }






        #endregion

        #region Reset Password

        public bool UpdatePassword(ref Dictionary<string, Command> commandList, ref int commandCounter)
        {
            try
            {
                Command command = new Command(UsersStoredProcedures.Password_Update, CommandType.StoredProcedure);
                command.AddParameter(UsersDBFields.ID, objWrapperClass.ID, DataAccessLayer.DataAccess.DataType.Number, 0, ParameterDirection.Input);
                command.AddParameter(UsersDBFields.Password, objWrapperClass.Password, DataAccessLayer.DataAccess.DataType.NVarChar, 100, ParameterDirection.Input);
                command.Name = "UpdateUser" + commandCounter.ToString();
                commandCounter++;
                commandList.Add(command.Name, command);

                return true;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "UpdateRecords", ex.Source, ex.Message, ex);
                return false;
            }
            finally
            {
            }
        }

        #endregion

        #region User Validation

        /// <summary>
        /// This method is used to retrieve all user information from the users table based on the username.
        /// You can utilize this method when you need to access user details throughout the entire application.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="ID"></param>
        /// <returns>This stored procedure returns all user information</returns>
        public Users IsUserIdExist(string userName, long ID)
        {
            SqlDataReader sqlDataReader = null;
            try
            {
                if (!string.IsNullOrEmpty(userName))
                {
                    if (this.Connection == null)
                        this.Connection = dbClass.GetSqlConnection();

                    SqlCommand sqlCommand = new SqlCommand(UsersStoredProcedures.User_IsUserExistCheckId, this.Connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue(UsersDBFields.UserName, userName);
                    sqlCommand.Parameters.AddWithValue(UsersDBFields.ID, ID);
                    sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.Read())
                        objWrapperClass = objUsersDataMapper.GetDetails(sqlDataReader);
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "IsUserIdExist(email=" + userName + ")", ex.Source, ex.Message, ex);
            }
            finally
            {
                dbClass.CloseSqlConnection(ref this.Connection);
            }

            return objWrapperClass;
        }


        public Users IsUsernameExistClick(string username)
        {
            SqlDataReader sqlDataReader = null;
            try
            {
                if (!string.IsNullOrEmpty(username))
                {
                    if (this.Connection == null)
                        this.Connection = dbClass.GetSqlConnection();

                    SqlCommand sqlCommand = new SqlCommand(UsersStoredProcedures.IsUsernameExistEditClick, this.Connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = Convert.ToInt32(ConstantsCommon.SQLCommandTimeOut);
                    sqlCommand.Parameters.AddWithValue(UsersDBFields.UserName, username);

                    sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.Read())
                        objWrapperClass = objUsersDataMapper.GetDetails(sqlDataReader);
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "IsUserNameExists(username=" + username + ")", ex.Source, ex.Message, ex);
            }
            finally
            {
                dbClass.CloseSqlConnection(ref this.Connection);
            }

            return objWrapperClass;
        }

        public Users IsUsernameExistEditClick(Int64 id,string userName)
        {
            SqlDataReader sqlDataReader = null;
            try
            {
                if (id != 0)
                {
                    if (this.Connection == null)
                        this.Connection = dbClass.GetSqlConnection();

                    SqlCommand sqlCommand = new SqlCommand(UsersStoredProcedures.User_IsUserExistCheckId, this.Connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = Convert.ToInt32(ConstantsCommon.SQLCommandTimeOut);
                    sqlCommand.Parameters.AddWithValue(UsersDBFields.ID, id);
                    sqlCommand.Parameters.AddWithValue(UsersDBFields.UserName, userName);

                    sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.Read())
                        objWrapperClass = objUsersDataMapper.GetDetails(sqlDataReader);
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "IsUsernameExistEditClick(ID=" + id + ")", ex.Source, ex.Message, ex);
            }
            finally
            {
                dbClass.CloseSqlConnection(ref this.Connection);
            }

            return objWrapperClass;
        }

        #endregion

        #region CRUD methods
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
                    Command command = new Command(UsersStoredProcedures.UsersSAVE, CommandType.StoredProcedure);
                    command.AddParameter(UsersDBFields.IU_Flag, "I", DataAccessLayer.DataAccess.DataType.Char, 0, ParameterDirection.Input);
                    command.AddParameter(UsersDBFields.ID, objWrapperClass.ID, DataAccessLayer.DataAccess.DataType.Number, 0, ParameterDirection.Input);
                    command.AddParameter(UsersDBFields.UserName, objWrapperClass.UserName, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                    command.AddParameter(UsersDBFields.Password, objWrapperClass.Password, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                    command.AddParameter(UsersDBFields.FirstName, objWrapperClass.FirstName, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                    command.AddParameter(UsersDBFields.LastName, objWrapperClass.LastName, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                    command.AddParameter(UsersDBFields.AlternateEmail, objWrapperClass.AlternateEmail, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                    command.AddParameter(UsersDBFields.ProfilePicture, objWrapperClass.ProfilePicture, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                    command.AddParameter(UsersDBFields.UUID, objWrapperClass.UUID, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                    command.AddParameter(UsersDBFields.RoleId, objWrapperClass.RoleId, DataAccessLayer.DataAccess.DataType.Number, 0, ParameterDirection.Input);
                    command.AddParameter(UsersDBFields.ParentId, objWrapperClass.ParentId, DataAccessLayer.DataAccess.DataType.Number, 0, ParameterDirection.Input);
                    command.AddParameter(UsersDBFields.LanguageId, objWrapperClass.LanguageId, DataAccessLayer.DataAccess.DataType.Number, 0, ParameterDirection.Input);
                    command.AddParameter(UsersDBFields.IsEmailVerified, objWrapperClass.IsEmailVerified, DataAccessLayer.DataAccess.DataType.Varchar2, 0, ParameterDirection.Input);
                    command.AddParameter(UsersDBFields.EmailVerficationCode, objWrapperClass.EmailVerficationCode, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                    command.AddParameter("RetID", 0, DataAccessLayer.DataAccess.DataType.Number, 0, ParameterDirection.Output);
                    command.AddParameter(UsersDBFields.APIKey, objWrapperClass.APIKey, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                    


                    command.Name = UsersStoredProcedures.UsersSAVE + commandCounter.ToString();
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
                Command command = new Command(UsersStoredProcedures.UsersSAVE, CommandType.StoredProcedure);

                command.AddParameter(UsersDBFields.IU_Flag, "U", DataAccessLayer.DataAccess.DataType.Char, 0, ParameterDirection.Input);
                command.AddParameter(UsersDBFields.ID, objWrapperClass.ID, DataAccessLayer.DataAccess.DataType.Number, 0, ParameterDirection.Input);
                command.AddParameter(UsersDBFields.UserName, objWrapperClass.UserName, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                command.AddParameter(UsersDBFields.Password, objWrapperClass.Password, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                command.AddParameter(UsersDBFields.FirstName, objWrapperClass.FirstName, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                command.AddParameter(UsersDBFields.LastName, objWrapperClass.LastName, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                command.AddParameter(UsersDBFields.AlternateEmail, objWrapperClass.AlternateEmail, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                command.AddParameter(UsersDBFields.ProfilePicture, objWrapperClass.ProfilePicture, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                command.AddParameter(UsersDBFields.UUID, objWrapperClass.UUID, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                command.AddParameter(UsersDBFields.RoleId, objWrapperClass.RoleId, DataAccessLayer.DataAccess.DataType.Number, 0, ParameterDirection.Input);
                command.AddParameter(UsersDBFields.ParentId, objWrapperClass.ParentId, DataAccessLayer.DataAccess.DataType.Number, 0, ParameterDirection.Input);
                command.AddParameter(UsersDBFields.LanguageId, objWrapperClass.LanguageId, DataAccessLayer.DataAccess.DataType.Number, 0, ParameterDirection.Input);
                command.AddParameter(UsersDBFields.IsEmailVerified, objWrapperClass.IsEmailVerified, DataAccessLayer.DataAccess.DataType.Varchar2, 0, ParameterDirection.Input);
                command.AddParameter(UsersDBFields.EmailVerficationCode, objWrapperClass.EmailVerficationCode, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
                command.AddParameter("RetID", 0, DataAccessLayer.DataAccess.DataType.Number, 0, ParameterDirection.Output);
                command.AddParameter(UsersDBFields.APIKey, objWrapperClass.APIKey, DataAccessLayer.DataAccess.DataType.NVarChar, 0, ParameterDirection.Input);
               

                command.Name = UsersStoredProcedures.UsersSAVE + commandCounter.ToString();
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

        public bool Delete(ref Dictionary<string, Command> commandList, ref int commandCounter)
        {
            try
            {
                Command command = new Command("SP_MASTERS_Delete", CommandType.StoredProcedure);
                command.AddParameter("@TableName", "Users", DataAccessLayer.DataAccess.DataType.Varchar, 0, ParameterDirection.Input);
                command.AddParameter("@PrimaryKeyColumn", "ID", DataAccessLayer.DataAccess.DataType.Number, 0, ParameterDirection.Input);
                command.AddParameter("@IDs", objWrapperClass.ID, DataAccessLayer.DataAccess.DataType.Number, 0, ParameterDirection.Input);
                command.Name = "DeleteUsers" + commandCounter.ToString();
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

        #endregion

        #region Not In Use 
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
        #endregion



        #endregion UniversalObject Interface Members
    }

    public class UsersWrapperColletion : UniversalCollection
    {
        private readonly string _module = "Users";
        private SqlConnection Connection;
        private List<Users> _Items = new List<Users>();

        public List<Users> Items
        { get { return this._Items; } set { this._Items = value; } }

        private DataSet _DtsDataset = null;
        private string _SortingString = "";

        #region UniversalCollection Interface Members Implementation

       

        #region System GetRecords methods

        public bool GetRecords(bool createDataSet, string[,] sortFields)
        {
            if (createDataSet)
                return GetDataSetForQuery(UsersStoredProcedures.GetUsersRecords);
            else
                return GetCollectionForQuery(UsersStoredProcedures.GetUsersRecords);
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
            ObjSqlParameter.ParameterName = UsersStoredProcedures.SortingString;
            ObjSqlParameter.Value = _SortingString;
            sqlParameterCollection.Add(ObjSqlParameter);

            if (createDataSet)
                return GetDataSetForQueryParameter(UsersStoredProcedures.GetUsersRecords, sqlParameterCollection);
            else
                return GetDataSetForQueryParameter(UsersStoredProcedures.GetUsersRecords, sqlParameterCollection);
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

            return GetCollectionForQueryWithParameters(UsersStoredProcedures.GetUsersRecordByValue, Fieldsname, Values);
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
                    UsersDataMapper objDataMapper = new UsersDataMapper();
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
                ObjSqlParameter.ParameterName = UsersStoredProcedures.SortingString;
                ObjSqlParameter.Value = _SortingString;
                sqlParameterCollection.Add(ObjSqlParameter);
            }

            if (createDataSet)
                return GetDataSetForQueryParameter(UsersStoredProcedures.GetUsersRecords, sqlParameterCollection);
            else
                return GetDataSetForQueryParameter(UsersStoredProcedures.GetUsersRecords, sqlParameterCollection);
        }

        #endregion GetRecords methods



        #region Get Methods

        public dynamic getUsersList(Int64 userId)
        {
            SqlDataReader sqlDataReader = null;
            UsersDataMapper objDataMapper = new UsersDataMapper();

            try
            {
                if (this.Connection == null)
                    this.Connection = dbClass.GetSqlConnection();

                SqlCommand sqlCommand = new SqlCommand(UsersStoredProcedures.Users_GetRecordsForUserList, this.Connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue(UsersDBFields.ID, userId);
                sqlDataReader = sqlCommand.ExecuteReader();

                _Items = objDataMapper.GetDetailsList(sqlDataReader);

                return _Items;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "getUsersList()", ex.Source, ex.Message, ex);
            }
            finally
            {
                dbClass.CloseSqlConnection(ref this.Connection);
            }

            return _Items;
        }

        public dynamic getUserInfo(string userId)
        {
            SqlDataReader sqlDataReader = null;
            UsersDataMapper objDataMapper = new UsersDataMapper();

            try
            {
                if (this.Connection == null)
                    this.Connection = dbClass.GetSqlConnection();

                SqlCommand sqlCommand = new SqlCommand(UsersStoredProcedures.GetUsersRecords, this.Connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue(UsersDBFields.UUID, userId);
                sqlDataReader = sqlCommand.ExecuteReader();

                _Items = objDataMapper.GetDetailsList(sqlDataReader);

                return _Items;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "getUserInfo()", ex.Source, ex.Message, ex);
            }
            finally
            {
                dbClass.CloseSqlConnection(ref this.Connection);
            }

            return _Items;
        }

        /// <summary>
        /// Return the user data base on api key 
        /// </summary>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        /// Main branch code
        public Users GetUserIdFromAPIKey(string apiKey)
        {
            SqlDataReader sqlDataReader = null;
            UsersDataMapper objDataMapper = new UsersDataMapper();

            try
            {
                if (this.Connection == null)
                    this.Connection = dbClass.GetSqlConnection();

                SqlCommand sqlCommand = new SqlCommand(UsersStoredProcedures.UsersGetRecordsByKey, this.Connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue(UsersDBFields.APIKey, apiKey);
                sqlDataReader = sqlCommand.ExecuteReader();
                Users objUsers = new Users();
                objUsers = objDataMapper.GetUserDetails(sqlDataReader);

                return objUsers;
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetUserIdFromAPIKey()", ex.Source, ex.Message, ex);
                
            }
            finally
            {
                dbClass.CloseSqlConnection(ref this.Connection);
            }

            return new Users();
        }

        

        #endregion

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

                GetCollectionForQueryWithParameter(UsersStoredProcedures.UsersSearch, sqlParameterCollection);
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
                ObjSqlParameter.ParameterName = UsersStoredProcedures.SortingString;
                ObjSqlParameter.Value = _SortingString;
                sqlParameterCollection.Add(ObjSqlParameter);

                if (createDataSet)
                    return GetDataSetForQueryParameter(UsersStoredProcedures.UsersSearchByValue, sqlParameterCollection);
                else
                    return GetDataSetForQueryParameter(UsersStoredProcedures.UsersSearchByValue, sqlParameterCollection);
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
                    ObjSqlParameter.ParameterName = UsersStoredProcedures.SortingString;
                    ObjSqlParameter.Value = _SortingString;
                    sqlParameterCollection.Add(ObjSqlParameter);
                }

                if (createDataSet)
                    return GetDataSetForQueryParameter(UsersStoredProcedures.UsersSearchByValueArray, sqlParameterCollection);
                else
                    return GetDataSetForQueryParameter(UsersStoredProcedures.UsersSearchByValueArray, sqlParameterCollection);
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
                DataSet _DtsUsers = new DataSet("Users");
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
                DataSet _DtsUsers = new DataSet("Users");
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
                    UsersDataMapper objUsersDataMapper = new UsersDataMapper();
                    this.Items.Add(objUsersDataMapper.GetDetails(_Dtr));
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
                    UsersDataMapper objUsersDataMapper = new UsersDataMapper();
                    this.Items.Add(objUsersDataMapper.GetDetails(_Dtr));
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
                UsersWrapper objUsersWrapper = new UsersWrapper();
                for (int i = 0; i < this.Items.Count; i++)
                {
                    if (this.Items[i].ObjectChanged == true)
                    {
                        Dictionary<string, Command> subCommands = new Dictionary<string, Command>();
                        objUsersWrapper.Save(ref subCommands, ref commandCounter);
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
                command.AddParameter("@TableName", UsersDBFields.TableNameVal, DataAccessLayer.DataAccess.DataType.Varchar, 0, ParameterDirection.Input);
                command.AddParameter("@PrimaryKeyColumn", UsersDBFields.ID, DataAccessLayer.DataAccess.DataType.Varchar, 0, ParameterDirection.Input);
                command.AddParameter("@IDs", ids, DataAccessLayer.DataAccess.DataType.Varchar, 0, ParameterDirection.Input);
                command.Name = "Delete" + UsersDBFields.TableNameVal + commandCounter.ToString();
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
    }
}