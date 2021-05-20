using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace _0_Framework.Application
{
    public class FileExtensionLimitAttribute:ValidationAttribute,IClientModelValidator
    {
        private readonly string[] _allowedExtensions;

        public FileExtensionLimitAttribute(string[] allowedExtensions)
        {
            _allowedExtensions = allowedExtensions;
        }

        public override bool IsValid(object value)
        {
            var file = value as IFormFile;
            if (file==null)
            {
                return true;
            }

            var extension = Path.GetExtension(file.FileName);
            return _allowedExtensions.Contains(extension);
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            context.Attributes.Add("data-val-fileExtensionLimit", ErrorMessage);
        }

        private static bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }
            attributes.Add(key, value);
            return true;
        }
    }
}
