

using HrApp.DAL.Dtos;

namespace HrApp.BL.Services.IServices;

public interface IEmployeeAttendanceService
{
    Task<GetEmployeeAttendanceOutputDto> GetEmployeeAttendance(GetEmployeeAttendanceInputDto dto);
    Task<bool> InsertEmployeeAttendances(InsertEmployeeAttendanceInputDto dto, string empId);
}
