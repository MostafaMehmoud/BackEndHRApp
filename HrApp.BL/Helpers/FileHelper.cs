
using HrApp.DAL.Entities;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace HrApp.BL.Helpers;

public class FileHelper
{
    public static UploadFile CreateUploadFile(IFormFile formFile)
    {
        var result = new UploadFile();
        try
        {
            result.Id = Guid.NewGuid().ToString();
            result.FileName = formFile.FileName;
            result.FileType = formFile.ContentType;
            using MemoryStream stream = new() ;      
            formFile.CopyTo(stream);           
            result.FileData = stream.ToArray();
        }
        catch (Exception)
        {
            throw new AppException("Error Occure when Upload File");
        }
        return result;
    }

    public async static Task DownloadFileById(UploadFile file)
    {
        try
        {
            var content = new MemoryStream(file.FileData);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "FileDownloaded",file.FileName);
            using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write);
            await content.CopyToAsync(fileStream);
        }
        catch (Exception)
        {
            throw new AppException("Error Occure when download File");
        }
    }

    private async Task CopyStream(Stream stream, string downloadPath)
    {
        using FileStream fileStream = new(downloadPath, FileMode.Create, FileAccess.Write);
        await stream.CopyToAsync(fileStream);
    }

}
