using INTEGRASI_API_2.Cls;
using INTEGRASI_API_2.ViewModels.Chart;
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

        [Route("api/ebek/datatable")]
        [HttpGet]
        public IHttpActionResult GetDataTable()
        {
            try
            {
                var response = clsEbek.GetDataTableEbek();
                return Ok(new { Success = true, Message = "Data berhasil diambil", Data = response });
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.BadRequest, new { Message = ex.Message, Status = false });
            }
        }

        [Route("api/ebek/datatable/filter")]
        [HttpPost]
        public IHttpActionResult GetFilteredDataTable(EbekFilterRequestsVM ebekFilterRequestsVM)
        {
            try
            {
                var response = clsEbek.GetFilteredDataTableEbek(ebekFilterRequestsVM);
                return Ok(new { Success = true, Message = "Data berhasil diambil", Data = response });
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.BadRequest, new { Message = ex.Message, Status = false });
            }
        }


        [Route("api/ebek/chart")]
        [HttpPost]
        public IHttpActionResult GetChart(ChartRequestVM chartRequestVM)
        {
            try
            {
                var response = clsEbek.GetChartEbek(chartRequestVM);
                return Ok(new { Success = true, Message = "Data berhasil diambil", Data = response });
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.BadRequest, new { Message = ex.Message, Status = false });
            }
        }

    }
}