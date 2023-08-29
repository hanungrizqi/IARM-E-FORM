using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APP_SURAT_KPT.Controllers
{
    public class EbekController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult Ebek()
        {
            return View();
        }

        public ActionResult EbekReport()
        {
            return View();
        }
       
    }
}