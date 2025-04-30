using HrApp.Api.Attributes;

using HrApp.API.Helpers;
using HrApp.BL.Helpers;
using HrApp.BL.Services.IServices;
using HrApp.DAL.Dtos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HrApp.Api.Controllers.Employee;
public class EmployeeController : AppControllerBase
{
    private readonly IVacationService _vacationService;
    private readonly IAdvancePaymentService _advancePaymentService;
    private readonly ILetterService _letterService;
    private readonly ICustodyService _custodyService;
    private readonly IPermissionService _PermissionService;
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IVacationService requestVacationService, IAdvancePaymentService requestAdvancePaymentService,
        ILetterService letterService, ICustodyService custodyService, IPermissionService permissionService, IEmployeeService employeeService)
    {
        _vacationService = requestVacationService;
        _advancePaymentService = requestAdvancePaymentService;
        _letterService = letterService;
        _custodyService = custodyService;
        _PermissionService = permissionService;
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
    [HttpGet("GetSearch")]
    [SwaggerOperation(Summary = "بحث", OperationId = "GetSearch")]
    public async Task<ActionResult<List<GetEmployeeSearchOutputDto>>> GetSearch(string? searchKey)
    {
        var empId = HttpContext.Items["EmployeeId"]?.ToString();
        if (empId == null) return Unauthorized();

        var res = await _employeeService.EmployeeSearchAsync(searchKey);
        return Ok(res);
    }
    [Authorize()]
    [HttpGet("GetEmployeeDetailsInfo")]
    [SwaggerOperation(Summary = "معلومات الموظف", OperationId = "GetEmployeeDetailsInfo")]
    public async Task<ActionResult<GetEmployeeDetailsOutputDto>> GetEmployeeDetailsInfo()
    {
        var empId = HttpContext.Items["EmployeeId"]?.ToString();
        if (empId == null) return Unauthorized();

        var res = await _employeeService.GetEmployeeDetailsAsync(empId);
        return Ok(res);
    }
    [Authorize()]
    [HttpPost("UploadEmployeeImage")]
    [SwaggerOperation(Summary = "أضافة صورة الموظف", OperationId = "UploadEmployeeImage")]
    public async Task<ActionResult<bool>> UploadEmployeeImage(IFormFile image)
    {
        var empId = HttpContext.Items["EmployeeId"]?.ToString();
        if (empId == null) return Unauthorized();

        var res = await _employeeService.UploadEmployeeImageAsync(empId,image);
        return Ok(res);
    }
    #endregion


    #region Vacation
    [Authorize()]
    [HttpGet("GetBasicVacationInfo")]
    [SwaggerOperation(Summary = "معلومات طلب الاجازه", OperationId = "GetBasicVacationInfo")]
    public async Task<ActionResult<BasicVacationInfoOutputDto>> GetBasicVacationInfo()
    {
        var empId = HttpContext.Items["EmployeeId"]?.ToString();
        if (empId == null) return Unauthorized();

        var res = await _vacationService.GetBasicVacationInfo(empId);
        return Ok(res);
    }

    [Authorize()]
    [HttpPost("RequestVacation")]
    [SwaggerOperation(Summary = "طلب أجازة ", OperationId = "RequestVacation")]
    public async Task<ActionResult<bool>> RequestVacation([FromForm] CreateRequestVacationInputDto dto)
    {
        var empId = HttpContext.Items["EmployeeId"]?.ToString();
        if(empId ==null) return Unauthorized();

        var res = await _vacationService.CreateRequestVacationAsync(dto, empId);
        return Ok(res);
    }

    [Authorize()]
    [HttpGet("GetEmployeeVacations")]
    [SwaggerOperation(Summary = "طلبات الأجازات للموظف ", OperationId = "GetEmployeeVacations")]

    public async Task<ActionResult<GetEmployeeVacationsOutputDto>> GetEmployeeVacations()
    {
        var empId = HttpContext.Items["EmployeeId"]?.ToString();
        if (empId == null) return Unauthorized();

        var res = await _vacationService.GetEmployeeVacationsAsync(empId);
        return Ok(res);
    }
    #endregion

    #region AdvancePayment
    [Authorize()]
    [HttpGet("GetBasicAdvancePaymentInfo")]
    [SwaggerOperation(Summary = "معلومات طلب السلفة", OperationId = "GetBasicAdvancePaymentInfo")]
    public async Task<ActionResult<BasicAdvancePaymentInfoOutputDto>> GetBasicAdvancePaymentInfo()
    {
        var empId = HttpContext.Items["EmployeeId"]?.ToString();
        if (empId == null) return Unauthorized();

        var res = await _advancePaymentService.GetBasicAdvancePaymentInfo(empId);
        return Ok(res);
    }

    [Authorize()]
    [HttpPost("RequestAdvancePayment")]
    [SwaggerOperation(Summary = "طلب سلفة ", OperationId = "RequestAdvancePayment")]

    public async Task<ActionResult<bool>> CreateRequestAdvancePayment([FromBody] CreateRequestAdvancePaymentInputDto dto)
    {
        var empId = HttpContext.Items["EmployeeId"]?.ToString();
        if (empId == null) return Unauthorized();

        if ((dto.InstallmentValue * (dto.NumberOfInstallment - 1))
            + dto.LastInstallmentValue != dto.AdvancePaymentValue)
            throw new AppException("Installments must be equle Advance Payment Value");


        var res = await _advancePaymentService.CreateRequestAdvancePaymentAsync(dto, empId);
        return Ok(res);
    }

    [Authorize()]
    [HttpGet("GetEmployeeAdvancePayments")]
    [SwaggerOperation(Summary = "طلبات السلف للموظف ", OperationId = "GetEmployeeAdvancePayments")]

    public async Task<ActionResult<GetEmployeeAdvancePaymentsOutputDto>> GetEmployeeAdvancePayments()
    {
        var empId = HttpContext.Items["EmployeeId"]?.ToString();
        if (empId == null) return Unauthorized();

        var res = await _advancePaymentService.GetEmployeeAdvancePaymentsAsync(empId);
        return Ok(res);
    }
    #endregion

    #region Custody
    [Authorize()]
    [HttpGet("GetBasicCustodyInfo")]
    [SwaggerOperation(Summary = "معلومات طلب عهدة", OperationId = "GetBasicCustodyInfo")]
    public async Task<ActionResult<BasicCustodyInfoOutputDto>> GetBasicCustodyInfo()
    {
        var empId = HttpContext.Items["EmployeeId"]?.ToString();
        if (empId == null) return Unauthorized();

        var res = await _custodyService.GetBasicCustodyInfo(empId);
        return Ok(res);
    }

    [Authorize()]
    [HttpPost("RequestCustody")]
    [SwaggerOperation(Summary = "طلب عهدة ", OperationId = "RequestCustody")]

    public async Task<ActionResult<bool>> CreateRequestCustody([FromForm] CreateRequestCustodyInputDto dto)
    {
        var empId = HttpContext.Items["EmployeeId"]?.ToString();
        if (empId == null) return Unauthorized();

        var res = await _custodyService.CreateCustodyAsync(dto, empId);
        return Ok(res);
    }

    [Authorize()]
    [HttpGet("GetEmployeeCustodies")]
    [SwaggerOperation(Summary = "طلبات العهد للموظف ", OperationId = "GetEmployeeCustodies")]

    public async Task<ActionResult<GetEmployeeCustodiesOutputDto>> GetEmployeeCustodies()
    {
        var empId = HttpContext.Items["EmployeeId"]?.ToString();
        if (empId == null) return Unauthorized();

        var res = await _custodyService.GetEmployeeCustodiesAsync(empId);
        return Ok(res);
    }
    #endregion

    #region Permission
    [Authorize()]
    [HttpGet("GetBasicPermissionInfo")]
    [SwaggerOperation(Summary = "معلومات طلب أذن", OperationId = "GetBasicPermissionInfo")]
    public async Task<ActionResult<BasicPermissionInfoOutputDto>> GetBasicPermissionInfo()
    {
        var empId = HttpContext.Items["EmployeeId"]?.ToString();
        if (empId == null) return Unauthorized();

        var res = await _PermissionService.GetBasicPermissionInfo(empId);
        return Ok(res);
    }

    [Authorize()]
    [HttpPost("RequestPermission")]
    [SwaggerOperation(Summary = "طلب أذن ", OperationId = "RequestPermission")]

    public async Task<ActionResult<bool>> CreateRequestPermission([FromForm] CreateRequestPermissionInputDto dto)
    {
        var empId = HttpContext.Items["EmployeeId"]?.ToString();
        if (empId == null) return Unauthorized();
        if (dto.StartDate >= dto.EndDate) throw new AppException("Start DateTime must be before End DateTime");
        var res = await _PermissionService.CreateRequestPermissionAsync(dto, empId);
        return Ok(res);
    }

    [Authorize()]
    [HttpGet("GetEmployeePermissions")]
    [SwaggerOperation(Summary = "طلبات الأذنات للموظف ", OperationId = "GetEmployeePermissions")]

    public async Task<ActionResult<GetEmployeePermissionsOutputDto>> GetEmployeePermissions()
    {
        var empId = HttpContext.Items["EmployeeId"]?.ToString();
        if (empId == null) return Unauthorized();

        var res = await _PermissionService.GetEmployeePermissionsAsync(empId);
        return Ok(res);
    }
    #endregion

    #region Letter
    [Authorize()]
    [HttpGet("GetBasicLetterInfo")]
    [SwaggerOperation(Summary = "معلومات طلب خطاب", OperationId = "GetBasicLetterInfo")]
    public async Task<ActionResult<BasicLetterInfoOutputDto>> GetBasicLetterInfo()
    {
        var empId = HttpContext.Items["EmployeeId"]?.ToString();
        if (empId == null) return Unauthorized();

        var res = await _letterService.GetBasicLetterInfo(empId);
        return Ok(res);
    }

    [Authorize()]
    [HttpPost("RequestLetter")]
    [SwaggerOperation(Summary = "طلب خطاب ", OperationId = "RequestLetter")]

    public async Task<ActionResult<bool>> CreateRequestLetter([FromForm] CreateRequestLetterInputDto dto)
    {
        var empId = HttpContext.Items["EmployeeId"]?.ToString();
        if (empId == null) return Unauthorized();

        var res = await _letterService.CreateRequestLetterAsync(dto, empId);
        return Ok(res);
    }

    [Authorize()]
    [HttpGet("GetEmployeeLetter")]
    [SwaggerOperation(Summary = "طلبات الخطابات للموظف ", OperationId = "GetEmployeeLetter")]

    public async Task<ActionResult<GetEmployeeLettersOutputDto>> GetEmployeeLetter()
    {
        var empId = HttpContext.Items["EmployeeId"]?.ToString();
        if (empId == null) return Unauthorized();

        var res = await _letterService.GetEmployeeLettersAsync(empId);
        return Ok(res);
    }
    #endregion
}


