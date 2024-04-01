using Core.Business.DataAccess.Constants;
using Core.Entity;
using Core.Utility.Common;
using System.Data;
using System.Data.SqlClient;

namespace Core.Business.DataAccess.Mapper
{
    public class RolesDataMapper
    {
        private static readonly string _module = "Core.Business.DataAccess.Mapper.RolesDataMapper";
        private Roles objRoles = null;

        public Roles GetDetails(SqlDataReader sqlDataReader)
        {
            try
            {
                objRoles = new Roles();

                if (sqlDataReader.HasColumn(RolesDBFields.ID))
                    objRoles.ID = (sqlDataReader[RolesDBFields.ID] != DBNull.Value ? Convert.ToInt32(sqlDataReader[RolesDBFields.ID]) : 0);
                if (sqlDataReader.HasColumn(RolesDBFields.RoleName))
                    objRoles.RoleName = (sqlDataReader[RolesDBFields.RoleName] != DBNull.Value ? Convert.ToString(sqlDataReader[RolesDBFields.RoleName]) : string.Empty);
                if (sqlDataReader.HasColumn(RolesDBFields.StatusId))
                    objRoles.StatusId = (sqlDataReader[RolesDBFields.StatusId] != DBNull.Value ? Convert.ToByte(sqlDataReader[RolesDBFields.StatusId]) : (byte)0);
                if (sqlDataReader.HasColumn(RolesDBFields.CreatedDate))
                    objRoles.CreatedDate = (sqlDataReader[RolesDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(sqlDataReader[RolesDBFields.CreatedDate]) : DateTime.Now);
                if (sqlDataReader.HasColumn(RolesDBFields.UpdateDate))
                    objRoles.UpdateDate = (sqlDataReader[RolesDBFields.UpdateDate] != DBNull.Value ? Convert.ToDateTime(sqlDataReader[RolesDBFields.UpdateDate]) : DateTime.Now);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(sqlDataReader)", ex.Source, ex.Message, ex);
            }
            return objRoles;
        }

        public List<Roles> GetDetailsList(SqlDataReader sqlDataReader)
        {
            List<Roles> list = new List<Roles>();
            try
            {
                while (sqlDataReader.Read())
                {
                    objRoles = GetDetails(sqlDataReader);
                    list.Add(objRoles);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetailsList(sqlDataReader)", ex.Source, ex.Message, ex);
            }
            return list;
        }

        public List<Roles> GetDetails(DataSet dataSet)
        {
            List<Roles> Roless = new List<Roles>();

            try
            {
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drow in dataSet.Tables[0].Rows)
                    {
                        objRoles = new Roles();

                        if (drow.Table.Columns.Contains(RolesDBFields.ID))
                            objRoles.ID = (drow[RolesDBFields.ID] != DBNull.Value ? Convert.ToInt32(drow[RolesDBFields.ID]) : 0);
                        if (drow.Table.Columns.Contains(RolesDBFields.RoleName))
                            objRoles.RoleName = (drow[RolesDBFields.RoleName] != DBNull.Value ? Convert.ToString(drow[RolesDBFields.RoleName]) : string.Empty);
                        if (drow.Table.Columns.Contains(RolesDBFields.StatusId))
                            objRoles.StatusId = (drow[RolesDBFields.StatusId] != DBNull.Value ? Convert.ToByte(drow[RolesDBFields.StatusId]) : (byte)0);
                        if (drow.Table.Columns.Contains(RolesDBFields.CreatedDate))
                            objRoles.CreatedDate = (drow[RolesDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(drow[RolesDBFields.CreatedDate]) : DateTime.Now);
                        if (drow.Table.Columns.Contains(RolesDBFields.UpdateDate))
                            objRoles.UpdateDate = (drow[RolesDBFields.UpdateDate] != DBNull.Value ? Convert.ToDateTime(drow[RolesDBFields.UpdateDate]) : DateTime.Now);

                        Roless.Add(objRoles);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(dataSet)", ex.Source, ex.Message, ex);
            }

            return Roless;
        }

        public Roles GetDetailsobj(DataSet dataSet)
        {
            Roles objRoles = new Roles();

            try
            {
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drow in dataSet.Tables[0].Rows)
                    {
                        objRoles = new Roles();

                        if (drow.Table.Columns.Contains(RolesDBFields.ID))
                            objRoles.ID = (drow[RolesDBFields.ID] != DBNull.Value ? Convert.ToInt32(drow[RolesDBFields.ID]) : 0);
                        if (drow.Table.Columns.Contains(RolesDBFields.RoleName))
                            objRoles.RoleName = (drow[RolesDBFields.RoleName] != DBNull.Value ? Convert.ToString(drow[RolesDBFields.RoleName]) : string.Empty);
                        if (drow.Table.Columns.Contains(RolesDBFields.StatusId))
                            objRoles.StatusId = (drow[RolesDBFields.StatusId] != DBNull.Value ? Convert.ToByte(drow[RolesDBFields.StatusId]) : (byte)0);
                        if (drow.Table.Columns.Contains(RolesDBFields.CreatedDate))
                            objRoles.CreatedDate = (drow[RolesDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(drow[RolesDBFields.CreatedDate]) : DateTime.Now);
                        if (drow.Table.Columns.Contains(RolesDBFields.UpdateDate))
                            objRoles.UpdateDate = (drow[RolesDBFields.UpdateDate] != DBNull.Value ? Convert.ToDateTime(drow[RolesDBFields.UpdateDate]) : DateTime.Now);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(dataSet)", ex.Source, ex.Message, ex);
            }

            return objRoles;
        }

        public Roles GetDetails(DataTable dataTable)
        {
            Roles objRoles = new Roles();

            try
            {
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow drow in dataTable.Rows)
                    {
                        objRoles = new Roles();

                        if (drow.Table.Columns.Contains(RolesDBFields.ID))
                            objRoles.ID = (drow[RolesDBFields.ID] != DBNull.Value ? Convert.ToInt32(drow[RolesDBFields.ID]) : 0);
                        if (drow.Table.Columns.Contains(RolesDBFields.RoleName))
                            objRoles.RoleName = (drow[RolesDBFields.RoleName] != DBNull.Value ? Convert.ToString(drow[RolesDBFields.RoleName]) : string.Empty);
                        if (drow.Table.Columns.Contains(RolesDBFields.StatusId))
                            objRoles.StatusId = (drow[RolesDBFields.StatusId] != DBNull.Value ? Convert.ToByte(drow[RolesDBFields.StatusId]) : (byte)0);
                        if (drow.Table.Columns.Contains(RolesDBFields.CreatedDate))
                            objRoles.CreatedDate = (drow[RolesDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(drow[RolesDBFields.CreatedDate]) : DateTime.Now);
                        if (drow.Table.Columns.Contains(RolesDBFields.UpdateDate))
                            objRoles.UpdateDate = (drow[RolesDBFields.UpdateDate] != DBNull.Value ? Convert.ToDateTime(drow[RolesDBFields.UpdateDate]) : DateTime.Now);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(DataTable)", ex.Source, ex.Message, ex);
            }

            return objRoles;
        }
    }
}