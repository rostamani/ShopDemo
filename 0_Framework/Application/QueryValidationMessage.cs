using System;
using System.Collections.Generic;
using System.Text;

namespace _0_Framework.Application
{
    public class QueryValidationMessage
    {
        public const string NotFound = "چنین رکوردی در دیتابیس وجود ندارد.";
        public const string DuplicateRecord = "امکان ثبت رکورد تکراری وجود ندارد.";
        public const string WrongUserPass = "نام کاربری یا رمز عبور اشتباه است.";
    }
}
