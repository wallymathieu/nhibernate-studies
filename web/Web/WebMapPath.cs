using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SomeBasicNHApp.Core;

namespace SomeBasicNHApp
{
    public class WebMapPath : IMapPath
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public WebMapPath(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public string MapPath(string path) => Path.Combine(_hostingEnvironment.ContentRootPath, "..", "..", path);
    }
}
