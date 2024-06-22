using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Infrastructure.SQL.SeedData.SeedModel
{
    internal static class Roles
    {
        public const string Admin = "Admin";
        public const string User = "User";
        public const string Personnel = "Personnel";
        public const string PaySlip = "PaySlip";
        public const string PayStatement = "PayStatement";
        public const string Setting = "Setting";

        public const string FAdmin = "مدیر";
        public const string FUser = "کاربر";
        public const string FPersonnel = "پرسنل";
        public const string FPaySlip = "فیش حقوقی";
        public const string FPayStatement = "احکام کارگزینی";
        public const string FSetting = "تنظیمات";
    }

    internal static class Claims
    {
        public const string Create = "Create";
        public const string Read = "Read";
        public const string Update = "Update";
        public const string Delete = "Delete";
    }


}
