using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APP_SURAT_KPT.Controllers;

namespace APP_SURAT_KPT.Controllers
{
    public class IntegritasController : BaseController
    {

        //private readonly DbSurat_KPTDataContext dbSurat_KPTDataContext;
        //public IntegritasController()
        //{
        //    var KPTDataContext = new DbSurat_KPTDataContext();
        //    dbSurat_KPTDataContext = KPTDataContext;
        //}

        public ActionResult RptFaktaIntegritas()
        {
           // Auth auth = new Auth(this.HttpContext);

           // var nrp = @Session["Employee_id"];

           // Memanggil stored procedure menggunakan LINQ
           //var result = dbSurat_KPTDataContext.Getdatasurat_Fakta_integritas(nrp.ToString())
           //    .Select(x => new Getdatasurat_Fakta_integritasResult
           //    {
           //        NO_SURAT = x.NO_SURAT,
           //        NRP = x.NRP,
           //        NAME = x.NAME,
           //        DEPT_DESC = x.DEPT_DESC,
           //        Address = x.Address,
           //        LETTER_DATE = x.LETTER_DATE,
           //        CRE_DATE = x.CRE_DATE,
           //        CRE_BY = x.CRE_BY,
           //        MOD_DATE = x.MOD_DATE,
           //        MOD_BY = x.MOD_BY
           //    })
           //    .FirstOrDefault();

           // if (result == null)
           // {
           //     Handle jika data tidak ditemukan
           //     return HttpNotFound();
           // }

           // ViewBag.NAME = result.NAME;
           // ViewBag.NRP = result.NRP;
           // ViewBag.DEPT_DESC = result.DEPT_DESC;
           // ViewBag.Address = result.Address;
           // ViewBag.LETTER_DATE = result.LETTER_DATE;
           // ViewBag.CRE_DATE = result.CRE_DATE;
           // ViewBag.CRE_BY = result.CRE_BY;
           // ViewBag.MOD_DATE = result.MOD_DATE;
           // ViewBag.MOD_BY = result.MOD_BY;

            return View();
        }

        public ActionResult SubmitForm()
        {


            
             return View();

        }
    }
}