using HrApp.DAL.Dtos;
using HrApp.DAL.Entities;

namespace HrApp.BL.Services.IServices;

public interface IManageService
{
    Task<List<ModelOutputDto>> GetAllManageAsync();
    Task<ApiResponse<Manage>> Add(CreateManage createManage);
    Task<ApiResponse<Manage>> Update(UpdateManage updateManage);
    Task<ApiResponse<Manage>> Delete(string id);
    Task<ApiResponse<Manage>> GetById(string id);
    Task<ApiResponse<List<Manage>>> GetAll();
}
