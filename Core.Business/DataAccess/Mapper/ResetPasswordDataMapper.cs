using Core.Business.DataAccess.Constants;
using Core.Entity;
using Core.Utility.Common;
using System.Data;
using System.Data.SqlClient;

namespace Core.Business.DataAccess.Mapper
{
    public class ResetPasswordDataMapper
    {
        private static readonly string _module = "Core.Business.DataAccess.Mapper.ResetPasswordDataMapper";
        private ResetPassword objResetPassword = null;

        public ResetPassword GetDetails(SqlDataReader sqlDataReader)
        {
            try
            {
                objResetPassword = new ResetPassword();

                if (sqlDataReader.HasColumn(ResetPasswordDBFields.ID))
                    objResetPassword.ID = (sqlDataReader[ResetPasswordDBFields.ID] != DBNull.Value ? Convert.ToInt32(sqlDataReader[ResetPasswordDBFields.ID]) : 0);
                if (sqlDataReader.HasColumn(ResetPasswordDBFields.UserID))
                    objResetPassword.UserID = (sqlDataReader[ResetPasswordDBFields.UserID] != DBNull.Value ? Convert.ToInt32(sqlDataReader[ResetPasswordDBFields.UserID]) : 0);
                if (sqlDataReader.HasColumn(ResetPasswordDBFields.PassResetCode))
                    objResetPassword.PassResetCode = (sqlDataReader[ResetPasswordDBFields.PassResetCode] != DBNull.Value ? Convert.ToString(sqlDataReader[ResetPasswordDBFields.PassResetCode]) : string.Empty);
                if (sqlDataReader.HasColumn(ResetPasswordDBFields.StatusId))
                    objResetPassword.StatusId = (sqlDataReader[ResetPasswordDBFields.StatusId] != DBNull.Value ? Convert.ToByte(sqlDataReader[ResetPasswordDBFields.StatusId]) : (byte)0);
                if (sqlDataReader.HasColumn(ResetPasswordDBFields.CreatedDate))
                    objResetPassword.CreatedDate = (sqlDataReader[ResetPasswordDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(sqlDataReader[ResetPasswordDBFields.CreatedDate]) : DateTime.Now);
                if (sqlDataReader.HasColumn(ResetPasswordDBFields.UpdatedDate))
                    objResetPassword.UpdatedDate = (sqlDataReader[ResetPasswordDBFields.UpdatedDate] != DBNull.Value ? Convert.ToDateTime(sqlDataReader[ResetPasswordDBFields.UpdatedDate]) : DateTime.Now);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(sqlDataReader)", ex.Source, ex.Message, ex);
            }
            return objResetPassword;
        }

        public List<ResetPassword> GetDetailsList(SqlDataReader sqlDataReader)
        {
            List<ResetPassword> list = new List<ResetPassword>();
            try
            {
                while (sqlDataReader.Read())
                {
                    objResetPassword = GetDetails(sqlDataReader);
                    list.Add(objResetPassword);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetailsList(sqlDataReader)", ex.Source, ex.Message, ex);
            }
            return list;
        }

        public List<ResetPassword> GetDetails(DataSet dataSet)
        {
            List<ResetPassword> ResetPasswords = new List<ResetPassword>();

            try
            {
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drow in dataSet.Tables[0].Rows)
                    {
                        objResetPassword = new ResetPassword();

                        if (drow.Table.Columns.Contains(ResetPasswordDBFields.ID))
                            objResetPassword.ID = (drow[ResetPasswordDBFields.ID] != DBNull.Value ? Convert.ToInt32(drow[ResetPasswordDBFields.ID]) : 0);
                        if (drow.Table.Columns.Contains(ResetPasswordDBFields.UserID))
                            objResetPassword.UserID = (drow[ResetPasswordDBFields.UserID] != DBNull.Value ? Convert.ToInt32(drow[ResetPasswordDBFields.UserID]) : 0);
                        if (drow.Table.Columns.Contains(ResetPasswordDBFields.PassResetCode))
                            objResetPassword.PassResetCode = (drow[ResetPasswordDBFields.PassResetCode] != DBNull.Value ? Convert.ToString(drow[ResetPasswordDBFields.PassResetCode]) : string.Empty);
                        if (drow.Table.Columns.Contains(ResetPasswordDBFields.StatusId))
                            objResetPassword.StatusId = (drow[ResetPasswordDBFields.StatusId] != DBNull.Value ? Convert.ToByte(drow[ResetPasswordDBFields.StatusId]) : (byte)0);
                        if (drow.Table.Columns.Contains(ResetPasswordDBFields.CreatedDate))
                            objResetPassword.CreatedDate = (drow[ResetPasswordDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(drow[ResetPasswordDBFields.CreatedDate]) : DateTime.Now);
                        if (drow.Table.Columns.Contains(ResetPasswordDBFields.UpdatedDate))
                            objResetPassword.UpdatedDate = (drow[ResetPasswordDBFields.UpdatedDate] != DBNull.Value ? Convert.ToDateTime(drow[ResetPasswordDBFields.UpdatedDate]) : DateTime.Now);

                        ResetPasswords.Add(objResetPassword);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(dataSet)", ex.Source, ex.Message, ex);
            }

            return ResetPasswords;
        }

        public ResetPassword GetDetailsobj(DataSet dataSet)
        {
            ResetPassword objResetPassword = new ResetPassword();

            try
            {
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drow in dataSet.Tables[0].Rows)
                    {
                        objResetPassword = new ResetPassword();

                        if (drow.Table.Columns.Contains(ResetPasswordDBFields.ID))
                            objResetPassword.ID = (drow[ResetPasswordDBFields.ID] != DBNull.Value ? Convert.ToInt32(drow[ResetPasswordDBFields.ID]) : 0);
                        if (drow.Table.Columns.Contains(ResetPasswordDBFields.UserID))
                            objResetPassword.UserID = (drow[ResetPasswordDBFields.UserID] != DBNull.Value ? Convert.ToInt32(drow[ResetPasswordDBFields.UserID]) : 0);
                        if (drow.Table.Columns.Contains(ResetPasswordDBFields.PassResetCode))
                            objResetPassword.PassResetCode = (drow[ResetPasswordDBFields.PassResetCode] != DBNull.Value ? Convert.ToString(drow[ResetPasswordDBFields.PassResetCode]) : string.Empty);
                        if (drow.Table.Columns.Contains(ResetPasswordDBFields.StatusId))
                            objResetPassword.StatusId = (drow[ResetPasswordDBFields.StatusId] != DBNull.Value ? Convert.ToByte(drow[ResetPasswordDBFields.StatusId]) : (byte)0);
                        if (drow.Table.Columns.Contains(ResetPasswordDBFields.CreatedDate))
                            objResetPassword.CreatedDate = (drow[ResetPasswordDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(drow[ResetPasswordDBFields.CreatedDate]) : DateTime.Now);
                        if (drow.Table.Columns.Contains(ResetPasswordDBFields.UpdatedDate))
                            objResetPassword.UpdatedDate = (drow[ResetPasswordDBFields.UpdatedDate] != DBNull.Value ? Convert.ToDateTime(drow[ResetPasswordDBFields.UpdatedDate]) : DateTime.Now);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(dataSet)", ex.Source, ex.Message, ex);
            }

            return objResetPassword;
        }

        public ResetPassword GetDetails(DataTable dataTable)
        {
            ResetPassword objResetPassword = new ResetPassword();

            try
            {
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow drow in dataTable.Rows)
                    {
                        objResetPassword = new ResetPassword();

                        if (drow.Table.Columns.Contains(ResetPasswordDBFields.ID))
                            objResetPassword.ID = (drow[ResetPasswordDBFields.ID] != DBNull.Value ? Convert.ToInt32(drow[ResetPasswordDBFields.ID]) : 0);
                        if (drow.Table.Columns.Contains(ResetPasswordDBFields.UserID))
                            objResetPassword.UserID = (drow[ResetPasswordDBFields.UserID] != DBNull.Value ? Convert.ToInt32(drow[ResetPasswordDBFields.UserID]) : 0);
                        if (drow.Table.Columns.Contains(ResetPasswordDBFields.PassResetCode))
                            objResetPassword.PassResetCode = (drow[ResetPasswordDBFields.PassResetCode] != DBNull.Value ? Convert.ToString(drow[ResetPasswordDBFields.PassResetCode]) : string.Empty);
                        if (drow.Table.Columns.Contains(ResetPasswordDBFields.StatusId))
                            objResetPassword.StatusId = (drow[ResetPasswordDBFields.StatusId] != DBNull.Value ? Convert.ToByte(drow[ResetPasswordDBFields.StatusId]) : (byte)0);
                        if (drow.Table.Columns.Contains(ResetPasswordDBFields.CreatedDate))
                            objResetPassword.CreatedDate = (drow[ResetPasswordDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(drow[ResetPasswordDBFields.CreatedDate]) : DateTime.Now);
                        if (drow.Table.Columns.Contains(ResetPasswordDBFields.UpdatedDate))
                            objResetPassword.UpdatedDate = (drow[ResetPasswordDBFields.UpdatedDate] != DBNull.Value ? Convert.ToDateTime(drow[ResetPasswordDBFields.UpdatedDate]) : DateTime.Now);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(DataTable)", ex.Source, ex.Message, ex);
            }

            return objResetPassword;
        }
    }
}