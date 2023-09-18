using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using FormsAuth;
using APP_SURAT_KPT.ViewModels.Account;
using System.DirectoryServices;
using APP_SURAT_KPT.Controllers;
using System.Threading.Tasks;
using APP_SURAT_KPT.Cls.Auth;

namespace APP_SURAT_KPT.Controllers
{
    public class LoginController : Controller
    {

        ClsAuth auth = new ClsAuth();

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public JsonResult LoginUser(UserData requestVM)
        {
            Session["Nrp"] = requestVM.Nrp;
            Session["Role"] = requestVM.Role;
            Session["IsSectionHead"] = requestVM.IsSectionHead;
            return new JsonResult() { Data = new { Success = true, Message = "Berhasil Login" }};
        }
        
        public ActionResult Login()
        {
            //return View();
            ViewBag.ApiUrl = System.Configuration.ConfigurationManager.AppSettings["API_PATH"].ToString();
            if (TempData["Error"] != null)
            {
                ViewBag.Msg = TempData["Error"].ToString();
            }
            return View();
        }
    }
}