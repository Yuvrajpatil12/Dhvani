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
    public class VMTagsDataMapper
    {
        private static readonly string _module = "Core.Business.DataAccess.Mapper.VMTagsDataMapper";
        private VMTags objVMTags = null;

        public VMTags GetDetails(SqlDataReader sqlDataReader)
        {
            try
            {
                objVMTags = new VMTags();
               
			   if (sqlDataReader.HasColumn(VMTagsDBFields.ID))
   objVMTags.ID = (sqlDataReader[VMTagsDBFields.ID] != DBNull.Value ? Convert.ToInt32(sqlDataReader[VMTagsDBFields.ID]) : 0); 
if (sqlDataReader.HasColumn(VMTagsDBFields.VoiceMasterID))
   objVMTags.VoiceMasterID = (sqlDataReader[VMTagsDBFields.VoiceMasterID] != DBNull.Value ? Convert.ToInt32(sqlDataReader[VMTagsDBFields.VoiceMasterID]) : 0); 
if (sqlDataReader.HasColumn(VMTagsDBFields.TagMasterID))
   objVMTags.TagMasterID = (sqlDataReader[VMTagsDBFields.TagMasterID] != DBNull.Value ? Convert.ToInt32(sqlDataReader[VMTagsDBFields.TagMasterID]) : 0); 
if (sqlDataReader.HasColumn(VMTagsDBFields.StatusId))
   objVMTags.StatusId = (sqlDataReader[VMTagsDBFields.StatusId] != DBNull.Value ? Convert.ToByte(sqlDataReader[VMTagsDBFields.StatusId]) : (byte)0); 
if (sqlDataReader.HasColumn(VMTagsDBFields.CreatedDate))
   objVMTags.CreatedDate = (sqlDataReader[VMTagsDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(sqlDataReader[VMTagsDBFields.CreatedDate]) : DateTime.Now); 
if (sqlDataReader.HasColumn(VMTagsDBFields.UpdateDate))
   objVMTags.UpdateDate = (sqlDataReader[VMTagsDBFields.UpdateDate] != DBNull.Value ? Convert.ToDateTime(sqlDataReader[VMTagsDBFields.UpdateDate]) : DateTime.Now); 

            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(sqlDataReader)", ex.Source, ex.Message, ex);
            }
            return objVMTags;
        }
		
		public List<VMTags> GetDetailsList(SqlDataReader sqlDataReader)
        {
            List<VMTags> list = new List<VMTags>();
            try
            {
                while (sqlDataReader.Read())
                {
                    objVMTags = GetDetails(sqlDataReader);
                    list.Add(objVMTags);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetailsList(sqlDataReader)", ex.Source, ex.Message, ex);
            }
            return list;
        }

        public List<VMTags> GetDetails(DataSet dataSet)
        {
            List<VMTags> VMTagss = new List<VMTags>();

            try
            {
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drow in dataSet.Tables[0].Rows)
                    {
                        objVMTags = new VMTags();
                       
						if (drow.Table.Columns.Contains(VMTagsDBFields.ID)) 
  objVMTags.ID = (drow[VMTagsDBFields.ID] != DBNull.Value ? Convert.ToInt32(drow[VMTagsDBFields.ID]) : 0); 
if (drow.Table.Columns.Contains(VMTagsDBFields.VoiceMasterID)) 
  objVMTags.VoiceMasterID = (drow[VMTagsDBFields.VoiceMasterID] != DBNull.Value ? Convert.ToInt32(drow[VMTagsDBFields.VoiceMasterID]) : 0); 
if (drow.Table.Columns.Contains(VMTagsDBFields.TagMasterID)) 
  objVMTags.TagMasterID = (drow[VMTagsDBFields.TagMasterID] != DBNull.Value ? Convert.ToInt32(drow[VMTagsDBFields.TagMasterID]) : 0); 
if (drow.Table.Columns.Contains(VMTagsDBFields.StatusId)) 
  objVMTags.StatusId = (drow[VMTagsDBFields.StatusId] != DBNull.Value ? Convert.ToByte(drow[VMTagsDBFields.StatusId]) : (byte)0); 
if (drow.Table.Columns.Contains(VMTagsDBFields.CreatedDate)) 
  objVMTags.CreatedDate = (drow[VMTagsDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(drow[VMTagsDBFields.CreatedDate]) : DateTime.Now); 
if (drow.Table.Columns.Contains(VMTagsDBFields.UpdateDate)) 
  objVMTags.UpdateDate = (drow[VMTagsDBFields.UpdateDate] != DBNull.Value ? Convert.ToDateTime(drow[VMTagsDBFields.UpdateDate]) : DateTime.Now); 


                        VMTagss.Add(objVMTags);
                    }
                }

            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(dataSet)", ex.Source, ex.Message, ex);
            }

            return VMTagss;
        }
		
		public VMTags GetDetailsobj(DataSet dataSet)
        {
            VMTags objVMTags = new VMTags();

            try
            {
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drow in dataSet.Tables[0].Rows)
                    {
                        objVMTags = new VMTags();
                      
						if (drow.Table.Columns.Contains(VMTagsDBFields.ID)) 
  objVMTags.ID = (drow[VMTagsDBFields.ID] != DBNull.Value ? Convert.ToInt32(drow[VMTagsDBFields.ID]) : 0); 
if (drow.Table.Columns.Contains(VMTagsDBFields.VoiceMasterID)) 
  objVMTags.VoiceMasterID = (drow[VMTagsDBFields.VoiceMasterID] != DBNull.Value ? Convert.ToInt32(drow[VMTagsDBFields.VoiceMasterID]) : 0); 
if (drow.Table.Columns.Contains(VMTagsDBFields.TagMasterID)) 
  objVMTags.TagMasterID = (drow[VMTagsDBFields.TagMasterID] != DBNull.Value ? Convert.ToInt32(drow[VMTagsDBFields.TagMasterID]) : 0); 
if (drow.Table.Columns.Contains(VMTagsDBFields.StatusId)) 
  objVMTags.StatusId = (drow[VMTagsDBFields.StatusId] != DBNull.Value ? Convert.ToByte(drow[VMTagsDBFields.StatusId]) : (byte)0); 
if (drow.Table.Columns.Contains(VMTagsDBFields.CreatedDate)) 
  objVMTags.CreatedDate = (drow[VMTagsDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(drow[VMTagsDBFields.CreatedDate]) : DateTime.Now); 
if (drow.Table.Columns.Contains(VMTagsDBFields.UpdateDate)) 
  objVMTags.UpdateDate = (drow[VMTagsDBFields.UpdateDate] != DBNull.Value ? Convert.ToDateTime(drow[VMTagsDBFields.UpdateDate]) : DateTime.Now); 

                        
                    }
                }

            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(dataSet)", ex.Source, ex.Message, ex);
            }

            return objVMTags;
        }
		
		public VMTags GetDetails(DataTable dataTable)
        {
            VMTags objVMTags = new VMTags();

            try
            {
                if (dataTable != null &&  dataTable.Rows.Count > 0)
                {
                    foreach (DataRow drow in dataTable.Rows)
                    {
                        objVMTags = new VMTags();
                      
						if (drow.Table.Columns.Contains(VMTagsDBFields.ID)) 
  objVMTags.ID = (drow[VMTagsDBFields.ID] != DBNull.Value ? Convert.ToInt32(drow[VMTagsDBFields.ID]) : 0); 
if (drow.Table.Columns.Contains(VMTagsDBFields.VoiceMasterID)) 
  objVMTags.VoiceMasterID = (drow[VMTagsDBFields.VoiceMasterID] != DBNull.Value ? Convert.ToInt32(drow[VMTagsDBFields.VoiceMasterID]) : 0); 
if (drow.Table.Columns.Contains(VMTagsDBFields.TagMasterID)) 
  objVMTags.TagMasterID = (drow[VMTagsDBFields.TagMasterID] != DBNull.Value ? Convert.ToInt32(drow[VMTagsDBFields.TagMasterID]) : 0); 
if (drow.Table.Columns.Contains(VMTagsDBFields.StatusId)) 
  objVMTags.StatusId = (drow[VMTagsDBFields.StatusId] != DBNull.Value ? Convert.ToByte(drow[VMTagsDBFields.StatusId]) : (byte)0); 
if (drow.Table.Columns.Contains(VMTagsDBFields.CreatedDate)) 
  objVMTags.CreatedDate = (drow[VMTagsDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(drow[VMTagsDBFields.CreatedDate]) : DateTime.Now); 
if (drow.Table.Columns.Contains(VMTagsDBFields.UpdateDate)) 
  objVMTags.UpdateDate = (drow[VMTagsDBFields.UpdateDate] != DBNull.Value ? Convert.ToDateTime(drow[VMTagsDBFields.UpdateDate]) : DateTime.Now); 

                        
                    }
                }

            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(DataTable)", ex.Source, ex.Message, ex);
            }

            return objVMTags;
        }

    }
}
