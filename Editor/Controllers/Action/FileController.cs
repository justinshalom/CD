using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Editor.Controllers
{
    /// <summary>
    /// File Controller
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class FileController : BaseController
    {

        /// <summary>
        /// Files the manager.
        /// </summary>
        /// <returns>View</returns>
        public ActionResult FileManager()
        {
            return View();
        }
        public ContentResult GetJson(string filename)
        {
            var file=Server.MapPath("~/Content/json/" + filename);
            var content=System.IO.File.ReadAllText(filename);
            return Content(content, "application/json");
        }
	}
}