﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Editor.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Settings
        public ActionResult DataBase()
        {
            return View();
        }
    }
}