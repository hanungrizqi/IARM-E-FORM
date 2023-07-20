using INTEGRASI_API_2.Cls;
using INTEGRASI_API_2.ViewModels.Ebek;
using System;
using System.Net;
using System.Web.Http;

namespace INTEGRASI_API_2.Controllers
{
    public class EbekController : ApiController
    {
        // GET: Ebek
        ClsEbek clsEbek = new ClsEbek();

        [Route("api/ebek/getUserData/{Nrp}")]
        [HttpGet]
        public IHttpActionResult GetUserData([FromUri] string Nrp)
        {
            try
            {
                var userData = clsEbek.GetUserData(Nrp);
                if (userData != null)
                {
                    return Ok(new { Data = userData, Success = true, Message = "Data berhasil diambil!" });
                }
                else
                {
                    return Ok(new { Status = false, Message = "Data User tidak ditemukan" });
                }
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.BadRequest, new { Message = ex.Message, Status = false });
            }
        }

        [Route("api/ebek/submit")]
        [HttpPost]
        public IHttpActionResult SubmitEbek(EbekRequestsVM requestsVM)
        {
            try
            {
                var response = clsEbek.SubmitEbekData(requestsVM);
                return Ok(new { Success = response.Success, Message = response.Message });
            }
            catch (Exception ex)
            {
                return Ok(new { Success = false, Message = $"Tejadi kesalahan: {ex.Message}" });
            }
        }
    }
}