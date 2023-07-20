using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INTEGRASI_API_2.ViewModels.Ebek
{
    public class EbekRequestsVM
    {
        public string Nrp { get; set; }
        public string Location { get; set; }
        public bool NoFamily { get; set; }
        public bool NoMikad { get; set; }
        public bool NoCoi { get; set; }
        public List<MemberData> HadFamily { get; set; }
        public List<MemberData> HadMikad { get; set; }
        public CoiData HadCoi { get; set; }
    }

    public class MemberData
    {
        public string MemberName { get; set;}
        public string MemberPos { get; set;}
        public string MemberDept { get; set;}
        public string MemberRelation { get; set;}
    }

    public class CoiData
    {
        public CoiMember BusinessRelation { get; set;}
        public CoiMember InvestationNonPublic { get; set;}
        public CoiMember InvestationSupplier { get; set;}
        public CoiMember AnotherBusiness { get; set;}
        public CoiMember SideJob { get; set;}
    }

    public class CoiMember
    {
        public string MemberCompany { get; set;}
        public string MemberPosition { get; set;}
        public string Detail { get; set;}
    }
}