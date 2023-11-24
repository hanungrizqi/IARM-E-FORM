using INTEGRASI_API_2.Models;
using INTEGRASI_API_2.ViewModels.PI;
using INTEGRASI_API_2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INTEGRASI_API_2.ViewModels.Ebek;
using Microsoft.Ajax.Utilities;

namespace INTEGRASI_API_2.Cls
{
    public class ClsGratifikasi
    {
        DBPakta_IntegritasDataContext db = new DBPakta_IntegritasDataContext();

        public List<VW_GRATIFIKASI_REPORT_PM> GetDataTablePM(string district, string nrp)
        {
            var chcekRole = db.VW_ALL_USERs.Where(a => a.EMPLOYEE_ID == nrp).FirstOrDefault();
            if (chcekRole.ROLE == "ADMINISTRATOR")
            {
                var listDataPI = db.VW_GRATIFIKASI_REPORT_PMs.Where(a => a.STATUS == "CREATED").ToList();
                return listDataPI;
            }
            else
            {
                var listDataPI = db.VW_GRATIFIKASI_REPORT_PMs.Where(a => a.STATUS == "CREATED" && a.SITE == district).ToList();
                return listDataPI;
            }
        }

        public List<VW_GRATIFIKASI_REPORT_PM> GetFilteredDataTablePM(PIFilterRequestsVM requestsVM, string district)
        {
            var listDataPI = db.VW_GRATIFIKASI_REPORT_PMs.Where(a => a.STATUS == "CREATED" && a.SITE == district).AsQueryable();
            if (!requestsVM.Nama.IsNullOrWhiteSpace())
            {
                listDataPI = listDataPI.Where(x => x.NAME.Contains(requestsVM.Nama));
            }

            if (!requestsVM.District.IsNullOrWhiteSpace())
            {
                listDataPI = listDataPI.Where(x => x.SITE == requestsVM.District);
            }

            if (!requestsVM.Dept.IsNullOrWhiteSpace())
            {
                listDataPI = listDataPI.Where(x => x.DEPT == requestsVM.Dept);
            }

            return listDataPI.ToList();
        }

        public List<VW_GRATIFIKASI_REPORT_DEPTHEAD> GetDataTableDeptHead(string dept)
        {
            var listDataPI = db.VW_GRATIFIKASI_REPORT_DEPTHEADs.Where(a => a.STATUS == "CREATED" && a.DEPT == dept).ToList();
            return listDataPI;
        }

        public List<VW_GRATIFIKASI_REPORT_DEPTHEAD> GetFilteredDataTableDeptHead(PIFilterRequestsVM requestsVM, string dept)
        {
            var listDataPI = db.VW_GRATIFIKASI_REPORT_DEPTHEADs.Where(a => a.STATUS == "CREATED" && a.DEPT == dept).AsQueryable();
            if (!requestsVM.Nama.IsNullOrWhiteSpace())
            {
                listDataPI = listDataPI.Where(x => x.NAME.Contains(requestsVM.Nama));
            }

            if (!requestsVM.District.IsNullOrWhiteSpace())
            {
                listDataPI = listDataPI.Where(x => x.SITE == requestsVM.District);
            }

            if (!requestsVM.Dept.IsNullOrWhiteSpace())
            {
                listDataPI = listDataPI.Where(x => x.DEPT == requestsVM.Dept);
            }

            return listDataPI.ToList();
        }

        public List<VW_GRATIFIKASI_REPORT_BOD> GetDataTableBOD(string posid)
        {
            //var listDataPI = db.VW_GRATIFIKASI_REPORT_BODs.Where(a => a.STATUS == "CREATED").ToList();
            //return listDataPI;

            var listDataPI = db.VW_GRATIFIKASI_REPORT_BODs.Where(a => a.STATUS == "CREATED" &&
                (a.Atasan_1 == posid ||
                    a.Atasan_2 == posid ||
                    a.Atasan_3 == posid ||
                    a.Atasan_4 == posid ||
                    a.Atasan_5 == posid ||
                    a.Atasan_6 == posid ||
                    a.Atasan_7 == posid ||
                    a.Atasan_8 == posid ||
                    a.Atasan_9 == posid ||
                    a.Atasan_10 == posid)).ToList();
            return listDataPI;
        }

        public List<VW_GRATIFIKASI_REPORT_BOD> GetFilteredDataTableBOD(PIFilterRequestsVM requestsVM)
        {
            var listDataPI = db.VW_GRATIFIKASI_REPORT_BODs.Where(a => a.STATUS == "CREATED").AsQueryable();
            if (!requestsVM.Nama.IsNullOrWhiteSpace())
            {
                listDataPI = listDataPI.Where(x => x.NAME.Contains(requestsVM.Nama));
            }

            if (!requestsVM.District.IsNullOrWhiteSpace())
            {
                listDataPI = listDataPI.Where(x => x.SITE == requestsVM.District);
            }

            if (!requestsVM.Dept.IsNullOrWhiteSpace())
            {
                listDataPI = listDataPI.Where(x => x.DEPT == requestsVM.Dept);
            }

            return listDataPI.ToList();
        }
    }
}