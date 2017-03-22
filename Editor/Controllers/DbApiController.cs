using Editor.Models;
using Editor.Models.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Editor.Controllers
{
    public class DbApiController : BaseApiController
    {

        public IHttpActionResult GetAllTables()
        {
            var TableNames = DbModel.GetAllTableNames();
            return Ok(false, "Get", TableNames);
        }
        /// <summary>
        /// Get all fields for corresponding table
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetTableWithColumn(string tableName)
        {
            var Tablecolumns = DbModel.GetTableColumns(tableName);
            return Ok(false, "Get", Tablecolumns);
        }

        public IHttpActionResult GetAllServerNames()
        {
            var ServerList = DbModel.GetAllServerNames();
            return Ok(false, "Get", ServerList);
        }
        public IHttpActionResult GetAllDatabases(string ServerName)
        {
            var DatabaseList = DbModel.GetAllDatabases(ServerName);
            return Ok(false, "Get", DatabaseList);

        }

       
    }
}
