namespace Editor.Controllers
{
    using System.Web.Http;

    using Editor.Models.DataAccess;

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
        public IHttpActionResult GetAllTables()
        {
            var TableNames = DbModel.GetAllTableNames();
            return Ok(false, "Get", TableNames);
        }
        /// <summary>
        /// Get all fields for corresponding table
        /// </summary>
        /// <param name="tableName">
        /// The table Name.
        /// </param>
        /// <returns>
        /// </returns>
        public IHttpActionResult GetTableWithColumn(string tableName)
        {
            var Tablecolumns = DbModel.GetTableColumns(tableName);
            return Ok(false, "Get", Tablecolumns);
        }

        /// <summary>
        /// The get all server names.
        /// </summary>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        public IHttpActionResult GetAllServerNames()
        {
            var ServerList = DbModel.GetAllServerNames();
            return Ok(false, "Get", ServerList);
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
        public IHttpActionResult GetAllDatabases(string ServerName)
        {
            var DatabaseList = DbModel.GetAllDatabases(ServerName);
            return Ok(false, "Get", DatabaseList);

        }

       
    }
}
