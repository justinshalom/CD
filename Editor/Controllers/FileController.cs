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
    public class FileController : Controller
    {

        /// <summary>
        /// Files the manager.
        /// </summary>
        /// <returns>View</returns>
        public ActionResult FileManager()
        {
            return View();
        }
	}
}