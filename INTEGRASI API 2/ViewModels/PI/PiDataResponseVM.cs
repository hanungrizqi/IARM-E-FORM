using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INTEGRASI_API_2.ViewModels.PI
{
    public class PiDataResponseVM
    {
        public string PiNo { get; set; }
        public string Nrp { get; set; }
        public string Name { get; set; }
        public string Dept { get; set; }
        public bool IsSubmitted { get; set; }
    }
}