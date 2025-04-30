using HrApp.Api.Attributes;
using HrApp.BL.Services.IServices;
using HrApp.DAL.Dtos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HrApp.Api.Controllers.Auth;

public class AuthController : AppControllerBase
{
    private readonly IAuthenticationService _authService;
    public AuthController(IAuthenticationService authService)
    {
        _authService = authService;
    }


    [AllowAnonymous]
    [HttpPost("SignIn")]
    [SwaggerOperation(Summary = "تسجيل الدخول بواسطه اسم المستخدم او رقم الموظف", OperationId = "SignIn")]
    public async Task<ActionResult<SignInOutputDto>> SignIn([FromForm] SignInInputDto dto)
    {
        
        var result = await _authService.SignIn(dto);
        return Ok(result);
    }
    [Authorize()]
    [HttpPost("SignOut")]
    [SwaggerOperation(Summary = "تسجيل الخروج", OperationId = "SignOut")]
    public async Task<ActionResult<bool>> SignOut()
    {
        var empId = HttpContext.Items["EmployeeId"]?.ToString();
        if (empId == null) return Unauthorized();


        var result = await _authService.SignOut(empId);
        return Ok(result);
    }
}
