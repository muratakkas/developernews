using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dews.Api.Extensions
{
    public static class ImageExtensions
    {
        public static void SaveImage(string path, string imageBase64)
        {
            var base64array = Convert.FromBase64String(imageBase64);
            string filePath = Path.GetDirectoryName(path);
            if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);
            System.IO.File.WriteAllBytes(path, base64array);
        }

        public static void DeleteImage(string path)
        {
            System.IO.File.Delete(path);
        }
    }
}
