
using HrApp.DAL.Dtos;

namespace HrApp.BL.Services.IServices;

public interface IPermissionService
{
    Task<bool> CreateRequestPermissionAsync(CreateRequestPermissionInputDto dto, string empId);
    Task<GetPindingPermissionsOutputDto> GetPermissionPindingListAsync(InputDto dto);
    Task<bool> DecisionOfPermissionAsync(DecisionInputDto dto);
    Task<GetEmployeePermissionsOutputDto> GetEmployeePermissionsAsync(string empId);
    Task<BasicPermissionInfoOutputDto> GetBasicPermissionInfo(string empId);
}
