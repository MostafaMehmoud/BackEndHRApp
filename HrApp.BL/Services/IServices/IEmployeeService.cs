
using HrApp.DAL.Dtos;
using Microsoft.AspNetCore.Http;
namespace HrApp.BL.Services.IServices;

public interface IEmployeeService
{
    Task<EmployeeInfoOutputDto> GetEmployeeInfoByIdAsync(string employeeId);
    Task<GetEmployeeDetailsOutputDto> GetEmployeeDetailsAsync(string employeeId);
    Task<bool> UploadEmployeeImageAsync(string employeeId, IFormFile image);
    Task<string> GetEmployeeName(string employeeId);
    Task<GetEmployeeSearchOutputDto> EmployeeSearchAsync(string? searchKey);
    Task<GetEmployeesTreeOutputDto> GetEmployeeTreeAsync(GetEmployeesTreeInputDto dto);
    Task<GetEmployeeTreeByIdOutputDto> GetEmployeeTreeByIdAsync(string employeeId);


}
