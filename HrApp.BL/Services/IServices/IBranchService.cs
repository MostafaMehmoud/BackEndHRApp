

using HrApp.DAL.Dtos;
using HrApp.DAL.Entities;

namespace HrApp.BL.Services.IServices;

public interface IBranchService
{
    Task<List<ModelOutputDto>> GetAllBranchAsync();
    Task<ApiResponse<Branch>> Add(CreateBranch createBranch);
    Task<ApiResponse<Branch>> Update(UpdateBranch updateBranch);
    Task<ApiResponse<Branch>> Delete(string id);
    Task<ApiResponse<Branch>> GetById(string id);
    Task<ApiResponse<List<Branch>>> GetAll();
   ApiResponse<IEnumerable<Branch>> GetAllInculdeBranches();
}
