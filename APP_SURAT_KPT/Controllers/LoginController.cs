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
        public async Task<ActionResult> LoginUser(LoginVM requestVM)
        {
            var tryLogin = await auth.GetUserData(requestVM);
            if (tryLogin.Success)
            {
                Session["Nrp"] = tryLogin.Data.Nrp;
                Session["Role"] = tryLogin.Data.Role;
                Session["IsSectionHead"] = tryLogin.Data.IsSectionHead;
                return RedirectToAction("dashboard", "dashboard");
            }

            TempData["Error"] = tryLogin.Message;
            return RedirectToAction("Login", "Login");
        }
        
        public ActionResult Login()
        {
            //return View();

            if (TempData["Error"] != null)
            {
                ViewBag.Msg = TempData["Error"].ToString();
            }
            return View();
        }
    }
}