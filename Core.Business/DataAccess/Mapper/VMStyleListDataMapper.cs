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
    public class VMStyleListDataMapper
    {
        private static readonly string _module = "Core.Business.DataAccess.Mapper.VMStyleListDataMapper";
        private VMStyleList objVMStyleList = null;

        public VMStyleList GetDetails(SqlDataReader sqlDataReader)
        {
            try
            {
                objVMStyleList = new VMStyleList();

                if (sqlDataReader.HasColumn(VMStyleListDBFields.ID))
                    objVMStyleList.ID = (sqlDataReader[VMStyleListDBFields.ID] != DBNull.Value ? Convert.ToInt32(sqlDataReader[VMStyleListDBFields.ID]) : 0);
                if (sqlDataReader.HasColumn(VMStyleListDBFields.VoiceMasterID))
                    objVMStyleList.VoiceMasterID = (sqlDataReader[VMStyleListDBFields.VoiceMasterID] != DBNull.Value ? Convert.ToInt32(sqlDataReader[VMStyleListDBFields.VoiceMasterID]) : 0);
                if (sqlDataReader.HasColumn(VMStyleListDBFields.StyleName))
                    objVMStyleList.StyleName = (sqlDataReader[VMStyleListDBFields.StyleName] != DBNull.Value ? Convert.ToString(sqlDataReader[VMStyleListDBFields.StyleName]) : string.Empty);
                if (sqlDataReader.HasColumn(VMStyleListDBFields.StatusId))
                    objVMStyleList.StatusId = (sqlDataReader[VMStyleListDBFields.StatusId] != DBNull.Value ? Convert.ToByte(sqlDataReader[VMStyleListDBFields.StatusId]) : (byte)0);
                if (sqlDataReader.HasColumn(VMStyleListDBFields.CreatedDate))
                    objVMStyleList.CreatedDate = (sqlDataReader[VMStyleListDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(sqlDataReader[VMStyleListDBFields.CreatedDate]) : DateTime.Now);
                if (sqlDataReader.HasColumn(VMStyleListDBFields.UpdateDate))
                    objVMStyleList.UpdateDate = (sqlDataReader[VMStyleListDBFields.UpdateDate] != DBNull.Value ? Convert.ToDateTime(sqlDataReader[VMStyleListDBFields.UpdateDate]) : DateTime.Now);
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(sqlDataReader)", ex.Source, ex.Message, ex);
            }
            return objVMStyleList;
        }

        public List<VMStyleList> GetDetailsList(SqlDataReader sqlDataReader)
        {
            List<VMStyleList> list = new List<VMStyleList>();
            try
            {
                while (sqlDataReader.Read())
                {
                    objVMStyleList = GetDetails(sqlDataReader);
                    list.Add(objVMStyleList);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetailsList(sqlDataReader)", ex.Source, ex.Message, ex);
            }
            return list;
        }

        public List<VMStyleList> GetDetails(DataSet dataSet)
        {
            List<VMStyleList> VMStyleLists = new List<VMStyleList>();

            try
            {
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drow in dataSet.Tables[0].Rows)
                    {
                        objVMStyleList = new VMStyleList();

                        if (drow.Table.Columns.Contains(VMStyleListDBFields.ID))
                            objVMStyleList.ID = (drow[VMStyleListDBFields.ID] != DBNull.Value ? Convert.ToInt32(drow[VMStyleListDBFields.ID]) : 0);
                        if (drow.Table.Columns.Contains(VMStyleListDBFields.VoiceMasterID))
                            objVMStyleList.VoiceMasterID = (drow[VMStyleListDBFields.VoiceMasterID] != DBNull.Value ? Convert.ToInt32(drow[VMStyleListDBFields.VoiceMasterID]) : 0);
                        if (drow.Table.Columns.Contains(VMStyleListDBFields.StyleName))
                            objVMStyleList.StyleName = (drow[VMStyleListDBFields.StyleName] != DBNull.Value ? Convert.ToString(drow[VMStyleListDBFields.StyleName]) : string.Empty);
                        if (drow.Table.Columns.Contains(VMStyleListDBFields.StatusId))
                            objVMStyleList.StatusId = (drow[VMStyleListDBFields.StatusId] != DBNull.Value ? Convert.ToByte(drow[VMStyleListDBFields.StatusId]) : (byte)0);
                        if (drow.Table.Columns.Contains(VMStyleListDBFields.CreatedDate))
                            objVMStyleList.CreatedDate = (drow[VMStyleListDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(drow[VMStyleListDBFields.CreatedDate]) : DateTime.Now);
                        if (drow.Table.Columns.Contains(VMStyleListDBFields.UpdateDate))
                            objVMStyleList.UpdateDate = (drow[VMStyleListDBFields.UpdateDate] != DBNull.Value ? Convert.ToDateTime(drow[VMStyleListDBFields.UpdateDate]) : DateTime.Now);

                        VMStyleLists.Add(objVMStyleList);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(dataSet)", ex.Source, ex.Message, ex);
            }

            return VMStyleLists;
        }

        public VMStyleList GetDetailsobj(DataSet dataSet)
        {
            VMStyleList objVMStyleList = new VMStyleList();

            try
            {
                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drow in dataSet.Tables[0].Rows)
                    {
                        objVMStyleList = new VMStyleList();

                        if (drow.Table.Columns.Contains(VMStyleListDBFields.ID))
                            objVMStyleList.ID = (drow[VMStyleListDBFields.ID] != DBNull.Value ? Convert.ToInt32(drow[VMStyleListDBFields.ID]) : 0);
                        if (drow.Table.Columns.Contains(VMStyleListDBFields.VoiceMasterID))
                            objVMStyleList.VoiceMasterID = (drow[VMStyleListDBFields.VoiceMasterID] != DBNull.Value ? Convert.ToInt32(drow[VMStyleListDBFields.VoiceMasterID]) : 0);
                        if (drow.Table.Columns.Contains(VMStyleListDBFields.StyleName))
                            objVMStyleList.StyleName = (drow[VMStyleListDBFields.StyleName] != DBNull.Value ? Convert.ToString(drow[VMStyleListDBFields.StyleName]) : string.Empty);
                        if (drow.Table.Columns.Contains(VMStyleListDBFields.StatusId))
                            objVMStyleList.StatusId = (drow[VMStyleListDBFields.StatusId] != DBNull.Value ? Convert.ToByte(drow[VMStyleListDBFields.StatusId]) : (byte)0);
                        if (drow.Table.Columns.Contains(VMStyleListDBFields.CreatedDate))
                            objVMStyleList.CreatedDate = (drow[VMStyleListDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(drow[VMStyleListDBFields.CreatedDate]) : DateTime.Now);
                        if (drow.Table.Columns.Contains(VMStyleListDBFields.UpdateDate))
                            objVMStyleList.UpdateDate = (drow[VMStyleListDBFields.UpdateDate] != DBNull.Value ? Convert.ToDateTime(drow[VMStyleListDBFields.UpdateDate]) : DateTime.Now);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(dataSet)", ex.Source, ex.Message, ex);
            }

            return objVMStyleList;
        }

        public VMStyleList GetDetails(DataTable dataTable)
        {
            VMStyleList objVMStyleList = new VMStyleList();

            try
            {
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow drow in dataTable.Rows)
                    {
                        objVMStyleList = new VMStyleList();

                        if (drow.Table.Columns.Contains(VMStyleListDBFields.ID))
                            objVMStyleList.ID = (drow[VMStyleListDBFields.ID] != DBNull.Value ? Convert.ToInt32(drow[VMStyleListDBFields.ID]) : 0);
                        if (drow.Table.Columns.Contains(VMStyleListDBFields.VoiceMasterID))
                            objVMStyleList.VoiceMasterID = (drow[VMStyleListDBFields.VoiceMasterID] != DBNull.Value ? Convert.ToInt32(drow[VMStyleListDBFields.VoiceMasterID]) : 0);
                        if (drow.Table.Columns.Contains(VMStyleListDBFields.StyleName))
                            objVMStyleList.StyleName = (drow[VMStyleListDBFields.StyleName] != DBNull.Value ? Convert.ToString(drow[VMStyleListDBFields.StyleName]) : string.Empty);
                        if (drow.Table.Columns.Contains(VMStyleListDBFields.StatusId))
                            objVMStyleList.StatusId = (drow[VMStyleListDBFields.StatusId] != DBNull.Value ? Convert.ToByte(drow[VMStyleListDBFields.StatusId]) : (byte)0);
                        if (drow.Table.Columns.Contains(VMStyleListDBFields.CreatedDate))
                            objVMStyleList.CreatedDate = (drow[VMStyleListDBFields.CreatedDate] != DBNull.Value ? Convert.ToDateTime(drow[VMStyleListDBFields.CreatedDate]) : DateTime.Now);
                        if (drow.Table.Columns.Contains(VMStyleListDBFields.UpdateDate))
                            objVMStyleList.UpdateDate = (drow[VMStyleListDBFields.UpdateDate] != DBNull.Value ? Convert.ToDateTime(drow[VMStyleListDBFields.UpdateDate]) : DateTime.Now);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(_module, "GetDetails(DataTable)", ex.Source, ex.Message, ex);
            }

            return objVMStyleList;
        }
    }
}