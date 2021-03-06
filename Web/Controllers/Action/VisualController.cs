﻿namespace Web.Controllers.Action
{
    using System.Web.Mvc;

    public class VisualController : BaseController
    {
        // GET: Design
        public ActionResult Editor()
        {
            return this.View();
        }
        public ActionResult Designer()
        {
            return this.View();
        }

        public ActionResult MenuTemplate(string editormode,bool isjquery, bool isbootstrap)
        {
            ViewBag.editormode = editormode;
            ViewBag.isjquery = isjquery;
            ViewBag.isbootstrap = isbootstrap;
            return this.View();
        }

    }
}