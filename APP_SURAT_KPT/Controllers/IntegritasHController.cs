using APP_SURAT_KPT.Helper;
using APP_SURAT_KPT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APP_SURAT_KPT.Controllers
{
    
    public class IntegritasHController : Controller
    {
        private readonly DbSurat_KPTDataContext dbSurat_KPTDataContext;
        public IntegritasHController()
        {
            var KPTDataContext = new DbSurat_KPTDataContext();
            dbSurat_KPTDataContext = KPTDataContext;
        }


        // GET: IntegritasH
        public ActionResult RptFaktaIntegritasH()
        {
            // Auth auth = new Auth(this.HttpContext);

            var nrp = @Session["Employee_id"];

            // Memanggil stored procedure menggunakan LINQ
            var result = dbSurat_KPTDataContext.Fakta_Integritas_H(nrp.ToString())
                .Select(x => new Fakta_Integritas_HResult
                {
                    NO_SURAT = x.NO_SURAT,
                    NRP = x.NRP,
                    NAME = x.NAME,
                    DEPT_DESC = x.DEPT_DESC,
                    ADDRESS = x.ADDRESS,
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
            ViewBag.ADDRESS = result.ADDRESS;
            ViewBag.LETTER_DATE = result.LETTER_DATE;
            ViewBag.CRE_DATE = result.CRE_DATE;
            ViewBag.CRE_BY = result.CRE_BY;
            ViewBag.MOD_DATE = result.MOD_DATE;
            ViewBag.MOD_BY = result.MOD_BY;

            return View(result);
        }

        //public ActionResult RptFaktaIntegritasH()
        //{
        //    var nrp = @Session["Employee_id"];
        //    var data1 = db.TBL_H_LETTER_FAKTA_INTEGRITAs.Where(a => a.NRP == nrp).FirstOrDefault();
        //        //.Select(x=> new TBL_H_LETTER_FAKTA_INTEGRITA {
        //        //    NO_SURAT = x.NO_SURAT,
        //        //    NRP = x.NRP,
        //        //    NAME = x.NAME,
        //        //    DEPT_DESC = x.DEPT_DESC,
        //        //    ADDRESS = x.ADDRESS,
        //        //    LETTER_DATE = x.LETTER_DATE,
        //        //    CRE_DATE = x.CRE_DATE,
        //        //    CRE_BY = x.CRE_BY,
        //        //    MOD_DATE = x.MOD_DATE,
        //        //    MOD_BY = x.MOD_BY
        //        //}).FirstOrDefault();

        //   // var result = dbSurat_KPTDataContext.TBL_H_LETTER_FAKTA_INTEGRITAs.Where(a => a.NRP == nrp)
        //    //.Select(x => new TBL_H_LETTER_FAKTA_INTEGRITA
        //    //{

        //    //})
        //    //    .FirstOrDefault();

        //    if (data1 == null)
        //    {
        //        // Handle jika data tidak ditemukan
        //        return HttpNotFound();
        //    }

        //    ViewBag.NAME = data1.NAME;
        //    ViewBag.NRP = data1.NRP;
        //    ViewBag.DEPT_DESC = data1.DEPT_DESC;
        //    ViewBag.ADDRESS = data1.ADDRESS;
        //    ViewBag.LETTER_DATE = data1.LETTER_DATE;
        //    ViewBag.CRE_DATE = data1.CRE_DATE;
        //    ViewBag.CRE_BY = data1.CRE_BY;
        //    ViewBag.MOD_DATE = data1.MOD_DATE;
        //    ViewBag.MOD_BY = data1.MOD_BY;

        //    return View(data1);
        //    //return Redirect("/Integritas/RptFaktaIntegritasH");
        //    //return Redirect("/Integritas/RptFaktaIntegritasH");
        //}
        ////return View();
    }
}
