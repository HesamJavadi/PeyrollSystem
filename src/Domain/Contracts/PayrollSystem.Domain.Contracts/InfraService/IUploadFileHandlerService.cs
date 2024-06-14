using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Domain.Contracts.InfraService
{
    public interface IUploadFileHandlerService
    {
        Task<string> SaveFileAsync(IFormFile file, string relativeDirectory);
        string ReturnFilePath(string relativeDirectory, string fileName);
    }
}
