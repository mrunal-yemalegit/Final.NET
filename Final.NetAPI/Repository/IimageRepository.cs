using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final.NetAPI.Repository
{
    public interface IimageRepository
    {
        Task<string> Upload(IFormFile file, string fileName);
    }
}
