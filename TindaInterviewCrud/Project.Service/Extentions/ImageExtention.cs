using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Extentions
{
    public static class ImageExtention
    {
        public static bool IsImage(this IFormFile file)
        {
            return file.ContentType.Contains("Image");
        }
        public static bool IsSizeOk(this IFormFile file,int mb)
        {
            return file.Length / 1024 / 1024 <= mb;
        }
        public static string CreateImage(this IFormFile file, string folder, string path)
        {
            string fullName = Guid.NewGuid().ToString() + file.FileName;
            string fulPath = Path.Combine(folder, path, fullName);
            using (FileStream stream = new(fulPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return fullName;
        }


        public static async Task<string> ToBase64StringAsync(this IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();
                return Convert.ToBase64String(fileBytes);
            }
        }

    }
}
