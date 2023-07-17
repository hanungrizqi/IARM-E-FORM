using APP_SURAT_KPT.Helper;
using APP_SURAT_KPT.Models;
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

namespace APP_SURAT_KPT.Controllers
{
    public class LoginController : Controller
    {

        DbSurat_KPTDataContext db = new DbSurat_KPTDataContext();
        Auth clsAuth = new Auth();


        public ActionResult Process(LoginVM request)
        {
            bool isAuthenticated = true;

            isAuthenticated = clsAuth.AuthLDAP(request);

            isAuthenticated = true;

            if (isAuthenticated)
            {
                var dataUser = db.VW_LOGIN_USERs.Where(x => x.EMPLOYEE_ID == request.nrp).FirstOrDefault();

                if (dataUser == null)
                {
                    TempData["ErrorMsg"] = $"User belum terdaftar  aplikasi Integrasi KPT <br />";
                    return RedirectToAction("Login");
                }
                //var dta=db.TBL_H_LETTER_FAKTA_INTEGRITAs.Where(x => x.NRP==request.nrp).FirstOrDefault();
                //if (dta != null)
                //{
                //    TempData["ErrorMsg"] = $"User sudah Submit di aplikasi Integrasi KPT <br />";
                //    return RedirectToAction("Login");
                //}


                var payload = new Dictionary<string, object>()                {
                    { "Name", dataUser.NAME },
                    //{ "Nrp", dataUser.NRP },
                    { "Dstrct_code", dataUser.dstrct_code },
                    { "Employee_id", dataUser.EMPLOYEE_ID },
                    { "Pos_title", dataUser.POS_TITLE },
                };


                Session["Employee_id"] = dataUser.EMPLOYEE_ID;
                Session["Name"] = dataUser.NAME;
                Session["Position_id"] = dataUser.POSITION_ID;
                return Redirect("/dashboard/dashboard");

            }
            else
            {
                TempData["ErrorMsg"] = $"Nrp/Password Salah <br />Message :";
                return RedirectToAction("Login");
            }
        }

        public bool OpenLdap(string username = "", string password = "")
        {
            bool status = true;
            String uid = "cn=" + username + ",ou=Users,dc=kpp,dc=net";

            DirectoryEntry root = new DirectoryEntry("LDAP://10.12.101.102", uid, password, AuthenticationTypes.None);


            try
            {
                // attempt to use LDAP connection
                object connected = root.NativeObject;
                status = true;
                // no exception, login successful
                //Session["LoginNRP"] = model.username.ToUpper();
                //return RedirectToLocal(returnUrl);

            }
            catch (Exception ex)
            {
                status = false;
            }

            return status;
        }

        //GET: Login
        public ActionResult Login()
        {
            //return View();

            if (TempData["ErrorMsg"] != null)
            {
                ViewBag.Msg = TempData["ErrorMsg"].ToString();
            }
            return View();
        }
    }
}