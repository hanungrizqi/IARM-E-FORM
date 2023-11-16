using APP_SURAT_KPT.Cls.Auth;
using APP_SURAT_KPT.Models;
using APP_SURAT_KPT.ViewModels.Account;
using System.Linq;
using System.Web.Mvc;

namespace APP_SURAT_KPT.Controllers
{
    public class LoginController : Controller
    {

        ClsAuth auth = new ClsAuth();
        DBPakta_IntegritasDataContext _dbContext = new DBPakta_IntegritasDataContext();

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public JsonResult LoginUser(UserData requestVM)
        {
            Session["Nrp"] = requestVM.Nrp;
            Session["Role"] = requestVM.Role;
            Session["District"] = requestVM.District;
            Session["IsSectionHead"] = requestVM.IsSectionHead;
            return new JsonResult() { Data = new { Success = true, Message = "Berhasil Login" } };
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

        public ActionResult LoginMok(string o, string i)
        {
            var userDataMok = _dbContext.VW_MOK_LOGINs.Where(f => f.username == o && f.token == i).FirstOrDefault();

            if (o == null || i == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                if (userDataMok != null)
                {
                    var userDataApp = _dbContext.VW_ALL_USERs.Where(x => x.EMPLOYEE_ID == o).FirstOrDefault();

                    if (userDataApp != null)
                    {
                        Session["Nrp"] = userDataApp.EMPLOYEE_ID;
                        Session["Role"] = userDataApp.ROLE;
                        Session["IsSectionHead"] = userDataApp.SECTION_HEAD != null ? true : false;
                        return RedirectToAction("dashboard", "dashboard");
                    }
                    else
                    {
                        return RedirectToAction("Login", "Login");
                    }
                }
                else
                {
                    return View((object)"Data tidak ditemukan !");
                }
            }
        }
    }
}