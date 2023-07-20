using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APP_SURAT_KPT.Controllers;

namespace APP_SURAT_KPT.Controllers
{
    public class IntegritasController : BaseController
    {


        public ActionResult RptFaktaIntegritas()
        {
           
            return View();
        }

        public ActionResult PaktaIntegritasReport()
        {            
             return View();
        }
    }
}