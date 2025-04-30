using HrApp.Api.Attributes;
using HrApp.BL.Services.IServices;
using HrApp.DAL.Dtos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HrApp.Api.Controllers.Admin;
public class ManageController : AppControllerBase
{
    private readonly IManageService _manageService;

    public ManageController(IManageService manageService)
    {
        _manageService = manageService;
    }

    [Authorize()]
    [HttpPost("GetAllManage")]
    [SwaggerOperation(Summary = "عرض جميع الادارات", OperationId = "GetAllManage")]

    public async Task<ActionResult<List<ModelOutputDto>>> GetAllManageAsync()
    {
        var result = await _manageService.GetAllManageAsync();
        return Ok(result);
    }

}


