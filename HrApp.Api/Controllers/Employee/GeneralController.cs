using HrApp.Api.Attributes;


using HrApp.BL.Services.IServices;
using HrApp.DAL.Dtos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HrApp.Api.Controllers.Employee;
public class GeneralController : AppControllerBase
{
    private readonly IEmployeeService _employeeService;

    public GeneralController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }
    #region Employee Info

    [Authorize()]
    [HttpGet("GetEmployeeName")]
    [SwaggerOperation(Summary = "اسم الموظف", OperationId = "GetEmployeeName")]
    public async Task<ActionResult<string>> GetEmployeeName(string EmployeeId)
    {
        var empId = HttpContext.Items["EmployeeId"]?.ToString();
        if (empId == null) return Unauthorized();

        var res = await _employeeService.GetEmployeeName(EmployeeId);
        return Ok(res);
    }
    [Authorize()]
    [HttpGet("EmployeeSearch")]
    [SwaggerOperation(Summary = "بحث", OperationId = "EmployeeSearch")]
    public async Task<ActionResult<GetEmployeeSearchOutputDto>> EmployeeSearch(string? searchKey)
    {
        var empId = HttpContext.Items["EmployeeId"]?.ToString();
        if (empId == null) return Unauthorized();

        var res = await _employeeService.EmployeeSearchAsync(searchKey);
        return Ok(res);
    }
  
    #endregion

}


