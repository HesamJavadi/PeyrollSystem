using Microsoft.AspNetCore.Http;
using PayrollSystem.Domain.Contracts.InfraService;
using System;
using System.IO;
using System.Threading.Tasks;


namespace PayrollSystem.Infrastructure.Service.UploadFiles;

public class UploadFileHandlerService : IUploadFileHandlerService
{
    private readonly string _uploadsPath;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UploadFileHandlerService(IHttpContextAccessor httpContextAccessor)
    {
        _uploadsPath = Directory.GetCurrentDirectory();
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> SaveFileAsync(IFormFile file, string relativeDirectory)
    {
        var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\" + relativeDirectory);
        if (!Directory.Exists(uploadsFolderPath))
        {
            Directory.CreateDirectory(uploadsFolderPath);
        }

        var filePath = Path.Combine(uploadsFolderPath, file.FileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return filePath;
    }

    public string ReturnFilePath(string relativeDirectory, string fileName)
    {
        var request = _httpContextAccessor.HttpContext.Request;
        return $"{request.Scheme}://{request.Host}/{ relativeDirectory }/{ fileName }";

    }

}
