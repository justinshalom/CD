using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Editor.Controllers
{
    public class VisualController : BaseController
    {
        // GET: Design
        public ActionResult Editor()
        {
            return View();
        }
        public ActionResult Designer()
        {
            return View();
        }
    }
}