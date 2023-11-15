using INTEGRASI_API_2.Models;
using INTEGRASI_API_2.ViewModels.PI;
using INTEGRASI_API_2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INTEGRASI_API_2.Cls
{
    public class ClsGratifikasi
    {
        DBPakta_IntegritasDataContext db = new DBPakta_IntegritasDataContext();

        public StatusResponseVM SubmitGratifikasiData(SubmitPIDataRequestVM requestVM)
        {
            try
            {
                TBL_T_PI newData = new TBL_T_PI()
                {
                    NRP = requestVM.UserNrp,
                    SUBMIT = true,
                    SIGN_LOCATION = requestVM.Location,
                    SUBMITDATE = DateTime.Now
                };
                db.TBL_T_PIs.InsertOnSubmit(newData);
                db.SubmitChanges();

                var responseVM = new StatusResponseVM()
                {
                    Success = true,
                    Message = "Data berhasil disubmit"
                };

                return responseVM;
            }
            catch (Exception ex)
            {
                var responseVM = new StatusResponseVM()
                {
                    Success = false,
                    Message = $"Terjadi kesalahan: {ex.Message}"
                };
                return responseVM;
            }
        }
    }
}