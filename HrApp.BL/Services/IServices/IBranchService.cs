

using HrApp.DAL.Dtos;

namespace HrApp.BL.Services.IServices;

public interface IBranchService
{
    Task<List<ModelOutputDto>> GetAllBranchAsync();
}
