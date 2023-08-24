using INTEGRASI_API_2.Cls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace INTEGRASI_API_2.Controllers
{
    public class MasterDataController : ApiController
    {
        ClsMasterData cls = new ClsMasterData();

        [Route("api/masterdata/district")]
        [HttpGet]
        public IHttpActionResult GetListDistrict()
        {
            try
            {
                var data = cls.GetListDistrict();
                return Ok(new { Data = data, Status = true, Message = "Data berhasil diambil!" });

            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.BadRequest, new { Message = ex.Message, Status = false });
            }
        }

        [Route("api/masterdata/dept")]
        [HttpGet]
        public IHttpActionResult GetListDept()
        {
            try
            {
                var data = cls.GetListDept();
                return Ok(new { Data = data, Status = true, Message = "Data berhasil diambil!" });

            }
            catch (Exception ex)
            {

                return Content(HttpStatusCode.BadRequest, new { Message = ex.Message, Status = false });
            }
        }
    }
}