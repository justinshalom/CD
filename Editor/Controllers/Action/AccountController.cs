﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Editor.Controllers.Action
{
    public class AccountController : BaseController
    {
        //
        // GET: /Account/
        public ActionResult Index()
        {
            return View();
        }
    }
}