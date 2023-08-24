using INTEGRASI_API_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INTEGRASI_API_2.Cls
{
    public class ClsMasterData
    {
        DBPakta_IntegritasDataContext db = new DBPakta_IntegritasDataContext();

        public List<VW_DISTRICT_ALL> GetListDistrict()
        {
            var listDistrict = db.VW_DISTRICT_ALLs.ToList();
            return listDistrict;
        }

        public List<VW_DEPT_ALL> GetListDept()
        {
            var listDistrict = db.VW_DEPT_ALLs.ToList();
            return listDistrict;
        }

    }
}