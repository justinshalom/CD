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
        /// The update stored procedure.
        /// </summary>
        /// <param name="storedProcedureModel">
        /// The stored procedure model.
        /// </param>
        /// <returns>
        /// The <see cref="DataSet"/>.
        /// </returns>
        public static DataSet UpdateStoredProcedure(StoredProcedureModel storedProcedureModel)
        {
            try
            {
                var query = "SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + storedProcedureModel.StoredProcedureName + "]')";
                var storedProcedureQuery = "CREATE PROCEDURE " + storedProcedureModel.StoredProcedureName + " AS BEGIN " + storedProcedureModel.StoredProcedureQuery + " END";
                
                DataSet databaseExistSp = Sql.ExecuteDataset(storedProcedureModel.ConnectionString, CommandType.Text, query);
                DataSet databaseCreateSp = new DataSet();
                if (databaseExistSp != null && databaseExistSp.Tables[0].Rows.Count == 0)
                {
                    databaseCreateSp = Sql.ExecuteDataset(storedProcedureModel.ConnectionString, CommandType.Text, storedProcedureQuery);
                }

                
                return databaseCreateSp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// The get all table names.
        /// </summary>
        /// <param name="databaseConnectionModel">
        /// The database Connection Model.
        /// </param>
        /// <returns>
        /// The <see cref="DataSet"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// Throw exception
        /// </exception>
        public static DataSet GetAllTableNames(DatabaseConnectionModel databaseConnectionModel)
        {
            try
            {
                var storedProcedureName = "Auto_GetTables";
                var connectionString = DbConnection.GenerateString(databaseConnectionModel);
                var storedProcedureModel = new StoredProcedureModel
                                               {
                                                   ConnectionString = connectionString,
                                                   StoredProcedureName = storedProcedureName,
                                                   StoredProcedureQuery = "SELECT* FROM [dbo].[" + databaseConnectionModel.InitialCatalog + "].sys.Tables;"
                                               };

                UpdateStoredProcedure(storedProcedureModel);
                var db = Sql.ExecuteDataset(connectionString, CommandType.StoredProcedure, storedProcedureModel.StoredProcedureName);
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