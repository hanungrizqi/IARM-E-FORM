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

        public List<VW_GRATIFIKASI_REPORT_PM> GetDataTablePM(string district)
        {
            var listDataPI = db.VW_GRATIFIKASI_REPORT_PMs.Where(a => a.STATUS == "CREATED" && a.SITE == district).ToList();
            return listDataPI;
        }

        public List<VW_GRATIFIKASI_REPORT_PM> GetFilteredDataTablePM(PIFilterRequestsVM requestsVM)
        {
            var listDataPI = db.VW_GRATIFIKASI_REPORT_PMs.AsQueryable();
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

        public List<VW_GRATIFIKASI_REPORT_DEPTHEAD> GetDataTableDeptHead()
        {
            var listDataPI = db.VW_GRATIFIKASI_REPORT_DEPTHEADs.Where(a => a.STATUS == "CREATED").ToList();
            return listDataPI;
        }

        public List<VW_GRATIFIKASI_REPORT_DEPTHEAD> GetFilteredDataTableDeptHead(PIFilterRequestsVM requestsVM)
        {
            var listDataPI = db.VW_GRATIFIKASI_REPORT_DEPTHEADs.AsQueryable();
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

        public List<VW_GRATIFIKASI_REPORT_BOD> GetDataTableBOD()
        {
            var listDataPI = db.VW_GRATIFIKASI_REPORT_BODs.Where(a => a.STATUS == "CREATED").ToList();
            return listDataPI;
        }

        public List<VW_GRATIFIKASI_REPORT_BOD> GetFilteredDataTableBOD(PIFilterRequestsVM requestsVM)
        {
            var listDataPI = db.VW_GRATIFIKASI_REPORT_BODs.AsQueryable();
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