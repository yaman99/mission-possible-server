using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Common.Interfaces.Services
{
    public  interface IFileUploadService
    {
        Task<string> CreateFileAsync(IFormFile FileObject);
    }
}
