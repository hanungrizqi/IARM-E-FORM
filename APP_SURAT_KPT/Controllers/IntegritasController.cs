using APP_SURAT_KPT.Helper;
using APP_SURAT_KPT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APP_SURAT_KPT.Controllers;

namespace APP_SURAT_KPT.Controllers
{
    public class IntegritasController : Controller
    {

        private readonly DbSurat_KPTDataContext dbSurat_KPTDataContext;
        public IntegritasController()
        {
            var KPTDataContext = new DbSurat_KPTDataContext();
            dbSurat_KPTDataContext = KPTDataContext;
        }

        public ActionResult RptFaktaIntegritas()
        {
           // Auth auth = new Auth(this.HttpContext);

            var nrp = @Session["Employee_id"];

            // Memanggil stored procedure menggunakan LINQ
            var result = dbSurat_KPTDataContext.Getdatasurat_Fakta_integritas(nrp.ToString())
                .Select(x => new Getdatasurat_Fakta_integritasResult
                {
                    NO_SURAT=x.NO_SURAT,
                    NRP = x.NRP,
                    NAME = x.NAME,
                    DEPT_DESC = x.DEPT_DESC,
                    Address = x.Address,
                    LETTER_DATE = x.LETTER_DATE,
                    CRE_DATE = x.CRE_DATE,
                    CRE_BY = x.CRE_BY,
                    MOD_DATE = x.MOD_DATE,
                    MOD_BY = x.MOD_BY
                })
                .FirstOrDefault();

            if (result == null)
            {
                // Handle jika data tidak ditemukan
                return HttpNotFound();
            }
            
            ViewBag.NAME = result.NAME;
            ViewBag.NRP = result.NRP;
            ViewBag.DEPT_DESC = result.DEPT_DESC;
            ViewBag.Address = result.Address;
            ViewBag.LETTER_DATE = result.LETTER_DATE;
            ViewBag.CRE_DATE = result.CRE_DATE;
            ViewBag.CRE_BY = result.CRE_BY;
            ViewBag.MOD_DATE = result.MOD_DATE;
            ViewBag.MOD_BY = result.MOD_BY;

            return View(result);
        }

        public ActionResult SubmitForm()
        {


            if (ModelState.IsValid) {

                var nrp = @Session["Employee_id"];
                var data = dbSurat_KPTDataContext.TBL_H_LETTER_FAKTA_INTEGRITAs.Where(d => d.NRP == nrp.ToString()).FirstOrDefault();
                if (data != null) {
                    TempData["ErrorMsg"] = $"Anda sudah Submit di aplikasi Integrasi KPT !!<br />";
                    if (TempData["ErrorMsg"] != null)
                    {

                        string sc = "<script>alert('Anda sudah Submit di aplikasi Integrasi KPT !!'); window.location.href = '/IntegritasH/RptFaktaIntegritasH';</script>";
                        return Content(sc);

                        //ViewBag.Msg = TempData["ErrorMsg"].ToString();
                    }

                   // return RedirectToAction("/IntegritasH/RptFaktaIntegritasH");
                }
                var dta = dbSurat_KPTDataContext.Getdatasurat_Fakta_integritas(nrp.ToString())
                    .Select(x => new Getdatasurat_Fakta_integritasResult
                    {
                        NO_SURAT=x.NO_SURAT,
                        NRP = x.NRP,
                        NAME = x.NAME,
                        POSITION_ID = x.POSITION_ID,
                        DEPT_DESC = x.DEPT_DESC,
                        Address = x.Address,
                        LETTER_DATE = x.LETTER_DATE,
                        CRE_DATE = x.CRE_DATE,
                        CRE_BY = x.CRE_BY,
                        MOD_DATE = x.MOD_DATE,
                        MOD_BY = x.MOD_BY
                    })
                .FirstOrDefault();
                var result = dbSurat_KPTDataContext.insert_integritas(dta.NO_SURAT,dta.NRP, dta.NAME, dta.POSITION_ID, dta.DEPT_DESC, dta.Address, dta.LETTER_DATE, DateTime.Now, dta.CRE_BY, null, null);
                string script = "<script>alert('Data berhasil disimpan'); window.location.href = '/IntegritasH/RptFaktaIntegritasH';</script>";
                return Content(script);

                // return RedirectToAction("RptFaktaIntegritas");
            }
            //return RedirectToAction("/IntegritasH/RptFaktaIntegritasH");
             return View();

        }
    }
}