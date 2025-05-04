using HrApp.Api.Attributes;
using HrApp.BL.Services;
using HrApp.BL.Services.IServices;
using HrApp.DAL.Dtos;
using HrApp.DAL.Entities;
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
    [HttpPost("GetAllManages")]
    [SwaggerOperation(Summary = "عرض جميع الادارات", OperationId = "GetAllManages")]
    public async Task<ApiResponse<List<Manage>>> GetAllManages()
    {
        return await _manageService.GetAll();
    }


    [HttpPost("GetManageById")]
    [SwaggerOperation(Summary = "عرض الادارة عن طريق ID", OperationId = "GetManageById")]
    public async Task<ApiResponse<Manage>> GetManageById([FromQuery] string id)
    {
        return await _manageService.GetById(id);
    }


    [HttpPost("AddManage")]
    [SwaggerOperation(Summary = "إضافة الادارة جديدة", OperationId = "AddManage")]
    public async Task<ApiResponse<Manage>> AddManage([FromBody] CreateManage model)
    {
        return await _manageService.Add(model);
    }


    [HttpPut("UpdateManage")]
    [SwaggerOperation(Summary = "تعديل بيانات الادارة", OperationId = "UpdateManage")]
    public async Task<ApiResponse<Manage>> UpdateManage([FromBody] UpdateManage model)
    {
        return await _manageService.Update(model);
    }


    [HttpDelete("DeleteManage")]  // استخدم DELETE بدلاً من POST
    [SwaggerOperation(Summary = "حذف الادارة", OperationId = "DeleteManage")]
    public async Task<ApiResponse<Manage>> DeleteManage([FromQuery] string id)
    {
        return await _manageService.Delete(id);
    }

}


