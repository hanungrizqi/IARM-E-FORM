using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using INTEGRASI_API_2.Cls;
using INTEGRASI_API_2.Models;
using INTEGRASI_API_2.ViewModels;
using INTEGRASI_API_2.ViewModels.Chart;
using INTEGRASI_API_2.ViewModels.Ebek;
using INTEGRASI_API_2.ViewModels.PI;

namespace INTEGRASI_API_2.Controllers
{
    public class IntegritasController : ApiController
    {

        ClsPI clsPi = new ClsPI();

        [Route("api/integritas/getdataintegritas/{Nrp}")]
        [HttpGet]
        public IHttpActionResult getdataintegritas([FromUri] string Nrp)
        {
            try
            {
                var isSubmited = clsPi.IsUserHasSubmitPi(Nrp);
                if (!isSubmited)
                {
                    var data = clsPi.GetDataPI(Nrp);
                    return Ok(new { Data = data, Status = true, Message = "Data berhasil diambil!" });
                }
                else
                {
                    return Ok(new { Status = false, Message = "Anda sudah mensubmit Pakta Integritas" });
                }

            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.BadRequest, new { Message = ex.Message, Status = false });
            }
        }

        [Route("api/integritas/submit")]
        [HttpPost]
        public async Task<IHttpActionResult> submitPaktaIntegrasi(SubmitPIDataRequestVM requestVM)
        {
            try
            {
                var response = await clsPi.SubmitPaktaIntegritas(requestVM);
                await generatePdfFile(requestVM.UserNrp);

                return Ok(new { Success = response.Success, Message = response.Message });
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.BadRequest, new { Message = ex.Message, Status = false });
            }
        }

        [Route("api/integritas/datatable")]
        [HttpGet]
        public IHttpActionResult GetDataTable()
        {
            try
            {
                var response = clsPi.GetDataTablePI();
                return Ok(new { Success = true, Message = "Data berhasil diambil", Data = response } );
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.BadRequest, new { Message = ex.Message, Status = false });
            }
        }


        [Route("api/integritas/datatable/filter")]
        [HttpPost]
        public IHttpActionResult GetFilteredDataTable(PIFilterRequestsVM piFilterRequestsVM)
        {
            try
            {
                var response = clsPi.GetFilteredDataTablePI(piFilterRequestsVM);
                return Ok(new { Success = true, Message = "Data berhasil diambil", Data = response });
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.BadRequest, new { Message = ex.Message, Status = false });
            }
        }

        [Route("api/integritas/chart")]
        [HttpPost]
        public IHttpActionResult GetChart(ChartRequestVM chartRequestVM)
        {
            try
            {
                var response = clsPi.GetChartPI(chartRequestVM);
                return Ok(new { Success = true, Message = "Data berhasil diambil", Data = response });
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.BadRequest, new { Message = ex.Message, Status = false });
            }
        }

        [Route("api/integritas/pdffile")]
        [HttpPost]
        public async Task<IHttpActionResult> generatePdfFile(string nrp/*, string name, string dept, string location, DateTime submitdate*/)
        {
            try
            {
                var response = await clsPi.SignDocumentPI(nrp/*, name, dept, location, submitdate*/);
                return Ok(new { Success = true, Message = "Gagal" });
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.BadRequest, new { Message = ex.Message, Status = false });
            }
        }
    }
}
