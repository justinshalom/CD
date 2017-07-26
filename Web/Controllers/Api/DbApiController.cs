namespace Web.Controllers.Api
{
    using System.Web.Mvc;

    using Web.Models.DataAccess;

    /// <summary>
    /// The db api controller.
    /// </summary>
    public class DbApiController : BaseApiController
    {
        /// <summary>
        /// The get all tables.
        /// </summary>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        public JsonResult GetAllTables()
        {
            var TableNames = DbModel.GetAllTableNames();
            return this.Ok(false, "Get", TableNames);
        }
        /// <summary>
        /// Get all fields for corresponding table
        /// </summary>
        /// <param name="tableName">
        /// The table Name.
        /// </param>
        /// <returns>
        /// </returns>
        public JsonResult GetTableWithColumn(string tableName)
        {
            var Tablecolumns = DbModel.GetTableColumns(tableName);
            return this.Ok(false, "Get", Tablecolumns);
        }

        /// <summary>
        /// The get all server names.
        /// </summary>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        public JsonResult GetAllServerNames()
        {
            var ServerList = DbModel.GetAllServerNames();
            return this.Ok(false, "Get", ServerList);
        }

        /// <summary>
        /// The get all databases.
        /// </summary>
        /// <param name="ServerName">
        /// The server name.
        /// </param>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        public JsonResult GetAllDatabases(string ServerName)
        {
            var DatabaseList = DbModel.GetAllDatabases(ServerName);
            return this.Ok(false, "Get", DatabaseList);

        }

       
    }
}
