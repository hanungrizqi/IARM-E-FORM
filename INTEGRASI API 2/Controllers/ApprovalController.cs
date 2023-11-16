using INTEGRASI_API_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace INTEGRASI_API_2.Controllers
{
    [RoutePrefix("api/Approval")]
    public class ApprovalController : ApiController
    {
        DBPakta_IntegritasDataContext db = new DBPakta_IntegritasDataContext();

        [HttpGet]
        [Route("detailpm")]
        public IHttpActionResult detailpm(int id)
        {
            try
            {
                db.CommandTimeout = 120;
                var data = db.VW_GRATIFIKASI_REPORT_PMs.Where(a => a.ID == id).FirstOrDefault();

                return Ok(new { Data = data });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("ApprovePM")]
        public IHttpActionResult ApprovePM(TBL_T_GRATIFIKASI param)
        {
            try
            {
                var cek = db.TBL_T_GRATIFIKASIs.FirstOrDefault(a => a.ID == param.ID);

                cek.STATUS = param.STATUS;
                cek.UPDATED_BY = param.UPDATED_BY;
                cek.UPDATED_DATE = DateTime.Now;

                db.SubmitChanges();
                return Ok(new { Remarks = true });
            }
            catch (Exception e)
            {
                return Ok(new { Remarks = false, Message = e });
            }
        }

        [HttpPost]
        [Route("RejectPM")]
        public IHttpActionResult RejectPM(TBL_T_GRATIFIKASI param)
        {
            try
            {
                var cek = db.TBL_T_GRATIFIKASIs.FirstOrDefault(a => a.ID == param.ID);

                cek.STATUS = param.STATUS;
                cek.UPDATED_BY = param.UPDATED_BY;
                cek.UPDATED_DATE = DateTime.Now;

                db.SubmitChanges();
                return Ok(new { Remarks = true });
            }
            catch (Exception e)
            {
                return Ok(new { Remarks = false, Message = e });
            }
        }

        [HttpGet]
        [Route("detaildepthead")]
        public IHttpActionResult detaildepthead(int id)
        {
            try
            {
                db.CommandTimeout = 120;
                var data = db.VW_GRATIFIKASI_REPORT_DEPTHEADs.Where(a => a.ID == id).FirstOrDefault();

                return Ok(new { Data = data });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("ApproveDeptHead")]
        public IHttpActionResult ApproveDeptHead(TBL_T_GRATIFIKASI param)
        {
            try
            {
                var cek = db.TBL_T_GRATIFIKASIs.FirstOrDefault(a => a.ID == param.ID);

                cek.STATUS = param.STATUS;
                cek.UPDATED_BY = param.UPDATED_BY;
                cek.UPDATED_DATE = DateTime.Now;

                db.SubmitChanges();
                return Ok(new { Remarks = true });
            }
            catch (Exception e)
            {
                return Ok(new { Remarks = false, Message = e });
            }
        }

        [HttpPost]
        [Route("RejectDeptHead")]
        public IHttpActionResult RejectDeptHead(TBL_T_GRATIFIKASI param)
        {
            try
            {
                var cek = db.TBL_T_GRATIFIKASIs.FirstOrDefault(a => a.ID == param.ID);

                cek.STATUS = param.STATUS;
                cek.UPDATED_BY = param.UPDATED_BY;
                cek.UPDATED_DATE = DateTime.Now;

                db.SubmitChanges();
                return Ok(new { Remarks = true });
            }
            catch (Exception e)
            {
                return Ok(new { Remarks = false, Message = e });
            }
        }

        [HttpGet]
        [Route("detailbod")]
        public IHttpActionResult detailbod(int id)
        {
            try
            {
                db.CommandTimeout = 120;
                var data = db.VW_GRATIFIKASI_REPORT_BODs.Where(a => a.ID == id).FirstOrDefault();

                return Ok(new { Data = data });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("ApproveBOD")]
        public IHttpActionResult ApproveBOD(TBL_T_GRATIFIKASI param)
        {
            try
            {
                var cek = db.TBL_T_GRATIFIKASIs.FirstOrDefault(a => a.ID == param.ID);

                cek.STATUS = param.STATUS;
                cek.UPDATED_BY = param.UPDATED_BY;
                cek.UPDATED_DATE = DateTime.Now;

                db.SubmitChanges();
                return Ok(new { Remarks = true });
            }
            catch (Exception e)
            {
                return Ok(new { Remarks = false, Message = e });
            }
        }

        [HttpPost]
        [Route("RejectBOD")]
        public IHttpActionResult RejectBOD(TBL_T_GRATIFIKASI param)
        {
            try
            {
                var cek = db.TBL_T_GRATIFIKASIs.FirstOrDefault(a => a.ID == param.ID);

                cek.STATUS = param.STATUS;
                cek.UPDATED_BY = param.UPDATED_BY;
                cek.UPDATED_DATE = DateTime.Now;

                db.SubmitChanges();
                return Ok(new { Remarks = true });
            }
            catch (Exception e)
            {
                return Ok(new { Remarks = false, Message = e });
            }
        }
    }
}
