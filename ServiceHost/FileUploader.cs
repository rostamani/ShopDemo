using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ServiceHost
{
    public class FileUploader:IFileUploader
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUploader(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public string FileUpload(IFormFile file,string path)
        {
            if (file==null)
            {
                return "";
            }

            var directory = $"{_webHostEnvironment.WebRootPath}//UploadedFiles//{path}";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var fileName = DateTime.Now.ToFileTime() + "-" + file.FileName;
            var fileDirectory = Path.Combine(directory, fileName);
            using var output = File.Create(fileDirectory);
            file.CopyTo(output);

            return $"{path}//{fileName}";
        }
    }
}
