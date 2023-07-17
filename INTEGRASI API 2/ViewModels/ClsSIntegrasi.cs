using INTEGRASI_API_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INTEGRASI_API_2.ViewModels
{
    
    public class ClsSIntegrasi
    {
        DBSuratKPTDataContext db = new DBSuratKPTDataContext();

        public string NRP { get; set; }

        public string NAME { get; set; }

        public string POSITION_ID { get; set; }

        public string DEPT_DESC { get; set; }

        public string Address { get; set; }

        public System.DateTime LETTER_DATE { get; set; }

        public System.DateTime CRE_DATE { get; set; }

        public string CRE_BY { get; set; }

        public System.Nullable<int> MOD_DATE { get; set; }

        public System.Nullable<int> MOD_BY { get; set; }
       
            }
}