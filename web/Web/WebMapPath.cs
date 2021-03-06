﻿using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SomeBasicNHApp.Core;

namespace SomeBasicNHApp
{
    public class WebMapPath : IMapPath
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public WebMapPath(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public string MapPath(string path) => Path.Combine(_hostingEnvironment.ContentRootPath, "..", "..", path);
    }
}
