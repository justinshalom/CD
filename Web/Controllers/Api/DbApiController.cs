// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbApiController.cs" company="CD">
//  CD
// </copyright>
// <summary>
//   The database API controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Web.Controllers.Api
{
    using System.Configuration;
    using System.Web.Mvc;
    using System.Xml.Linq;

    using Code.File;

    using Newtonsoft.Json;

    using Web.Code.Config;
    using Web.Models.DataAccess;

    /// <summary>
    /// The database API controller.
    /// </summary>
    public class DbApiController : BaseApiController
    {
        /// <summary>
        /// The get all tables.
        /// </summary>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult GetAllTables()
        {
            var tableNames = DbModel.GetAllTableNames();
            return this.Json(false, "Get", tableNames);
        }

        /// <summary>
        /// Get all fields for corresponding table
        /// </summary>
        /// <param name="tableName">
        /// The table Name.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult GetTableWithColumn(string tableName)
        {
            var tablecolumns = DbModel.GetTableColumns(tableName);
            return this.Json(false, "Get", tablecolumns);
        }

        /// <summary>
        /// The get all server names.
        /// </summary>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult GetAllServerNames()
        {
            var serverList = DbModel.GetAllServerNames();
            return this.Json(false, "Get", serverList);
        }

        /// <summary>
        /// The get all databases.
        /// </summary>
        /// <param name="serverName">
        /// The server name.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult GetAllDatabases(string serverName)
        {
            var databaseList = DbModel.GetAllDatabases(serverName);
            return this.Json(false, "Get", databaseList);
        }

        /// <summary>
        /// The get all databases.
        /// </summary>
        /// <param name="directory">
        /// The directory.
        /// </param>
        /// <returns>
        /// The <see cref="FileResult"/>.
        /// </returns>
        public FileResult GetConfigDetails(string directory = null)
        {
            if (string.IsNullOrEmpty(directory))
            {
                directory = Project.DbDirectory;
            }
            else if (directory == "web")
            {
                directory = Project.WebDirectory;
            }

            var webConfigPath = directory + "web.config";
            return this.File(webConfigPath, "text/xml");
        }
    }
}