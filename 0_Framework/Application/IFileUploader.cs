using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace _0_Framework.Application
{
    public interface IFileUploader
    {
        string FileUpload(IFormFile file,string path);
    }
}
