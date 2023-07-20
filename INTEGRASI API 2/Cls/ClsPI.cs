using INTEGRASI_API_2.Models;
using INTEGRASI_API_2.ViewModels;
using INTEGRASI_API_2.ViewModels.PI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INTEGRASI_API_2.Cls
{
    public class ClsPI
    {
        DBPakta_IntegritasDataContext db = new DBPakta_IntegritasDataContext();

        public bool IsUserHasSubmitPi(string Nrp)
        {
            var userData = db.VW_ALL_USERs.Where(x => x.EMPLOYEE_ID == Nrp).FirstOrDefault();
            return userData.PI_SUBMIT == null ? false : userData.PI_SUBMIT.Value;
        }

        public PiDataResponseVM GetDataPI(string Nrp)
        {
            PiDataResponseVM responseVM = new PiDataResponseVM();
            var userData = db.VW_ALL_USERs.Where(x => x.EMPLOYEE_ID == Nrp).FirstOrDefault();
            responseVM.PiNo = GenerateNoPI();
            responseVM.Nrp = userData.EMPLOYEE_ID;
            responseVM.Name = userData.NAME;
            responseVM.Dept = userData.DEPT_DESC;
            responseVM.IsSubmitted = userData.PI_SUBMIT.HasValue ? userData.PI_SUBMIT.Value : false;
            return responseVM;
        }

        public string GenerateNoPI()
        {
            int number;
            string piNumber;
            var nowDateMonth = DateTime.Now.ToString("MM");
            var nowDateYear = DateTime.Now.ToString("yyyy");
            var findPiNumber = "INTEG" + "/" + nowDateYear + "/" + nowDateMonth;
            var latestPiNumber  = db.TBL_T_PIs.Where(x => x.PI_NO.Contains(findPiNumber))
                                                .OrderByDescending(x => x.PI_NO)
                                                .Select(x => x.PI_NO)
                                                .FirstOrDefault();
            if (latestPiNumber == null)
            {
                piNumber = "06/0001/INTEG/" + nowDateYear + "/" + nowDateMonth;
            }
            else
            {
                number = (int.Parse(latestPiNumber.Substring(3, 4)) + 1);
                piNumber = "06/" + number.ToString().PadLeft(4, '0') + "/INTEG/" + nowDateYear + "/" + nowDateMonth;
            }
            return piNumber;
        }

        public StatusResponseVM SubmitPaktaIntegritas(SubmitPIDataRequestVM requestVM)
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