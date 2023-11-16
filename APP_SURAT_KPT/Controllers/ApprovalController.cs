using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APP_SURAT_KPT.Controllers
{
    public class ApprovalController : BaseController
    {
        // GET: Approval
        public ActionResult PM()
        {
            return View();
        }

        public ActionResult Depthead()
        {
            return View();
        }

        public ActionResult BOD()
        {
            return View();
        }

        public ActionResult Detail_PM(int ID)
        {
            ViewBag.ID = ID;
            return View();
        }

        public ActionResult Detail_DeptHead(int ID)
        {
            ViewBag.ID = ID;
            return View();
        }

        public ActionResult Detail_BOD(int ID)
        {
            ViewBag.ID = ID;
            return View();
        }
    }
}