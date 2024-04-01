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
    public class TagMasterDataMapper
    {
        private static readonly string _module = "Core.Business.DataAccess.Mapper.TagMasterDataMapper";
        private TagMaster objTagMaster = null;

        public TagMaster GetDetails(SqlDataReader sqlDataReader)
        {
            try
            {
                objTagMaster = new TagMaster();
               
			   if (sqlDataReader.HasColumn(TagMasterDBFields.ID))
   objTagMaster.ID = (sqlDataReader[TagMasterDBFields.ID] != DBNull.Value ? Convert.ToInt32(sqlDataReader[TagMasterDBFields.ID]) : 0); 
if (sqlDataReader.HasColumn(TagMasterDBFields.TagName))
   objTagMaster.TagName = (sqlDataReader[TagMasterDBFields.TagName] != DBNull.Value ? Convert.ToString(sqlDataReader[TagMasterDBFields.TagName]) : string.Empty); 
if (sqlDataReader.HasColumn(TagMasterDBFields.StatusId))
   objTagMaster.StatusId = (sqlDataReader[TagMasterDBFields.StatusId] != DBNull.Value ? Convert.ToByte(sqlDataReader[TagMasterDBFields.StatusId]) : (byte)0); 
if (sqlDataReader.HasColumn(TagMasterDBFields.CreatedDate))
   objTagMaster.CreatedDate = (sqlDataReader[TagMasterDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(sqlDataReader[TagMasterDBFields.CreatedDate]) : DateTime.Now); 
if (sqlDataReader.HasColumn(TagMasterDBFields.UpdateDate))
   objTagMaster.UpdateDate = (sqlDataReader[TagMasterDBFields.UpdateDate] != DBNull.Value ? Convert.ToDateTime(sqlDataReader[TagMasterDBFields.UpdateDate]) : DateTime.Now); 

            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(sqlDataReader)", ex.Source, ex.Message, ex);
            }
            return objTagMaster;
        }
		
		public List<TagMaster> GetDetailsList(SqlDataReader sqlDataReader)
        {
            List<TagMaster> list = new List<TagMaster>();
            try
            {
                while (sqlDataReader.Read())
                {
                    objTagMaster = GetDetails(sqlDataReader);
                    list.Add(objTagMaster);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetailsList(sqlDataReader)", ex.Source, ex.Message, ex);
            }
            return list;
        }

        public List<TagMaster> GetDetails(DataSet dataSet)
        {
            List<TagMaster> TagMasters = new List<TagMaster>();

            try
            {
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drow in dataSet.Tables[0].Rows)
                    {
                        objTagMaster = new TagMaster();
                       
						if (drow.Table.Columns.Contains(TagMasterDBFields.ID)) 
  objTagMaster.ID = (drow[TagMasterDBFields.ID] != DBNull.Value ? Convert.ToInt32(drow[TagMasterDBFields.ID]) : 0); 
if (drow.Table.Columns.Contains(TagMasterDBFields.TagName)) 
  objTagMaster.TagName = (drow[TagMasterDBFields.TagName] != DBNull.Value ? Convert.ToString(drow[TagMasterDBFields.TagName]) : string.Empty); 
if (drow.Table.Columns.Contains(TagMasterDBFields.StatusId)) 
  objTagMaster.StatusId = (drow[TagMasterDBFields.StatusId] != DBNull.Value ? Convert.ToByte(drow[TagMasterDBFields.StatusId]) : (byte)0); 
if (drow.Table.Columns.Contains(TagMasterDBFields.CreatedDate)) 
  objTagMaster.CreatedDate = (drow[TagMasterDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(drow[TagMasterDBFields.CreatedDate]) : DateTime.Now); 
if (drow.Table.Columns.Contains(TagMasterDBFields.UpdateDate)) 
  objTagMaster.UpdateDate = (drow[TagMasterDBFields.UpdateDate] != DBNull.Value ? Convert.ToDateTime(drow[TagMasterDBFields.UpdateDate]) : DateTime.Now); 


                        TagMasters.Add(objTagMaster);
                    }
                }

            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(dataSet)", ex.Source, ex.Message, ex);
            }

            return TagMasters;
        }
		
		public TagMaster GetDetailsobj(DataSet dataSet)
        {
            TagMaster objTagMaster = new TagMaster();

            try
            {
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drow in dataSet.Tables[0].Rows)
                    {
                        objTagMaster = new TagMaster();
                      
						if (drow.Table.Columns.Contains(TagMasterDBFields.ID)) 
  objTagMaster.ID = (drow[TagMasterDBFields.ID] != DBNull.Value ? Convert.ToInt32(drow[TagMasterDBFields.ID]) : 0); 
if (drow.Table.Columns.Contains(TagMasterDBFields.TagName)) 
  objTagMaster.TagName = (drow[TagMasterDBFields.TagName] != DBNull.Value ? Convert.ToString(drow[TagMasterDBFields.TagName]) : string.Empty); 
if (drow.Table.Columns.Contains(TagMasterDBFields.StatusId)) 
  objTagMaster.StatusId = (drow[TagMasterDBFields.StatusId] != DBNull.Value ? Convert.ToByte(drow[TagMasterDBFields.StatusId]) : (byte)0); 
if (drow.Table.Columns.Contains(TagMasterDBFields.CreatedDate)) 
  objTagMaster.CreatedDate = (drow[TagMasterDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(drow[TagMasterDBFields.CreatedDate]) : DateTime.Now); 
if (drow.Table.Columns.Contains(TagMasterDBFields.UpdateDate)) 
  objTagMaster.UpdateDate = (drow[TagMasterDBFields.UpdateDate] != DBNull.Value ? Convert.ToDateTime(drow[TagMasterDBFields.UpdateDate]) : DateTime.Now); 

                        
                    }
                }

            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(dataSet)", ex.Source, ex.Message, ex);
            }

            return objTagMaster;
        }
		
		public TagMaster GetDetails(DataTable dataTable)
        {
            TagMaster objTagMaster = new TagMaster();

            try
            {
                if (dataTable != null &&  dataTable.Rows.Count > 0)
                {
                    foreach (DataRow drow in dataTable.Rows)
                    {
                        objTagMaster = new TagMaster();
                      
						if (drow.Table.Columns.Contains(TagMasterDBFields.ID)) 
  objTagMaster.ID = (drow[TagMasterDBFields.ID] != DBNull.Value ? Convert.ToInt32(drow[TagMasterDBFields.ID]) : 0); 
if (drow.Table.Columns.Contains(TagMasterDBFields.TagName)) 
  objTagMaster.TagName = (drow[TagMasterDBFields.TagName] != DBNull.Value ? Convert.ToString(drow[TagMasterDBFields.TagName]) : string.Empty); 
if (drow.Table.Columns.Contains(TagMasterDBFields.StatusId)) 
  objTagMaster.StatusId = (drow[TagMasterDBFields.StatusId] != DBNull.Value ? Convert.ToByte(drow[TagMasterDBFields.StatusId]) : (byte)0); 
if (drow.Table.Columns.Contains(TagMasterDBFields.CreatedDate)) 
  objTagMaster.CreatedDate = (drow[TagMasterDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(drow[TagMasterDBFields.CreatedDate]) : DateTime.Now); 
if (drow.Table.Columns.Contains(TagMasterDBFields.UpdateDate)) 
  objTagMaster.UpdateDate = (drow[TagMasterDBFields.UpdateDate] != DBNull.Value ? Convert.ToDateTime(drow[TagMasterDBFields.UpdateDate]) : DateTime.Now); 

                        
                    }
                }

            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(DataTable)", ex.Source, ex.Message, ex);
            }

            return objTagMaster;
        }

    }
}
