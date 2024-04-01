using Core.Business.DataAccess.Constants;
using Core.Entity;
using Core.Utility.Common;
using System.Data;
using System.Data.SqlClient;

namespace Core.Business.DataAccess.Mapper
{
    public class Short_UrlDataMapper
    {
        private static readonly string _module = "Core.Business.DataAccess.Mapper.Short_UrlDataMapper";
        private Short_Url objShort_Url = null;

        public Short_Url GetDetails(SqlDataReader sqlDataReader)
        {
            try
            {
                objShort_Url = new Short_Url();

                if (sqlDataReader.HasColumn(Short_UrlDBFields.ID))
                    objShort_Url.ID = (sqlDataReader[Short_UrlDBFields.ID] != DBNull.Value ? Convert.ToInt32(sqlDataReader[Short_UrlDBFields.ID]) : 0);
                if (sqlDataReader.HasColumn(Short_UrlDBFields.KeyValue))
                    objShort_Url.KeyValue = (sqlDataReader[Short_UrlDBFields.KeyValue] != DBNull.Value ? Convert.ToString(sqlDataReader[Short_UrlDBFields.KeyValue]) : string.Empty);
                if (sqlDataReader.HasColumn(Short_UrlDBFields.URLString))
                    objShort_Url.URLString = (sqlDataReader[Short_UrlDBFields.URLString] != DBNull.Value ? Convert.ToString(sqlDataReader[Short_UrlDBFields.URLString]) : string.Empty);
                if (sqlDataReader.HasColumn(Short_UrlDBFields.StatusID))
                    objShort_Url.StatusID = (sqlDataReader[Short_UrlDBFields.StatusID] != DBNull.Value ? Convert.ToByte(sqlDataReader[Short_UrlDBFields.StatusID]) : (byte)0);
                if (sqlDataReader.HasColumn(Short_UrlDBFields.CreatedDate))
                    objShort_Url.CreatedDate = (sqlDataReader[Short_UrlDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(sqlDataReader[Short_UrlDBFields.CreatedDate]) : DateTime.Now);
                if (sqlDataReader.HasColumn(Short_UrlDBFields.UpdatedDate))
                    objShort_Url.UpdatedDate = (sqlDataReader[Short_UrlDBFields.UpdatedDate] != DBNull.Value ? Convert.ToDateTime(sqlDataReader[Short_UrlDBFields.UpdatedDate]) : DateTime.Now);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(sqlDataReader)", ex.Source, ex.Message, ex);
            }
            return objShort_Url;
        }

        public List<Short_Url> GetDetailsList(SqlDataReader sqlDataReader)
        {
            List<Short_Url> list = new List<Short_Url>();
            try
            {
                while (sqlDataReader.Read())
                {
                    objShort_Url = GetDetails(sqlDataReader);
                    list.Add(objShort_Url);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetailsList(sqlDataReader)", ex.Source, ex.Message, ex);
            }
            return list;
        }

        public List<Short_Url> GetDetails(DataSet dataSet)
        {
            List<Short_Url> Short_Urls = new List<Short_Url>();

            try
            {
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drow in dataSet.Tables[0].Rows)
                    {
                        objShort_Url = new Short_Url();

                        if (drow.Table.Columns.Contains(Short_UrlDBFields.ID))
                            objShort_Url.ID = (drow[Short_UrlDBFields.ID] != DBNull.Value ? Convert.ToInt32(drow[Short_UrlDBFields.ID]) : 0);
                        if (drow.Table.Columns.Contains(Short_UrlDBFields.KeyValue))
                            objShort_Url.KeyValue = (drow[Short_UrlDBFields.KeyValue] != DBNull.Value ? Convert.ToString(drow[Short_UrlDBFields.KeyValue]) : string.Empty);
                        if (drow.Table.Columns.Contains(Short_UrlDBFields.URLString))
                            objShort_Url.URLString = (drow[Short_UrlDBFields.URLString] != DBNull.Value ? Convert.ToString(drow[Short_UrlDBFields.URLString]) : string.Empty);
                        if (drow.Table.Columns.Contains(Short_UrlDBFields.StatusID))
                            objShort_Url.StatusID = (drow[Short_UrlDBFields.StatusID] != DBNull.Value ? Convert.ToByte(drow[Short_UrlDBFields.StatusID]) : (byte)0);
                        if (drow.Table.Columns.Contains(Short_UrlDBFields.CreatedDate))
                            objShort_Url.CreatedDate = (drow[Short_UrlDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(drow[Short_UrlDBFields.CreatedDate]) : DateTime.Now);
                        if (drow.Table.Columns.Contains(Short_UrlDBFields.UpdatedDate))
                            objShort_Url.UpdatedDate = (drow[Short_UrlDBFields.UpdatedDate] != DBNull.Value ? Convert.ToDateTime(drow[Short_UrlDBFields.UpdatedDate]) : DateTime.Now);

                        Short_Urls.Add(objShort_Url);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(dataSet)", ex.Source, ex.Message, ex);
            }

            return Short_Urls;
        }

        public Short_Url GetDetailsobj(DataSet dataSet)
        {
            Short_Url objShort_Url = new Short_Url();

            try
            {
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drow in dataSet.Tables[0].Rows)
                    {
                        objShort_Url = new Short_Url();

                        if (drow.Table.Columns.Contains(Short_UrlDBFields.ID))
                            objShort_Url.ID = (drow[Short_UrlDBFields.ID] != DBNull.Value ? Convert.ToInt32(drow[Short_UrlDBFields.ID]) : 0);
                        if (drow.Table.Columns.Contains(Short_UrlDBFields.KeyValue))
                            objShort_Url.KeyValue = (drow[Short_UrlDBFields.KeyValue] != DBNull.Value ? Convert.ToString(drow[Short_UrlDBFields.KeyValue]) : string.Empty);
                        if (drow.Table.Columns.Contains(Short_UrlDBFields.URLString))
                            objShort_Url.URLString = (drow[Short_UrlDBFields.URLString] != DBNull.Value ? Convert.ToString(drow[Short_UrlDBFields.URLString]) : string.Empty);
                        if (drow.Table.Columns.Contains(Short_UrlDBFields.StatusID))
                            objShort_Url.StatusID = (drow[Short_UrlDBFields.StatusID] != DBNull.Value ? Convert.ToByte(drow[Short_UrlDBFields.StatusID]) : (byte)0);
                        if (drow.Table.Columns.Contains(Short_UrlDBFields.CreatedDate))
                            objShort_Url.CreatedDate = (drow[Short_UrlDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(drow[Short_UrlDBFields.CreatedDate]) : DateTime.Now);
                        if (drow.Table.Columns.Contains(Short_UrlDBFields.UpdatedDate))
                            objShort_Url.UpdatedDate = (drow[Short_UrlDBFields.UpdatedDate] != DBNull.Value ? Convert.ToDateTime(drow[Short_UrlDBFields.UpdatedDate]) : DateTime.Now);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(dataSet)", ex.Source, ex.Message, ex);
            }

            return objShort_Url;
        }

        public Short_Url GetDetails(DataTable dataTable)
        {
            Short_Url objShort_Url = new Short_Url();

            try
            {
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow drow in dataTable.Rows)
                    {
                        objShort_Url = new Short_Url();

                        if (drow.Table.Columns.Contains(Short_UrlDBFields.ID))
                            objShort_Url.ID = (drow[Short_UrlDBFields.ID] != DBNull.Value ? Convert.ToInt32(drow[Short_UrlDBFields.ID]) : 0);
                        if (drow.Table.Columns.Contains(Short_UrlDBFields.KeyValue))
                            objShort_Url.KeyValue = (drow[Short_UrlDBFields.KeyValue] != DBNull.Value ? Convert.ToString(drow[Short_UrlDBFields.KeyValue]) : string.Empty);
                        if (drow.Table.Columns.Contains(Short_UrlDBFields.URLString))
                            objShort_Url.URLString = (drow[Short_UrlDBFields.URLString] != DBNull.Value ? Convert.ToString(drow[Short_UrlDBFields.URLString]) : string.Empty);
                        if (drow.Table.Columns.Contains(Short_UrlDBFields.StatusID))
                            objShort_Url.StatusID = (drow[Short_UrlDBFields.StatusID] != DBNull.Value ? Convert.ToByte(drow[Short_UrlDBFields.StatusID]) : (byte)0);
                        if (drow.Table.Columns.Contains(Short_UrlDBFields.CreatedDate))
                            objShort_Url.CreatedDate = (drow[Short_UrlDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(drow[Short_UrlDBFields.CreatedDate]) : DateTime.Now);
                        if (drow.Table.Columns.Contains(Short_UrlDBFields.UpdatedDate))
                            objShort_Url.UpdatedDate = (drow[Short_UrlDBFields.UpdatedDate] != DBNull.Value ? Convert.ToDateTime(drow[Short_UrlDBFields.UpdatedDate]) : DateTime.Now);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(DataTable)", ex.Source, ex.Message, ex);
            }

            return objShort_Url;
        }
    }
}