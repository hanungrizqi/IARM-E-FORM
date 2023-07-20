using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace APP_SURAT_KPT.Controllers
{
    public class EbekController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult Ebek()
        {
            return View();
        }

        public ActionResult EbekReport()
        {
            return View();
        }






        //[HttpPost]
        //public ActionResult InsertData()
        //{
        //    try
        //    {
        //        var context = HttpContext.Request;
        //        string json = context.Form["insertKpp"];
        //        InsertEbekHistoryRequest request = JsonConvert.DeserializeObject<InsertEbekHistoryRequest>(json);

        //        throw new NotImplementedException();

        //        using (DB_Surat_KPTEntities dbcontext = new DB_Surat_KPTEntities())
        //        {
        //            TBL_H_LETTER_EBEK ebek = new TBL_H_LETTER_EBEK();
        //            ebek.NO_SURAT = "0000";
        //            ebek.KELUARGA_DI_KPP_FLAG = (request.familyKPP.Count > 0);
        //            ebek.KELUARGA_DI_REKANAN_KPP = (request.familyRekanan.Count > 0);
        //            Console.WriteLine(ebek);
        //            dbcontext.Entry(ebek).State = System.Data.Entity.EntityState.Added;
        //            dbcontext.SaveChanges();

        //            if (request.familyKPP != null && request.familyKPP.Any())
        //            {
        //                foreach (FamilyKPP item in request.familyKPP)
        //                {
        //                    TBL_H_LETTER_EBEK_RELASI relasiKpp = new TBL_H_LETTER_EBEK_RELASI();
        //                    relasiKpp.ID_EBEK_HISTORY = ebek.ID;
        //                    relasiKpp.TYPE = item.Type;
        //                    relasiKpp.NAMA = item.Nama;
        //                    relasiKpp.JABATAN = item.Jabatan;
        //                    relasiKpp.DEPARTMENT = item.Department;
        //                    relasiKpp.RELASI = item.Relasi;

        //                    dbcontext.Entry(relasiKpp).State = System.Data.Entity.EntityState.Added;
        //                }
        //            }

        //            if (request.familyRekanan != null && request.familyRekanan.Any())
        //            {
        //                foreach (FamilyRekanan itemRekanan in request.familyRekanan)
        //                {
        //                    TBL_H_LETTER_EBEK_RELASI relasiRekanan = new TBL_H_LETTER_EBEK_RELASI();
        //                    relasiRekanan.ID_EBEK_HISTORY = ebek.ID;
        //                    relasiRekanan.TYPE = itemRekanan.Type;
        //                    relasiRekanan.NAMA = itemRekanan.Nama;
        //                    relasiRekanan.JABATAN = itemRekanan.Jabatan;
        //                    relasiRekanan.PERUSAHAAN = itemRekanan.Perusahaan;
        //                    relasiRekanan.RELASI = itemRekanan.Relasi;

        //                    dbcontext.Entry(relasiRekanan).State = System.Data.Entity.EntityState.Added;
        //                }
        //            }

        //            if (request.coi != null && request.coi.Any())
        //            {
        //                TBL_H_LETTER_EBEK_COI coi = new TBL_H_LETTER_EBEK_COI();
        //                coi.ID_EBEK_HISTORY = ebek.ID;
        //                coi.COI_NO_1_DEPARTMENT = request.coi[0].Department;
        //                coi.COI_NO_1_JABATAN = request.coi[0].Jabatan;
        //                coi.COI_NO_1_KELUARGA = request.coi[0].Keluarga;

        //                coi.COI_NO_2_DEPARTMENT = request.coi[1].Department;
        //                coi.COI_NO_2_JABATAN = request.coi[1].Jabatan;
        //                coi.COI_NO_2_KELUARGA = request.coi[1].Keluarga;

        //                coi.COI_NO_3_DEPARTMENT = request.coi[2].Department;
        //                coi.COI_NO_3_JABATAN = request.coi[2].Jabatan;
        //                coi.COI_NO_3_KELUARGA = request.coi[2].Keluarga;

        //                coi.COI_NO_4_DEPARTMENT = request.coi[3].Department;
        //                coi.COI_NO_4_JABATAN = request.coi[3].Jabatan;
        //                coi.COI_NO_4_KELUARGA = request.coi[3].Keluarga;

        //                coi.COI_NO_5_DEPARTMENT = request.coi[4].Department;
        //                coi.COI_NO_5_JABATAN = request.coi[4].Jabatan;
        //                coi.COI_NO_5_KELUARGA = request.coi[4].Keluarga;
        //            }
        //            dbcontext.SaveChanges();
        //        }

        //        HttpContext.Response.StatusCode = 201;

        //        return Json(new
        //        {
        //            status = 201,
        //            message = "Created"
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        HttpContext.Response.StatusCode = 500;

        //        return Json(new
        //        {
        //            status = 500,
        //            message = ex.Message
        //        });
        //    }
        //}
    }
}