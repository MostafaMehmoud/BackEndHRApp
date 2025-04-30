using HrApp.DAL.Entities;

namespace HrApp.BL.Services.IServices;

public interface IUploadFileService
{
    Task<UploadFile> GetFileById(string fileId);
}
