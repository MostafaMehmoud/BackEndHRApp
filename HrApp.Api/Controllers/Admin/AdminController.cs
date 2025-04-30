using HrApp.Api.Attributes;
using HrApp.BL.Services.IServices;
using HrApp.DAL.Dtos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HrApp.Api.Controllers.Admin;
public class AdminController : AppControllerBase
{
    private readonly IVacationService _vacationService;
    private readonly IAdvancePaymentService _advancePaymentService;
    private readonly ILetterService _letterService;
    private readonly IPermissionService _permissionService;
    private readonly ICustodyService _custodyService;

    public AdminController(IVacationService requestVacationService,IAdvancePaymentService requestAdvancePaymentService,
        ILetterService letterService,IPermissionService permissionService,ICustodyService custodyService)
    {
        _vacationService = requestVacationService;
        _advancePaymentService = requestAdvancePaymentService;
        _letterService = letterService;
        _permissionService = permissionService;
        _custodyService = custodyService;
    }

    #region Vaction

    [Authorize(RoleEnum.Admin)]
    [HttpGet("GetPindingRequestVacations")]
    [SwaggerOperation(Summary = " عرض طلبات الأجازات المعلقة", OperationId = "GetPindingRequestVacations")]

    public async Task<ActionResult<GetPindingVacationsOutputDto>> GetPindingRequestVacations([FromQuery] InputDto dto)
    {
        var result = await _vacationService.GetRequestVacationPindingListAsync(dto);
        return Ok(result);
    }
    [Authorize(RoleEnum.Admin)]
    [HttpPost("DecisionOfVacation")]
    [SwaggerOperation(Summary = "قرار الاجازة", OperationId = "DecisionOfVacation")]

    public async Task<IActionResult> DecisionOfVacation([FromBody] DecisionInputDto dto)
    {
        var res = await _vacationService.DecisionOfVacationAsync(dto);
        return Ok(res);
    }
    #endregion

    #region AdvancePayment
    [Authorize(RoleEnum.Admin)]
    [HttpGet("GetPindingAdvancePayments")]
    [SwaggerOperation(Summary = "عرض طلبات السلف المعلقة", OperationId = "RequestPindingAdvancePayments")]

    public async Task<ActionResult<GetPindingAdvancePaymentsOutputDto>> RequestPindingAdvancePayment([FromQuery] InputDto dto)
    {
        var result = await _advancePaymentService.GetAdvancePaymentPindingListAsync(dto);
        return Ok(result);
    }

    

    [Authorize(RoleEnum.Admin)]
    [HttpPost("DecisionOfAdvancePayment")]
    [SwaggerOperation(Summary = "قرار طلب السلفة ", OperationId = "DecisionOfAdvancePayment")]

    public async Task<IActionResult> DecisionOfAdvancePayment([FromBody] DecisionAdvancePaymentInputDto dto)
    {
        var res = await _advancePaymentService.DecisionOfAdvancePaymentAsync(dto);
        return Ok(res);
    }
    #endregion

    #region Custody

    [Authorize(RoleEnum.Admin)]
    [HttpGet("GetPindingRequestCustodies")]
    [SwaggerOperation(Summary = " عرض طلبات العهد المعلقة", OperationId = "GetPindingRequestCustodies")]

    public async Task<ActionResult<GetPindingCustodiesOutputDto>> GetPindingRequestCustodies([FromQuery] InputDto dto)
    {
        var result = await _custodyService.GetCustodyPindingListAsync(dto);
        return Ok(result);
    }
    [Authorize(RoleEnum.Admin)]
    [HttpPost("DecisionOfCustody")]
    [SwaggerOperation(Summary = "قرار العهده", OperationId = "DecisionOfCustody")]

    public async Task<IActionResult> DecisionOfCustody([FromBody] DecisionInputDto dto)
    {
        var res = await _custodyService.DecisionOfCustodyAsync(dto);
        return Ok(res);
    }
    #endregion

    #region Permission

    [Authorize(RoleEnum.Admin)]
    [HttpGet("GetPindingRequestPermissions")]
    [SwaggerOperation(Summary = " عرض طلبات الاذنات المعلقة", OperationId = "GetPindingRequestPermissions")]

    public async Task<ActionResult<GetPindingPermissionsOutputDto>> GetPindingRequestPermissions([FromQuery] InputDto dto)
    {
        var result = await _permissionService.GetPermissionPindingListAsync(dto);
        return Ok(result);
    }
    [Authorize(RoleEnum.Admin)]
    [HttpPost("DecisionOfPermission")]
    [SwaggerOperation(Summary = "قرار الأذن", OperationId = "DecisionOfPermission")]

    public async Task<IActionResult> DecisionOfPermission([FromBody] DecisionInputDto dto)
    {
        var res = await _permissionService.DecisionOfPermissionAsync(dto);
        return Ok(res);
    }
    #endregion

    #region Letter

    [Authorize(RoleEnum.Admin)]
    [HttpGet("GetPindingRequestLetters")]
    [SwaggerOperation(Summary = " عرض طلبات الخطابات المعلقة", OperationId = "GetPindingRequestLetters")]

    public async Task<ActionResult<GetPindingLettersOutputDto>> GetPindingRequestLetters([FromQuery] InputDto dto)
    {
        var result = await _letterService.GetLetterPindingListAsync(dto);
        return Ok(result);
    }
    [Authorize(RoleEnum.Admin)]
    [HttpPost("DecisionOfLetter")]
    [SwaggerOperation(Summary = "قرار الخطاب", OperationId = "DecisionOfLetter")]

    public async Task<IActionResult> DecisionOfLetter([FromBody] DecisionInputDto dto)
    {
        var res = await _letterService.DecisionOfLetterAsync(dto);
        return Ok(res);
    }
    #endregion
}


