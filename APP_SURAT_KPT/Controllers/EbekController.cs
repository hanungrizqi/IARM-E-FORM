using APP_SURAT_KPT.Models;
using APP_SURAT_KPT.Models.ADO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TBL_H_LETTER_EBEK = APP_SURAT_KPT.Models.ADO.TBL_H_LETTER_EBEK;
using TBL_H_LETTER_EBEK_RELASI = APP_SURAT_KPT.Models.ADO.TBL_H_LETTER_EBEK_RELASI;

namespace APP_SURAT_KPT.Controllers
{
    public class EbekController : Controller
    {
        // GET: Ebek
        //public ActionResult Ebek()
        //{
        //    return View();
        //}
        private readonly DbSurat_KPTDataContext dbSurat_KPTDataContext;
        public EbekController()
        {
            var KPTDataContext = new DbSurat_KPTDataContext();
            dbSurat_KPTDataContext = KPTDataContext;
        }



        public ActionResult Ebek()
        {
            // Auth auth = new Auth(this.HttpContext);

            var nrp = @Session["Employee_id"];

            // Memanggil stored procedure menggunakan LINQ
            var result = dbSurat_KPTDataContext.Getdatasurat_EBEK(nrp.ToString())
                .Select(x => new Getdatasurat_EBEKResult
                {
                    NO_SURAT = x.NO_SURAT,
                    NRP = x.NRP,
                    NAME = x.NAME,
                    DEPT_DESC = x.DEPT_DESC,
                    Address = x.Address,
                    LETTER_DATE = x.LETTER_DATE,
                    CRE_DATE = x.CRE_DATE,
                    CRE_BY = x.CRE_BY,
                    MOD_DATE = x.MOD_DATE,
                    MOD_BY = x.MOD_BY,
                    No_telp=x.No_telp,
                    Site= x.Site,
                    Tanggal_Masuk=x.Tanggal_Masuk
                    
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
            ViewBag.No_telp = result.No_telp;
            ViewBag.Site = result.Site;
            ViewBag.Tanggal_masuk=result.Tanggal_Masuk;
            return View(result);
        }






        [HttpPost]
        public ActionResult InsertData()
        {
            try
            {
                var context = HttpContext.Request;
                string json = context.Form["insertKpp"];
                InsertEbekHistoryRequest request = JsonConvert.DeserializeObject<InsertEbekHistoryRequest>(json);

                //throw new NotImplementedException();

                using (DB_Surat_KPTEntities dbcontext = new DB_Surat_KPTEntities())
                {
                    TBL_H_LETTER_EBEK ebek = new TBL_H_LETTER_EBEK();
                    ebek.NO_SURAT = "0000";
                    ebek.KELUARGA_DI_KPP_FLAG = (request.familyKPP.Count > 0);
                    ebek.KELUARGA_DI_REKANAN_KPP = (request.familyRekanan.Count > 0);
                    Console.WriteLine(ebek);
                    dbcontext.Entry(ebek).State = System.Data.Entity.EntityState.Added;
                    dbcontext.SaveChanges();

                    if (request.familyKPP != null && request.familyKPP.Any())
                    {
                        foreach (FamilyKPP item in request.familyKPP)
                        {
                            TBL_H_LETTER_EBEK_RELASI relasiKpp = new TBL_H_LETTER_EBEK_RELASI();
                            relasiKpp.ID_EBEK_HISTORY = ebek.ID;
                            relasiKpp.TYPE = item.Type;
                            relasiKpp.NAMA = item.Nama;
                            relasiKpp.JABATAN = item.Jabatan;
                            relasiKpp.DEPARTMENT = item.Department;
                            relasiKpp.RELASI = item.Relasi;

                            dbcontext.Entry(relasiKpp).State = System.Data.Entity.EntityState.Added;
                        }
                    }

                    if (request.familyRekanan != null && request.familyRekanan.Any())
                    {
                        foreach (FamilyRekanan itemRekanan in request.familyRekanan)
                        {
                            TBL_H_LETTER_EBEK_RELASI relasiRekanan = new TBL_H_LETTER_EBEK_RELASI();
                            relasiRekanan.ID_EBEK_HISTORY = ebek.ID;
                            relasiRekanan.TYPE = itemRekanan.Type;
                            relasiRekanan.NAMA = itemRekanan.Nama;
                            relasiRekanan.JABATAN = itemRekanan.Jabatan;
                            relasiRekanan.PERUSAHAAN = itemRekanan.Perusahaan;
                            relasiRekanan.RELASI = itemRekanan.Relasi;

                            dbcontext.Entry(relasiRekanan).State = System.Data.Entity.EntityState.Added;
                        }
                    }

                    if (request.coi != null && request.coi.Any())
                    {
                        TBL_H_LETTER_EBEK_COI coi = new TBL_H_LETTER_EBEK_COI();
                        coi.ID_EBEK_HISTORY = ebek.ID;
                        coi.COI_NO_1_DEPARTMENT = request.coi[0].Department;
                        coi.COI_NO_1_JABATAN = request.coi[0].Jabatan;
                        coi.COI_NO_1_KELUARGA = request.coi[0].Keluarga;

                        coi.COI_NO_2_DEPARTMENT = request.coi[1].Department;
                        coi.COI_NO_2_JABATAN = request.coi[1].Jabatan;
                        coi.COI_NO_2_KELUARGA = request.coi[1].Keluarga;

                        coi.COI_NO_3_DEPARTMENT = request.coi[2].Department;
                        coi.COI_NO_3_JABATAN = request.coi[2].Jabatan;
                        coi.COI_NO_3_KELUARGA = request.coi[2].Keluarga;

                        coi.COI_NO_4_DEPARTMENT = request.coi[3].Department;
                        coi.COI_NO_4_JABATAN = request.coi[3].Jabatan;
                        coi.COI_NO_4_KELUARGA = request.coi[3].Keluarga;

                        coi.COI_NO_5_DEPARTMENT = request.coi[4].Department;
                        coi.COI_NO_5_JABATAN = request.coi[4].Jabatan;
                        coi.COI_NO_5_KELUARGA = request.coi[4].Keluarga;
                    }
                    dbcontext.SaveChanges();
                }

                HttpContext.Response.StatusCode = 201;

                return Json(new
                {
                    status = 201,
                    message = "Created"
                });
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = 500;

                return Json(new
                {
                    status = 500,
                    message = ex.Message
                });
            }
        }
    }
}