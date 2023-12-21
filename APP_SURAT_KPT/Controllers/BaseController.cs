using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APP_SURAT_KPT.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.ApiUrl = System.Configuration.ConfigurationManager.AppSettings["API_PATH"].ToString();

            if (Session["Nrp"] == null)
            {
                filterContext.Result = RedirectToAction("Login", "Login");
                return;
            }

            ViewBag.UserNrp = Session["Nrp"];
            ViewBag.Role = Session["Role"];
            ViewBag.District = Session["District"];
            ViewBag.Department = Session["Department"];
            ViewBag.Posid = Session["Posid"];
            ViewBag.IsSectionHead = Session["IsSectionHead"];

            base.OnActionExecuting(filterContext);
        }
    }
}