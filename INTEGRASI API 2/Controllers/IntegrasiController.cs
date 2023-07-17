using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using INTEGRASI_API_2.Models;
using INTEGRASI_API_2.ViewModels;
namespace INTEGRASI_API_2.Controllers
{
    [RoutePrefix("api/integritas")]
    public class IntegrasiController : ApiController
    {

        DBSuratKPTDataContext db= new DBSuratKPTDataContext();
        ClsSIntegrasi clsS= new ClsSIntegrasi();
        // GET: Integrasi
        [HttpGet]
        [Route("getdataintegritas")]
        public IHttpActionResult getdataintegritas(string currentPage=null,string limit=null,string nrp=null)
        {
            try
            {
                //validate token 
                // validate user token
                var re = Request;
                var headers = re.Headers;
                string token = "";

                if (headers.Contains("Authorization"))
                {
                    token = headers.GetValues("Authorization").First();
                }
                var user = db.VW_USER_LOGINs.Where(item => item.token == token).FirstOrDefault();

                int DEFAULT_LIMIT = 50;
                int DEFAULT_CURRENT_PAGE = 1;

                int currentPageRes = DEFAULT_CURRENT_PAGE;
                int limitRes = DEFAULT_LIMIT;

                var data1 = db.Getdatasurat_Fakta_integritas(nrp).ToList();

                return Ok(new { Data = data1, Status = true, Message = "Data berhasil diambil!" });
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.BadRequest, new { Message = ex.Message, Status = false });
            }
        }


        [HttpGet]
        [Route("getdataHystoryintegritas")]
        public IHttpActionResult getdataHystoryintegritas(string currentPage = null, string limit = null, string nrp = null)
        {
            try
            {
                //validate token 
                // validate user token
                var re = Request;
                var headers = re.Headers;
                string token = "";

                if (headers.Contains("Authorization"))
                {
                    token = headers.GetValues("Authorization").First();
                }
                var user = db.VW_USER_LOGINs.Where(item => item.token == token).FirstOrDefault();
                
                int DEFAULT_LIMIT = 50;
                int DEFAULT_CURRENT_PAGE = 1;

                int currentPageRes = DEFAULT_CURRENT_PAGE;
                int limitRes = DEFAULT_LIMIT;

                var data1 = db.TBL_H_LETTER_FAKTA_INTEGRITAs.Where(a => a.NRP==user.NRP).ToList();

                return Ok(new { Data = data1, Status = true, Message = "Data berhasil diambil!" });
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.BadRequest, new { Message = ex.Message, Status = false });
            }
        }

        [HttpGet]
        [Route("getdataDetailintegritas")]
        public IHttpActionResult getdataDetailintegritas(string currentPage = null, string limit = null, string no_surat = null)
        {
            try
            {
                //validate token 
                // validate user token
                var re = Request;
                var headers = re.Headers;
                string token = "";

                if (headers.Contains("Authorization"))
                {
                    token = headers.GetValues("Authorization").First();
                }
                var user = db.VW_USER_LOGINs.Where(item => item.token == token).FirstOrDefault();

                int DEFAULT_LIMIT = 50;
                int DEFAULT_CURRENT_PAGE = 1;

                int currentPageRes = DEFAULT_CURRENT_PAGE;
                int limitRes = DEFAULT_LIMIT;

                var data1 = db.TBL_H_LETTER_FAKTA_INTEGRITAs.Where(a => a.NO_SURAT == no_surat).ToList();
                foreach (var data in data1)
                {
                    data.ADDRESS = data.ADDRESS.Trim();
                }

                

                return Ok(new { Data = data1, Status = true, Message = "Data berhasil diambil!" });
            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.BadRequest, new { Message = ex.Message, Status = false });
            }
        }

    }

}
