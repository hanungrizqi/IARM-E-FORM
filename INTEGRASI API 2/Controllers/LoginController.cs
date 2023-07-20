using INTEGRASI_API_2.Cls;
using INTEGRASI_API_2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace INTEGRASI_API_2.Controllers
{
    public class LoginController : ApiController
    {
        ClsAuth clsAuth = new ClsAuth();

        [Route("api/login")]
        [HttpPost]
        public IHttpActionResult Authenticate([FromBody] LoginVM login)
        {
            try
            {
                var isAuthorized = clsAuth.AuthLDAP(login);
                //isAuthorized = true;
                if (isAuthorized)
                {
                    var dataUser = clsAuth.GetUserData(login.Username);
                    if (dataUser.Nrp != null)
                    {
                        return Ok(new { Success = true, Message = "Login Success", Data = dataUser });
                    }
                    else
                    {
                        return Ok(new { Success = false, Message = "User tidak memiliki akses ke aplikasi ini!" });
                    }
                }
                return Ok(new { Success = false, Message = "Invalid NRP/Password!" });
            }
            catch (Exception ex)
            {
                return Ok(new { Success = false, Message = $"Terjadi kesalahan: {ex.Message}" });

            }
        }
    }
}