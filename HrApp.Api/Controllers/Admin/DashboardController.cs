using HrApp.Api.Attributes;
using HrApp.BL.Services.IServices;
using HrApp.DAL.Dtos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HrApp.Api.Controllers.Admin;
public class DashboardController : AppControllerBase
{
    private readonly IEmployeeService _employeeService;

    public DashboardController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    #region Employee Tree

    [Authorize(RoleEnum.Admin)]
    [HttpPost("GetEmployeesTree")]
    [SwaggerOperation(Summary = " عرض الموظفين", OperationId = "GetEmployeesTree")]

    public async Task<ActionResult<GetEmployeesTreeOutputDto>> GetEmployeesTreeAsync([FromBody] GetEmployeesTreeInputDto dto)
    {
        var result = await _employeeService.GetEmployeeTreeAsync(dto);
        return Ok(result);
    }
    [Authorize(RoleEnum.Admin)]
    [HttpPost("GetEmployeeTreeById")]
    [SwaggerOperation(Summary = " عرض الموظف", OperationId = "GetEmployeeTreeById")]

    public async Task<ActionResult<GetEmployeeTreeByIdOutputDto>> GetEmployeeTreeByIdAsync([FromQuery]string employeeId)
    {
        var result = await _employeeService.GetEmployeeTreeByIdAsync(employeeId);
        return Ok(result);
    }
    #endregion


}


