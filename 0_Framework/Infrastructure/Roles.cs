using System;
using System.Collections.Generic;
using System.Text;

namespace _0_Framework.Infrastructure
{
    public static class Roles
    {
        public const string Admin = "1";
        public const string User = "2";
        public const string Operator = "3";

        public static string GetRoleBy(long id)
        {
            switch (id)
            {
                case 1:
                    return "مدیر سیستم";
                case 2:
                    return "کاربر عادی";
                case 3:
                    return "اپراتور سیستم";
                default:
                    return "";
            }
        }
    }
}
