﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APP_SURAT_KPT.Controllers
{
    public class dashboardController : BaseController
    {
        // GET: dasboard
        public ActionResult dashboard()
        {
            return View();
        }
    }
}