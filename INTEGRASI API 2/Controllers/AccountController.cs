using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using HAI_API.Service;
using INTEGRASI_API_2.Models;
//using INTEGRASI_API_2.Service;
//using INTEGRASI_API_2.VM;
using INTEGRASI_API_2.ViewModels;
using Jose;


namespace HAI_API.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        DBSuratKPTDataContext db = new DBSuratKPTDataContext();
        Auth clsAuth = new Auth();

        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(LoginVM request)
        {
            bool isAuthenticated = true;

            isAuthenticated = clsAuth.AuthLDAP(request);

            isAuthenticated = true;

            if (isAuthenticated)
            {
                var dataUser = db.VW_LOGIN_USERs.Where(x => x.EMPLOYEE_ID == request.nrp).FirstOrDefault();

                if (dataUser == null)
                {
                    return Ok(new { Status = false, Message = "User belum terdaftar di dalam aplikasi HAI WEB" });
                }

                var payload = new Dictionary<string, object>()                {
                    { "Name", dataUser.NAME },
                    { "Nrp", dataUser.EMPLOYEE_ID },
                    { "Pos_title", dataUser.POS_TITLE }
                };

                var secretKey = new byte[] { 164, 60, 194, 0, 161, 189, 41, 38, 130, 89, 141, 165, 45, 170, 159, 209, 69, 137, 243, 216, 191, 131, 47, 250, 32, 107, 231, 117, 37, 158, 225, 234 };
                string token = Jose.JWT.Encode(payload, secretKey, JwsAlgorithm.HS256);

                return Ok(new { Token = token, Data = payload, Status = true, Message = "Data berhasil diambil!" });
            }
            else
            {
                return Ok(new { Status = false, Message = "Nrp/Password Salah" });
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





    }
}