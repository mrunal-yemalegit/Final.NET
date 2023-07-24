using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Final.NetAPI.Repository
{
    public class LocalStorageImageRepository:IimageRepository
    {
        
        public async Task<string> Upload(IFormFile file, string fileName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Images", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            //using Stream fileStream = new FileStream(filePath, FileMode.Create);
            //await file.CopyToAsync(fileStream);
            return filePath;
        }

       /* private string GetServerRelativePath(string fileName)
        {
            return Path.Combine(@"Resources\Images\", fileName);
        }*/
    }
}
