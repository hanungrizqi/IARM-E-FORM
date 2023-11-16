using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APP_SURAT_KPT.ViewModels.Account
{
    public class LoginResponseVM
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public UserData Data { get; set; }
    }

    public class UserData
    {
        public string Nrp { get; set; }
        public string Role { get; set; }
        public string District { get; set; }
        public bool IsSectionHead { get; set; }
    }
}