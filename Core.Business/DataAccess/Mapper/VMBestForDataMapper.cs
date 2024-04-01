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
    public class VMBestForDataMapper
    {
        private static readonly string _module = "Core.Business.DataAccess.Mapper.VMBestForDataMapper";
        private VMBestFor objVMBestFor = null;

        public VMBestFor GetDetails(SqlDataReader sqlDataReader)
        {
            try
            {
                objVMBestFor = new VMBestFor();
               
			   if (sqlDataReader.HasColumn(VMBestForDBFields.ID))
   objVMBestFor.ID = (sqlDataReader[VMBestForDBFields.ID] != DBNull.Value ? Convert.ToInt32(sqlDataReader[VMBestForDBFields.ID]) : 0); 
if (sqlDataReader.HasColumn(VMBestForDBFields.VoiceMasterID))
   objVMBestFor.VoiceMasterID = (sqlDataReader[VMBestForDBFields.VoiceMasterID] != DBNull.Value ? Convert.ToInt32(sqlDataReader[VMBestForDBFields.VoiceMasterID]) : 0); 
if (sqlDataReader.HasColumn(VMBestForDBFields.BestForMasterID))
   objVMBestFor.BestForMasterID = (sqlDataReader[VMBestForDBFields.BestForMasterID] != DBNull.Value ? Convert.ToInt32(sqlDataReader[VMBestForDBFields.BestForMasterID]) : 0); 
if (sqlDataReader.HasColumn(VMBestForDBFields.StatusId))
   objVMBestFor.StatusId = (sqlDataReader[VMBestForDBFields.StatusId] != DBNull.Value ? Convert.ToByte(sqlDataReader[VMBestForDBFields.StatusId]) : (byte)0); 
if (sqlDataReader.HasColumn(VMBestForDBFields.CreatedDate))
   objVMBestFor.CreatedDate = (sqlDataReader[VMBestForDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(sqlDataReader[VMBestForDBFields.CreatedDate]) : DateTime.Now); 
if (sqlDataReader.HasColumn(VMBestForDBFields.UpdateDate))
   objVMBestFor.UpdateDate = (sqlDataReader[VMBestForDBFields.UpdateDate] != DBNull.Value ? Convert.ToDateTime(sqlDataReader[VMBestForDBFields.UpdateDate]) : DateTime.Now); 

            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(sqlDataReader)", ex.Source, ex.Message, ex);
            }
            return objVMBestFor;
        }
		
		public List<VMBestFor> GetDetailsList(SqlDataReader sqlDataReader)
        {
            List<VMBestFor> list = new List<VMBestFor>();
            try
            {
                while (sqlDataReader.Read())
                {
                    objVMBestFor = GetDetails(sqlDataReader);
                    list.Add(objVMBestFor);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetailsList(sqlDataReader)", ex.Source, ex.Message, ex);
            }
            return list;
        }

        public List<VMBestFor> GetDetails(DataSet dataSet)
        {
            List<VMBestFor> VMBestFors = new List<VMBestFor>();

            try
            {
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drow in dataSet.Tables[0].Rows)
                    {
                        objVMBestFor = new VMBestFor();
                       
						if (drow.Table.Columns.Contains(VMBestForDBFields.ID)) 
  objVMBestFor.ID = (drow[VMBestForDBFields.ID] != DBNull.Value ? Convert.ToInt32(drow[VMBestForDBFields.ID]) : 0); 
if (drow.Table.Columns.Contains(VMBestForDBFields.VoiceMasterID)) 
  objVMBestFor.VoiceMasterID = (drow[VMBestForDBFields.VoiceMasterID] != DBNull.Value ? Convert.ToInt32(drow[VMBestForDBFields.VoiceMasterID]) : 0); 
if (drow.Table.Columns.Contains(VMBestForDBFields.BestForMasterID)) 
  objVMBestFor.BestForMasterID = (drow[VMBestForDBFields.BestForMasterID] != DBNull.Value ? Convert.ToInt32(drow[VMBestForDBFields.BestForMasterID]) : 0); 
if (drow.Table.Columns.Contains(VMBestForDBFields.StatusId)) 
  objVMBestFor.StatusId = (drow[VMBestForDBFields.StatusId] != DBNull.Value ? Convert.ToByte(drow[VMBestForDBFields.StatusId]) : (byte)0); 
if (drow.Table.Columns.Contains(VMBestForDBFields.CreatedDate)) 
  objVMBestFor.CreatedDate = (drow[VMBestForDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(drow[VMBestForDBFields.CreatedDate]) : DateTime.Now); 
if (drow.Table.Columns.Contains(VMBestForDBFields.UpdateDate)) 
  objVMBestFor.UpdateDate = (drow[VMBestForDBFields.UpdateDate] != DBNull.Value ? Convert.ToDateTime(drow[VMBestForDBFields.UpdateDate]) : DateTime.Now); 


                        VMBestFors.Add(objVMBestFor);
                    }
                }

            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(dataSet)", ex.Source, ex.Message, ex);
            }

            return VMBestFors;
        }
		
		public VMBestFor GetDetailsobj(DataSet dataSet)
        {
            VMBestFor objVMBestFor = new VMBestFor();

            try
            {
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drow in dataSet.Tables[0].Rows)
                    {
                        objVMBestFor = new VMBestFor();
                      
						if (drow.Table.Columns.Contains(VMBestForDBFields.ID)) 
  objVMBestFor.ID = (drow[VMBestForDBFields.ID] != DBNull.Value ? Convert.ToInt32(drow[VMBestForDBFields.ID]) : 0); 
if (drow.Table.Columns.Contains(VMBestForDBFields.VoiceMasterID)) 
  objVMBestFor.VoiceMasterID = (drow[VMBestForDBFields.VoiceMasterID] != DBNull.Value ? Convert.ToInt32(drow[VMBestForDBFields.VoiceMasterID]) : 0); 
if (drow.Table.Columns.Contains(VMBestForDBFields.BestForMasterID)) 
  objVMBestFor.BestForMasterID = (drow[VMBestForDBFields.BestForMasterID] != DBNull.Value ? Convert.ToInt32(drow[VMBestForDBFields.BestForMasterID]) : 0); 
if (drow.Table.Columns.Contains(VMBestForDBFields.StatusId)) 
  objVMBestFor.StatusId = (drow[VMBestForDBFields.StatusId] != DBNull.Value ? Convert.ToByte(drow[VMBestForDBFields.StatusId]) : (byte)0); 
if (drow.Table.Columns.Contains(VMBestForDBFields.CreatedDate)) 
  objVMBestFor.CreatedDate = (drow[VMBestForDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(drow[VMBestForDBFields.CreatedDate]) : DateTime.Now); 
if (drow.Table.Columns.Contains(VMBestForDBFields.UpdateDate)) 
  objVMBestFor.UpdateDate = (drow[VMBestForDBFields.UpdateDate] != DBNull.Value ? Convert.ToDateTime(drow[VMBestForDBFields.UpdateDate]) : DateTime.Now); 

                        
                    }
                }

            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(dataSet)", ex.Source, ex.Message, ex);
            }

            return objVMBestFor;
        }
		
		public VMBestFor GetDetails(DataTable dataTable)
        {
            VMBestFor objVMBestFor = new VMBestFor();

            try
            {
                if (dataTable != null &&  dataTable.Rows.Count > 0)
                {
                    foreach (DataRow drow in dataTable.Rows)
                    {
                        objVMBestFor = new VMBestFor();
                      
						if (drow.Table.Columns.Contains(VMBestForDBFields.ID)) 
  objVMBestFor.ID = (drow[VMBestForDBFields.ID] != DBNull.Value ? Convert.ToInt32(drow[VMBestForDBFields.ID]) : 0); 
if (drow.Table.Columns.Contains(VMBestForDBFields.VoiceMasterID)) 
  objVMBestFor.VoiceMasterID = (drow[VMBestForDBFields.VoiceMasterID] != DBNull.Value ? Convert.ToInt32(drow[VMBestForDBFields.VoiceMasterID]) : 0); 
if (drow.Table.Columns.Contains(VMBestForDBFields.BestForMasterID)) 
  objVMBestFor.BestForMasterID = (drow[VMBestForDBFields.BestForMasterID] != DBNull.Value ? Convert.ToInt32(drow[VMBestForDBFields.BestForMasterID]) : 0); 
if (drow.Table.Columns.Contains(VMBestForDBFields.StatusId)) 
  objVMBestFor.StatusId = (drow[VMBestForDBFields.StatusId] != DBNull.Value ? Convert.ToByte(drow[VMBestForDBFields.StatusId]) : (byte)0); 
if (drow.Table.Columns.Contains(VMBestForDBFields.CreatedDate)) 
  objVMBestFor.CreatedDate = (drow[VMBestForDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(drow[VMBestForDBFields.CreatedDate]) : DateTime.Now); 
if (drow.Table.Columns.Contains(VMBestForDBFields.UpdateDate)) 
  objVMBestFor.UpdateDate = (drow[VMBestForDBFields.UpdateDate] != DBNull.Value ? Convert.ToDateTime(drow[VMBestForDBFields.UpdateDate]) : DateTime.Now); 

                        
                    }
                }

            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(DataTable)", ex.Source, ex.Message, ex);
            }

            return objVMBestFor;
        }

    }
}
