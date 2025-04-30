using HrApp.DAL.Dtos;

namespace HrApp.BL.Services.IServices;

public interface IManageService
{
    Task<List<ModelOutputDto>> GetAllManageAsync();
}
