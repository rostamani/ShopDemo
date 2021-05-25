using System;
using System.Collections.Generic;
using System.Text;

namespace _0_Framework.Application
{
    public class ValidationMessage
    {
        public const string IsRequired = "این مقدار نمیتواند خالی باشد.";
        public const string MaxFileSizeError = "حجم فایل بیشتر از مقدار مجاز است.";
        public const string InvalidFileFormat = "فرمت فایل معتبر نیست.";
        public const string ComparePassword = "کلمه عبور و تکرار کلمه عبور با هم تطابق ندارند.";
        public const string MaxLength = "طول این فیلد بیشتر از حد مجاز است.";
    }
}
