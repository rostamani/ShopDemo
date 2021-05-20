using System;
using System.Collections.Generic;
using System.Text;

namespace _0_Framework.Application
{
    public class OperationResult
    {
        public string Message { get; set; }

        public bool IsSucceeded { get; set; }

        public OperationResult()
        {
            IsSucceeded = false;
            Message = "عملیات با شکست مواجه شد.";
        }

        public OperationResult Succeeded(string message="عملیات با موفقیت انجام شد.")
        {
            IsSucceeded = true;
            Message = message;
            return this;
        }

        public OperationResult Failed(string message)
        {
            IsSucceeded = false;
            Message = message;
            return this;
        }
    }
}
