// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbModel.cs" company="JL">
//   
// </copyright>
// <summary>
//   DB Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Web.Models.DataAccess
{
    using System;
    using System.Data;
    using System.Data.Sql;
    using System.Data.SqlClient;

    using Web.Code.Config;
    using Web.Code.Sql;

    /// <summary>
    /// DB Model
    /// </summary>
    public class DbModel
    {
        /// <summary>
        /// The get all table names.
        /// </summary>
        /// <returns>
        /// The <see cref="DataSet"/>.
        /// </returns>
        /// <exception cref="Exception">Throw exception
        /// </exception>
        public static DataSet GetAllTableNames(DatabaseConnectionModel databaseConnectionModel)
        {
            try
            {
                var spname = "Auto_GetTables";
                var query = "SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].["+ spname + "]')";
                var spQuery = "CREATE PROCEDURE Auto_GetTables AS BEGIN SELECT* FROM [dbo].[" + databaseConnectionModel.InitialCatalog + "].sys.Tables; END";

                DataSet dbExistSP = Sql.ExecuteDataset(DbConnection.GenerateString(databaseConnectionModel), CommandType.Text, query);
                if (dbExistSP.Tables[0].Rows.Count == 0)
                {
                    DataSet dbCreateSP = Sql.ExecuteDataset(DbConnection.GenerateString(databaseConnectionModel), CommandType.Text, spQuery);
                }

                DataSet db = Sql.ExecuteDataset(DbConnection.GenerateString(databaseConnectionModel), CommandType.StoredProcedure, "Auto_GetTables");
                return db;
            }
            catch (Exception ex)
            {
                // Logger.Error(ex, "Error In GetAllTableNames of AffliateAdminDA");
                throw ex;
            }
        }

        /// <summary>
        /// The get table columns.
        /// </summary>
        /// <param name="tableName">
        /// The table name.
        /// </param>
        /// <returns>
        /// The <see cref="DataSet"/>.
        /// </returns>
        /// <exception cref="Exception">Throw Exception
        /// </exception>
        public static DataSet GetTableColumns(string tableName)
        {
            try
            {
                DataSet db = Sql.ExecuteDataset(DbConnection.DefaultString, CommandType.StoredProcedure, "Auto_GetTableColumn", new SqlParameter("@tableName", tableName));
                return db;

            }
            catch (Exception ex)
            {
                // Logger.Error(ex, "Error In PageCreation of GetParticularTableName");
                throw ex;
            }
        }

        /// <summary>
        /// The get all server names.
        /// </summary>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        internal static DataTable  GetAllServerNames()
        {
            string myServer = Environment.MachineName;
            
            DataTable servers = SqlDataSourceEnumerator.Instance.GetDataSources();
            return servers;
        }

        /// <summary>
        /// The get all databases.
        /// </summary>
        /// <param name="serverName">
        /// The server name.
        /// </param>
        /// <returns>
        /// The <see cref="DataTable"/>.
        /// </returns>
        internal static DataTable GetAllDatabases(string serverName)
        {
            using (var con = new SqlConnection("Data Source=" + serverName + "; Integrated Security=True;"))
            {
                con.Open();
                DataTable databases = con.GetSchema("Databases");
                return databases;
            } 


        }
    }
}