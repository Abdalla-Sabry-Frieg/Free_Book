using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Helper
    {
        //View
        public const string imagesPathUsers = "/Images/Users/";
        //Save
        public const string imagesSaveUsers = "Images/Users";

        // Sessions 

        public const string Success = "Success";
        public const string Error = "Error";

        public const string MsgType = "msgType";
        public const string Title = "title";
        public const string Msg = "msg";

        public const string Save = "Save";
        public const string Update = "Update";
        public const string Delete = "Delete";

        public enum eCurrentState
        {
            Active=1,
            Delete=0
        }

        // User & Roles 

        public const string Email = "superadmin@domin.com";
        public const string UserName = "superadmin@domin.com";
        public const string Name = "SuperAdmin";
        public const string Password = "superadmin@P@$$w0rd123456";

        public const string EmailBasic = "basicuser@domin.com";
        public const string UserNameBasic = "basicuser@domin.com";
        public const string NameBasic = "BasicUser";
        public const string PasswordBasic = "basicuser@P@$$w0rd123456";


        public const string Permission = "Permission";



        public enum Roles
        {
            SuperAdmin,
            Admin,
            Basic
        }

        public enum PermissionModuleName
        {
            Home,
            Accounts,
            Roles,
            Registers,
            Categories
        }
    }
}
