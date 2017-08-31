// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbApiController.cs" company="CD">
//  CD
// </copyright>
// <summary>
//   The database API controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

using Web.Models;

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
        /// <param name="databaseConnectionModel">
        /// The database Connection Model.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult GetAllTables(DatabaseConnectionModel databaseConnectionModel)
        {
            var tableNames = DbModel.GetAllTableNames(databaseConnectionModel);
            return this.Json(false, "Get", tableNames);
        }

        /// <summary>
        /// Get all fields for corresponding table
        /// </summary>
        /// <param name="databaseConnectionModel">
        /// The database Connection Model.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult GetTableWithColumn(DatabaseConnectionModel databaseConnectionModel)
        {
            var tableColumns = DbModel.GetTableColumns(databaseConnectionModel);
            return this.Json(false, "Get", tableColumns);
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
        /// <param name="databaseConnectionModel">
        /// The database Connection Model.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult GetAllDatabases(DatabaseConnectionModel databaseConnectionModel)
        {
            var databaseList = DbModel.GetAllDatabases(databaseConnectionModel);
            return this.Json(false, "Get", databaseList);
        }

        /// <summary>
        /// The get all authentication types.
        /// </summary>
        /// <param name="serverType">
        /// The server type.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult GetAllAuthenticationTypes(string serverType)
        {
            if (serverType == "mssql")
            {
                var authenticationsList = new List<Dictionary<string, string>>();
                var authentication = new Dictionary<string, string>
                                         {
                                             ["authentication"] = "Windows Authentication"
                                         };
                authenticationsList.Add(authentication);
                authentication = new Dictionary<string, string>
                                     {
                                         ["authentication"] = "Sql Password"
                                     };
                authenticationsList.Add(authentication);
                authentication = new Dictionary<string, string>
                                     {
                                         ["authentication"] = "Active Directory Integrated"
                                     };
                authenticationsList.Add(authentication);
                authentication = new Dictionary<string, string>
                                     {
                                         ["authentication"] = "Active Directory Password"
                                     };
                authenticationsList.Add(authentication);
                return this.OutPut(authenticationsList);
            }

            return this.OutPut(string.Empty);
        }

        /// <summary>
        /// The get all data list types.
        /// </summary>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult GetAllDataListTypes()
        {
            var datatypesList = new List<Dictionary<string, string>>();
            var datatype = new Dictionary<string, string>
                               {
                                   ["datatype"] = "RowsWithPagination"
                               };
            datatypesList.Add(datatype);
            datatype = new Dictionary<string, string>
                           {
                               ["datatype"] = "FieldValue"
                           };
            datatypesList.Add(datatype);

            datatype = new Dictionary<string, string>
                           {
                               ["datatype"] = "RowsWithScrollLoading"
                           };
            datatypesList.Add(datatype);
            datatype = new Dictionary<string, string>
                           {
                               ["datatype"] = "Sum"
                           };
            datatypesList.Add(datatype);
            datatype = new Dictionary<string, string>
                           {
                               ["datatype"] = "Count"
                           };
            datatypesList.Add(datatype);

            datatype = new Dictionary<string, string>
                           {
                               ["datatype"] = "Avearage"
                           };
            datatypesList.Add(datatype);
            datatype = new Dictionary<string, string>
                           {
                               ["datatype"] = "GroupBy"
                           };
            datatypesList.Add(datatype);
            return this.OutPut(datatypesList);
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