using HrApp.Api.Attributes;

using HrApp.BL.Services.IServices;
using HrApp.DAL.Dtos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HrApp.Api.Controllers;

public class EmployeeAttendanceController : AppControllerBase
{
    private readonly IEmployeeAttendanceService _employeeAttendanceService;

    public EmployeeAttendanceController(IEmployeeAttendanceService employeeAttendanceService)
    {
        _employeeAttendanceService = employeeAttendanceService;
    }

    [Authorize()]
    [HttpPost("GetEmployeeAttendance")]
    [SwaggerOperation(Summary = "حضور الموظفين", OperationId = "GetEmployeeAttendance")]
    public async Task<ActionResult<GetEmployeeDetailsOutputDto>> GetEmployeeAttendance(GetEmployeeAttendanceInputDto dto)
    {
        var res = await _employeeAttendanceService.GetEmployeeAttendance(dto);
        return Ok(res);
    }

    [Authorize()]
    [HttpPost("InsertEmployeeAttendance")]
    [SwaggerOperation(Summary = "تسجيل حضور الموظفين", OperationId = "InsertEmployeeAttendance")]
    public async Task<ActionResult<GetEmployeeDetailsOutputDto>> InsertEmployeeAttendances(InsertEmployeeAttendanceInputDto dto)
    {
        var empId = HttpContext.Items["EmployeeId"]?.ToString();
        if (empId == null) return Unauthorized();

        var res = await _employeeAttendanceService.InsertEmployeeAttendances(dto, empId);
        return Ok(res);
    }
}    
