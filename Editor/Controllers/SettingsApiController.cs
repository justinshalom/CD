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
    public class SettingsApiController : JsonApiBaseController
    {
        public IHttpActionResult SaveDBConnection(DatabaseConnectionModel model)
        { 
            if (ModelState.IsValid)
            {
                SettingsDA.SaveDBConnection(model);
            }
            return Ok(false, "", "");

        }
    }
}
