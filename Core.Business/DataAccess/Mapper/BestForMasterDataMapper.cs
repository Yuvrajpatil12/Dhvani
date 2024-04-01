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
    public class BestForMasterDataMapper
    {
        private static readonly string _module = "Core.Business.DataAccess.Mapper.BestForMasterDataMapper";
        private BestForMaster objBestForMaster = null;

        public BestForMaster GetDetails(SqlDataReader sqlDataReader)
        {
            try
            {
                objBestForMaster = new BestForMaster();
               
			   if (sqlDataReader.HasColumn(BestForMasterDBFields.ID))
   objBestForMaster.ID = (sqlDataReader[BestForMasterDBFields.ID] != DBNull.Value ? Convert.ToInt32(sqlDataReader[BestForMasterDBFields.ID]) : 0); 
if (sqlDataReader.HasColumn(BestForMasterDBFields.BestForName))
   objBestForMaster.BestForName = (sqlDataReader[BestForMasterDBFields.BestForName] != DBNull.Value ? Convert.ToString(sqlDataReader[BestForMasterDBFields.BestForName]) : string.Empty); 
if (sqlDataReader.HasColumn(BestForMasterDBFields.StatusId))
   objBestForMaster.StatusId = (sqlDataReader[BestForMasterDBFields.StatusId] != DBNull.Value ? Convert.ToByte(sqlDataReader[BestForMasterDBFields.StatusId]) : (byte)0); 
if (sqlDataReader.HasColumn(BestForMasterDBFields.CreatedDate))
   objBestForMaster.CreatedDate = (sqlDataReader[BestForMasterDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(sqlDataReader[BestForMasterDBFields.CreatedDate]) : DateTime.Now); 
if (sqlDataReader.HasColumn(BestForMasterDBFields.UpdateDate))
   objBestForMaster.UpdateDate = (sqlDataReader[BestForMasterDBFields.UpdateDate] != DBNull.Value ? Convert.ToDateTime(sqlDataReader[BestForMasterDBFields.UpdateDate]) : DateTime.Now); 

            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(sqlDataReader)", ex.Source, ex.Message, ex);
            }
            return objBestForMaster;
        }
		
		public List<BestForMaster> GetDetailsList(SqlDataReader sqlDataReader)
        {
            List<BestForMaster> list = new List<BestForMaster>();
            try
            {
                while (sqlDataReader.Read())
                {
                    objBestForMaster = GetDetails(sqlDataReader);
                    list.Add(objBestForMaster);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetailsList(sqlDataReader)", ex.Source, ex.Message, ex);
            }
            return list;
        }

        public List<BestForMaster> GetDetails(DataSet dataSet)
        {
            List<BestForMaster> BestForMasters = new List<BestForMaster>();

            try
            {
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drow in dataSet.Tables[0].Rows)
                    {
                        objBestForMaster = new BestForMaster();
                       
						if (drow.Table.Columns.Contains(BestForMasterDBFields.ID)) 
  objBestForMaster.ID = (drow[BestForMasterDBFields.ID] != DBNull.Value ? Convert.ToInt32(drow[BestForMasterDBFields.ID]) : 0); 
if (drow.Table.Columns.Contains(BestForMasterDBFields.BestForName)) 
  objBestForMaster.BestForName = (drow[BestForMasterDBFields.BestForName] != DBNull.Value ? Convert.ToString(drow[BestForMasterDBFields.BestForName]) : string.Empty); 
if (drow.Table.Columns.Contains(BestForMasterDBFields.StatusId)) 
  objBestForMaster.StatusId = (drow[BestForMasterDBFields.StatusId] != DBNull.Value ? Convert.ToByte(drow[BestForMasterDBFields.StatusId]) : (byte)0); 
if (drow.Table.Columns.Contains(BestForMasterDBFields.CreatedDate)) 
  objBestForMaster.CreatedDate = (drow[BestForMasterDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(drow[BestForMasterDBFields.CreatedDate]) : DateTime.Now); 
if (drow.Table.Columns.Contains(BestForMasterDBFields.UpdateDate)) 
  objBestForMaster.UpdateDate = (drow[BestForMasterDBFields.UpdateDate] != DBNull.Value ? Convert.ToDateTime(drow[BestForMasterDBFields.UpdateDate]) : DateTime.Now); 


                        BestForMasters.Add(objBestForMaster);
                    }
                }

            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(dataSet)", ex.Source, ex.Message, ex);
            }

            return BestForMasters;
        }
		
		public BestForMaster GetDetailsobj(DataSet dataSet)
        {
            BestForMaster objBestForMaster = new BestForMaster();

            try
            {
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drow in dataSet.Tables[0].Rows)
                    {
                        objBestForMaster = new BestForMaster();
                      
						if (drow.Table.Columns.Contains(BestForMasterDBFields.ID)) 
  objBestForMaster.ID = (drow[BestForMasterDBFields.ID] != DBNull.Value ? Convert.ToInt32(drow[BestForMasterDBFields.ID]) : 0); 
if (drow.Table.Columns.Contains(BestForMasterDBFields.BestForName)) 
  objBestForMaster.BestForName = (drow[BestForMasterDBFields.BestForName] != DBNull.Value ? Convert.ToString(drow[BestForMasterDBFields.BestForName]) : string.Empty); 
if (drow.Table.Columns.Contains(BestForMasterDBFields.StatusId)) 
  objBestForMaster.StatusId = (drow[BestForMasterDBFields.StatusId] != DBNull.Value ? Convert.ToByte(drow[BestForMasterDBFields.StatusId]) : (byte)0); 
if (drow.Table.Columns.Contains(BestForMasterDBFields.CreatedDate)) 
  objBestForMaster.CreatedDate = (drow[BestForMasterDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(drow[BestForMasterDBFields.CreatedDate]) : DateTime.Now); 
if (drow.Table.Columns.Contains(BestForMasterDBFields.UpdateDate)) 
  objBestForMaster.UpdateDate = (drow[BestForMasterDBFields.UpdateDate] != DBNull.Value ? Convert.ToDateTime(drow[BestForMasterDBFields.UpdateDate]) : DateTime.Now); 

                        
                    }
                }

            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(dataSet)", ex.Source, ex.Message, ex);
            }

            return objBestForMaster;
        }
		
		public BestForMaster GetDetails(DataTable dataTable)
        {
            BestForMaster objBestForMaster = new BestForMaster();

            try
            {
                if (dataTable != null &&  dataTable.Rows.Count > 0)
                {
                    foreach (DataRow drow in dataTable.Rows)
                    {
                        objBestForMaster = new BestForMaster();
                      
						if (drow.Table.Columns.Contains(BestForMasterDBFields.ID)) 
  objBestForMaster.ID = (drow[BestForMasterDBFields.ID] != DBNull.Value ? Convert.ToInt32(drow[BestForMasterDBFields.ID]) : 0); 
if (drow.Table.Columns.Contains(BestForMasterDBFields.BestForName)) 
  objBestForMaster.BestForName = (drow[BestForMasterDBFields.BestForName] != DBNull.Value ? Convert.ToString(drow[BestForMasterDBFields.BestForName]) : string.Empty); 
if (drow.Table.Columns.Contains(BestForMasterDBFields.StatusId)) 
  objBestForMaster.StatusId = (drow[BestForMasterDBFields.StatusId] != DBNull.Value ? Convert.ToByte(drow[BestForMasterDBFields.StatusId]) : (byte)0); 
if (drow.Table.Columns.Contains(BestForMasterDBFields.CreatedDate)) 
  objBestForMaster.CreatedDate = (drow[BestForMasterDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(drow[BestForMasterDBFields.CreatedDate]) : DateTime.Now); 
if (drow.Table.Columns.Contains(BestForMasterDBFields.UpdateDate)) 
  objBestForMaster.UpdateDate = (drow[BestForMasterDBFields.UpdateDate] != DBNull.Value ? Convert.ToDateTime(drow[BestForMasterDBFields.UpdateDate]) : DateTime.Now); 

                        
                    }
                }

            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(DataTable)", ex.Source, ex.Message, ex);
            }

            return objBestForMaster;
        }

    }
}
