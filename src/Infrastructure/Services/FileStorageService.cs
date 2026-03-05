using Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Hosting;

namespace Infrastructure.Services;

public class FileStorageService(IWebHostEnvironment environment) : IFileStorageService
{
    private readonly string _uploadFolder = "uploads/product-images";

    public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var filePath = Path.Combine(environment.WebRootPath, _uploadFolder, fileName);
            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await fileStream.CopyToAsync(stream, cancellationToken);
            }

            return $"/{_uploadFolder}/{fileName}";
        }
        catch (Exception e)
        {
            return string.Empty;
        }
    }

    public Task<bool> DeleteFileAsync(string fileUrl, CancellationToken cancellationToken = default)
    {
        try
        {
            var fileName = Path.GetFileName(fileUrl);
            var filePath = Path.Combine(environment.WebRootPath, _uploadFolder, fileName);

            if (!File.Exists(filePath)) 
                return Task.FromResult(false);
            
            File.Delete(filePath);
            return Task.FromResult(false);
        }
        catch (Exception e)
        {
            return Task.FromResult(false);
        }
    }

    public Task<bool> FileExistsAsync(string fileUrl, CancellationToken cancellationToken = default)
    {
        try
        {
            var fileName = Path.GetFileName(fileUrl);
            var filePath = Path.Combine(environment.WebRootPath, _uploadFolder, fileName);
            return Task.FromResult(File.Exists(filePath));
        }
        catch (Exception e)
        {
            return Task.FromResult(false);
        }
    }
}