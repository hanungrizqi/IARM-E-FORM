using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INTEGRASI_API_2.ViewModels.PI
{
    public class GeneratePDF
    {
        public string NRP { get; set; }
        public string NAME { get; set; }
        public string DEPT { get; set; }
        public string SITE { get; set; }
        public string SIGN_LOCATION { get; set; }
        public DateTime SUBMITDATE { get; set; }
        public string EMAIL { get; set; }
    }
}