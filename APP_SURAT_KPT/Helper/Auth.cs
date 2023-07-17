using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.UI.WebControls;
using FormsAuth;
using APP_SURAT_KPT.Models;
using APP_SURAT_KPT.ViewModels.Account;



namespace APP_SURAT_KPT.Helper
{
    public partial class Auth
    {
        DbSurat_KPTDataContext db = new DbSurat_KPTDataContext();
        bool status_ldap = false;

        public bool AuthLDAP(LoginVM request)
        {
            if (request.nrp.Length > 7)
            {
                status_ldap = CheckValidLogin(request);
            }
            else
            {
                status_ldap = OpenLdap(request.nrp, request.password);
            }
            return status_ldap;
        }
        private bool CheckValidLogin(LoginVM request)
        {
            bool status = false;

            try
            {
                var ldap = new LdapAuthentication("LDAP://KPPMINING:389");
                status = ldap.IsAuthenticated("KPPMINING", request.nrp, request.password);
            }
            catch (Exception)
            {
                status = false;
            }

            return status;

        }

        private bool OpenLdap(string username, string password)
        {
            bool status = true;
            String uid = $"cn={username},ou=Users,dc=kpp,dc=net";

            DirectoryEntry root = new DirectoryEntry("LDAP://10.12.101.102", uid, password, AuthenticationTypes.None);
            try
            {
                object connected = root.NativeObject;
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

            }
}