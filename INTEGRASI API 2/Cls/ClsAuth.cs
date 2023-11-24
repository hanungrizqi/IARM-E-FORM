using FormsAuth;
using INTEGRASI_API_2.Models;
using INTEGRASI_API_2.ViewModels;
using INTEGRASI_API_2.ViewModels.Auth;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace INTEGRASI_API_2.Cls
{
    public class ClsAuth
    {
        DBPakta_IntegritasDataContext db = new DBPakta_IntegritasDataContext();

        public bool AuthLDAP(LoginVM login)
        {
            bool status_ldap = false;

            if (login.Username.Length > 7)
            {
                status_ldap = CheckValidLogin(login);
            }
            else
            {
                status_ldap = OpenLdap(login.Username, login.Password);
            }

            return status_ldap;
        }
        private bool CheckValidLogin(LoginVM login)
        {
            bool status = false;

            try
            {
                var ldap = new LdapAuthentication("LDAP://KPPMINING:389");
                status = ldap.IsAuthenticated("KPPMINING", login.Username, login.Password);
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

        public UserRole GetUserData(string nrp)
        {
            UserRole dataUser = new UserRole();
            if (nrp.Length <= 7)
            {
                nrp = "k" + nrp;
            }

            var getUser = db.VW_ALL_USERs.Where(x => "k" + x.EMPLOYEE_ID == nrp).FirstOrDefault();

            if (getUser != null)
            {
                dataUser.Nrp = getUser.EMPLOYEE_ID;
                dataUser.Role = getUser.ROLE;
                dataUser.District = getUser.DSTRCT_CODE;
                dataUser.Department = getUser.DEPT_DESC;
                dataUser.Posid = getUser.POSITION_ID;
                dataUser.IsSectionHead = getUser.SECTION_HEAD == null ? false : true;
            }
            return dataUser;
        }
    }
}