using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace B2Bauto
{
    public class AutoPageDA
    {
        public static DataSet GetAllTableNames()
        {
            try
            {
                DataSet db = SqlHelper.ExecuteDataset(DBConfigSettings.ConnectionString, CommandType.StoredProcedure
                             , "AutoPage_GetTables");
                return db;

            }
            catch (Exception ex)
            {
               // Logger.Error(ex, "Error In GetAllTableNames of AffliateAdminDA");
                throw ex;
            }
        }

        public static DataSet GetTableColumns(string tableName)
        {
            try
            {
                DataSet db = SqlHelper.ExecuteDataset(DBConfigSettings.ConnectionString, CommandType.StoredProcedure
                             , "AutpPage_GetTableColumn"
                             , new SqlParameter("@tableName", tableName));
                return db;

            }
            catch (Exception ex)
            {
              //  Logger.Error(ex, "Error In PageCreation of GetParticularTableName");
                throw ex;
            }
        }

        public static DataSet GetautoRows(string selectData, string tableName, string JoinClause, string whereclause)
        {
            try
            {
                DataSet db = SqlHelper.ExecuteDataset(DBConfigSettings.ConnectionString, CommandType.StoredProcedure
                             , "GetAutoRowsSP"
                             , new SqlParameter("@SelectValues", selectData)
                             , new SqlParameter("@TableName", tableName)
                             , new SqlParameter("@JoinClause", JoinClause)
                             , new SqlParameter("@WhereClause", whereclause));
                return db;

            }
            catch (Exception ex)
            {
               // Logger.Error(ex, "Error In GetautoRows of get the table details");
                throw ex;
            }
        }
        public static string SaveAutoPage(string columns, string param, string tablename,string UniqueClause,string updateData)
        {
            try
            {
                string _query = "";

               
                        try
                        {
                            var obj = SqlHelper.ExecuteScalar(DBConfigSettings.ConnectionString, CommandType.StoredProcedure,
                                "SaveAutoPageSP",
                                new SqlParameter("@columns", columns),
                                new SqlParameter("@values", param.Trim()),
                                 new SqlParameter("@UpdateData", updateData),
                                new SqlParameter("@tablename", tablename),
                                 new SqlParameter("@UniqueClause", UniqueClause));
                            if(obj==null)
                            {
                                obj = SqlHelper.ExecuteScalar(DBConfigSettings.ConnectionString, CommandType.StoredProcedure,
                               "SaveAutoPageSP",
                               new SqlParameter("@columns", columns),
                               new SqlParameter("@values", param.Trim()),
                                new SqlParameter("@UpdateData", updateData),
                               new SqlParameter("@tablename", tablename),
                                new SqlParameter("@UniqueClause", UniqueClause));
                            }
                            _query = obj.ToString();
                         

                        }
                        catch (Exception ex)
                        {
                          
                   //         Logger.Error(ex, "Error In SaveAutoPage of AutoPageDA");
                            return "error";
                        }
                  
                return _query;
            }
            catch (Exception ex)
            {
              //  Logger.Error(ex, "Error In SaveAutoPage ");
                throw ex;
            }
        }

        public static string UpdateAutoPage(string updateData, string tableName, string whereClause, string UniqueClause)
        {
            try
            {
                string _query = "";

                using (SqlConnection cn = new SqlConnection(DBConfigSettings.ConnectionString))
                {
                    cn.Open();
                    using (SqlTransaction trans = cn.BeginTransaction())
                    {
                        try
                        {
                            _query = (SqlHelper.ExecuteScalar(trans, CommandType.StoredProcedure,
                                "UpdateeAutoPageSP",
                                new SqlParameter("@UpdateData", updateData),
                                new SqlParameter("@TableName", tableName),
                                new SqlParameter("@WhereClause", whereClause),
                                 new SqlParameter("@UniqueClause", UniqueClause))).ToString();
                            trans.Commit();

                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                      //      Logger.Error(ex, "Error In SaveAutoPage of AutoPageDA");
                            return "error";
                        }
                    }
                }
                return _query;
            }
            catch (Exception ex)
            {
                //Logger.Error(ex, "Error In SaveAutoPage ");
                throw ex;
            }
        }

        public static string DeleteAutoPage(string tableName, string whereClause)
        {
            try
            {
                string _query = "";

                using (SqlConnection cn = new SqlConnection(DBConfigSettings.ConnectionString))
                {
                    cn.Open();
                    using (SqlTransaction trans = cn.BeginTransaction())
                    {
                        try
                        {
                            _query = (SqlHelper.ExecuteScalar(trans, CommandType.StoredProcedure,
                                "DeleteAutoPageSP",
                                new SqlParameter("@tableName", tableName),
                                new SqlParameter("@WhereClause", whereClause))).ToString();
                            trans.Commit();

                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            //      Logger.Error(ex, "Error In SaveAutoPage of AutoPageDA");
                            return "error";
                        }
                    }
                }
                return _query;
            }
            catch (Exception ex)
            {
                //Logger.Error(ex, "Error In SaveAutoPage ");
                throw ex;
            }
        }

        public static DataSet GetAutcomplete(string selectData, string tableName, string whereClause,long limit)
        {
            try
            {
                DataSet db = SqlHelper.ExecuteDataset(DBConfigSettings.ConnectionString, CommandType.StoredProcedure
                             , "GetAutocompleteSP"
                             , new SqlParameter("@SelectValues", selectData)
                             , new SqlParameter("@TableName", tableName)
                             , new SqlParameter("@WhereClause", whereClause)
                             , new SqlParameter("@Limit", limit));
                return db;

            }
            catch (Exception ex)
            {
                // Logger.Error(ex, "Error In GetautoRows of get the table details");
                throw ex;
            }
        }
        
    }
}
