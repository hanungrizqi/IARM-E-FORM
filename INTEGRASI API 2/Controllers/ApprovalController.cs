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

        #region PM
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
                exec_notifBOD(cek.ID);
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
                exec_notifBOD(cek.ID);
                return Ok(new { Remarks = true });
            }
            catch (Exception e)
            {
                return Ok(new { Remarks = false, Message = e });
            }
        }
        #endregion

        #region Dept Head
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
                exec_notifBOD(cek.ID);
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
                exec_notifBOD(cek.ID);
                return Ok(new { Remarks = true });
            }
            catch (Exception e)
            {
                return Ok(new { Remarks = false, Message = e });
            }
        }
        #endregion

        #region BOD
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
                var check = db.VW_GRATIFIKASI_REPORT_BODs.Where(a => a.ID == param.ID).FirstOrDefault();
                if (check.SITE != "KPHO")
                {
                    exec_notifPM(cek.ID);
                }
                else
                {
                    exec_notifDEPTHEAD(cek.ID);
                }
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
                var check = db.VW_GRATIFIKASI_REPORT_BODs.Where(a => a.ID == param.ID).FirstOrDefault();
                if (check.SITE != "KPHO")
                {
                    exec_notifPM(cek.ID);
                }
                else
                {
                    exec_notifDEPTHEAD(cek.ID);
                }
                return Ok(new { Remarks = true });
            }
            catch (Exception e)
            {
                return Ok(new { Remarks = false, Message = e });
            }
        }
        #endregion

        #region Exec Notif
        [HttpPost]
        [Route("exec_notifPM")]
        public IHttpActionResult exec_notifPM(int id)
        {
            try
            {
                db.CommandTimeout = 120;
                var data = db.cusp_insertNotifProjectManager(id);

                return Ok(new { Data = data });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("exec_notifDEPTHEAD")]
        public IHttpActionResult exec_notifDEPTHEAD(int id)
        {
            try
            {
                db.CommandTimeout = 120;
                var data = db.cusp_insertNotifDeptHead(id);

                return Ok(new { Data = data });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("exec_notifBOD")]
        public IHttpActionResult exec_notifBOD(int id)
        {
            try
            {
                db.CommandTimeout = 120;
                var data = db.cusp_insertNotifBOD(id);

                return Ok(new { Data = data });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion
    }
}
