using Editor.Code.Config;
using Editor.Code.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Editor.Models.DataAccess
{
    public class DbModel
    {
        public static DataSet GetAllTableNames()
        {
            try
            {
                DataSet db = Sql.ExecuteDataset(DbConnection.DefaultString, CommandType.StoredProcedure
                             , "Auto_GetTables");
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
                DataSet db = Sql.ExecuteDataset(DbConnection.DefaultString, CommandType.StoredProcedure
                             , "Auto_GetTableColumn"
                             , new SqlParameter("@tableName", tableName));
                return db;

            }
            catch (Exception ex)
            {
                //  Logger.Error(ex, "Error In PageCreation of GetParticularTableName");
                throw ex;
            }
        }

        internal static DataTable  GetAllServerNames()
        {
            string myServer = Environment.MachineName;
            
            DataTable servers = SqlDataSourceEnumerator.Instance.GetDataSources();
            return servers;
        }

        internal static DataTable GetAllDatabases(string ServerName)
        {
            using (var con = new SqlConnection("Data Source=" + ServerName + "; Integrated Security=True;"))
            {
                con.Open();
                DataTable databases = con.GetSchema("Databases");
                return databases;
            } 


        }
    }
}