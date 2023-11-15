﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using INTEGRASI_API_2.Cls;
using INTEGRASI_API_2.Models;

namespace INTEGRASI_API_2.Controllers
{
    [RoutePrefix("api/Gratifikasi")]
    public class GratifikasiController : ApiController
    {
        DBPakta_IntegritasDataContext db = new DBPakta_IntegritasDataContext();

        [HttpPost]
        [Route("submitGratifikasi")]
        public IHttpActionResult submitGratifikasi(TBL_T_GRATIFIKASI param)
        {
            try
            {
                TBL_T_GRATIFIKASI tbl = new TBL_T_GRATIFIKASI();
                tbl.NRP = param.NRP;
                tbl.CREATED_DATE = DateTime.Now;
                tbl.JENIS_PENERIMAAN = param.JENIS_PENERIMAAN;
                tbl.ESTIMASI_HARGA = param.ESTIMASI_HARGA;
                tbl.TEMPAT_PENERIMAAN = param.TEMPAT_PENERIMAAN;
                tbl.TANGGAL_PENERIMAAN = param.TANGGAL_PENERIMAAN;
                tbl.NAMA_PEMBERI = param.NAMA_PEMBERI;
                tbl.PEKERJAAN_PEMBERI = param.PEKERJAAN_PEMBERI;
                tbl.NAMA_PERUSAHAAN = param.NAMA_PERUSAHAAN;
                tbl.HUBUNGAN_PEMBERI = param.HUBUNGAN_PEMBERI;
                tbl.ALASAN_PEMBERIAN = param.ALASAN_PEMBERIAN;

                db.TBL_T_GRATIFIKASIs.InsertOnSubmit(tbl);
                db.SubmitChanges();

                return Ok(new { Remarks = true });
            }
            catch (Exception e)
            {
                return Ok(new { Remarks = false, Message = e });
            }
        }

        [HttpPost]
        [Route("uploadBuktiGratifikasi")]
        public IHttpActionResult uploadBuktiGratifikasi()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var files = httpRequest.Files;
                var attachmentUrls = new List<string>();

                if (files.Count > 0)
                {
                    using (var dbContext = new DBPakta_IntegritasDataContext())
                    {
                        var latestGratifikasi = dbContext.TBL_T_GRATIFIKASIs.OrderByDescending(g => g.ID).FirstOrDefault();

                        if (latestGratifikasi != null)
                        {
                            for (int i = 0; i < files.Count; i++)
                            {
                                var postedFile = files[i];
                                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(postedFile.FileName);
                                var folderPath = HttpContext.Current.Server.MapPath("~/Content/BuktiGratifikasi");

                                if (!Directory.Exists(folderPath))
                                {
                                    Directory.CreateDirectory(folderPath);
                                }

                                var filePath = Path.Combine(folderPath, fileName);
                                if (File.Exists(filePath))
                                {
                                    return Ok(new { Remarks = false });
                                }

                                using (var fileStream = File.Create(filePath))
                                {
                                    postedFile.InputStream.CopyTo(fileStream);
                                    fileStream.Flush();
                                }
                                File.SetAttributes(filePath, FileAttributes.Normal);

                                var attachmentUrl = Url.Content("~/Content/BuktiGratifikasi/" + fileName);
                                attachmentUrls.Add(attachmentUrl);
                            }
                            latestGratifikasi.BUKTI_GRATIFIKASI = string.Join(",", attachmentUrls);
                            dbContext.SubmitChanges();
                        }
                    }

                    return Ok(new { Remarks = true, AttachmentUrls = attachmentUrls });
                }
                else
                {
                    return Ok(new { Remarks = true });
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}
