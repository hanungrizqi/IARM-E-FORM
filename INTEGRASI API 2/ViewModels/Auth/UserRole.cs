﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INTEGRASI_API_2.ViewModels.Auth
{
    public class UserRole
    {
        public string Nrp { get; set; }
        public string Role { get; set; }
        public string District { get; set; }
        public string Department { get; set; }
        public string Posid { get; set; }
        public bool IsSectionHead { get; set; }

    }
}