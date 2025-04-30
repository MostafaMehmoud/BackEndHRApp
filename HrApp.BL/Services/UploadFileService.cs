using HrApp.API.Helpers;
using HrApp.BL.Services.IServices;
using HrApp.DAL.Entities;
using HrApp.DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;


namespace HrApp.BL.Services;

public class UploadFileService : IUploadFileService
{
    private readonly IUnitOfWork _unitOfWork;

    public UploadFileService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<UploadFile> GetFileById(string fileId)
    {
        var res = await _unitOfWork.UploadFileRepository.GetTableNoTracking().Where(m => m.Id.ToLower() == fileId.ToLower()).FirstOrDefaultAsync();
        if (res == null)
            throw new ApplicationException("File Not Found");

        return res;
    }
}
