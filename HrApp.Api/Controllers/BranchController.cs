using HrApp.Api.Attributes;
using HrApp.BL.Services;
using HrApp.BL.Services.IServices;
using HrApp.DAL.Dtos;
using HrApp.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HrApp.Api.Controllers.Admin;
public class BranchController : AppControllerBase
{
    private readonly IBranchService _branchService;

    public BranchController(IBranchService branchService)
    {
        _branchService = branchService;
    }

    [Authorize()]
    [HttpPost("GetAllBranch")]
    [SwaggerOperation(Summary = "عرض جميع الافرع", OperationId = "GetAllBranch")]

    public async Task<ActionResult<List<ModelOutputDto>>> GetAllBranchAsync()
    {
        var result = await _branchService.GetAllBranchAsync();
        return Ok(result);
    }
    [HttpPost("GetAllBranches")]
    [SwaggerOperation(Summary = "عرض جميع الافرع", OperationId = "GetAllBranches")]
    public async Task<ApiResponse<List<Branch>>> GetAllBranches()
    {
        return await _branchService.GetAll();
    }
    [HttpPost("GetAllBranchesWithCompany")]
    [SwaggerOperation(Summary = "عرض جميع الافرع مع الشركات", OperationId = "GetAllBranchesWithCompany")]
    public ApiResponse<IEnumerable<Branch>> GetAllBranchesWithCompany()
    {
        return  _branchService.GetAllInculdeBranches();
    }

    [HttpPost("GetBranchById")]
    [SwaggerOperation(Summary = "عرض الفرع عن طريق ID", OperationId = "GetBranchById")]
    public async Task<ApiResponse<Branch>> GetBranchById([FromQuery] string id)
    {
        return await _branchService.GetById(id);
    }


    [HttpPost("AddBranch")]
    [SwaggerOperation(Summary = "إضافة الفرع جديدة", OperationId = "AddBranch")]
    public async Task<ApiResponse<Branch>> AddBranch([FromBody] CreateBranch model)
    {
        return await _branchService.Add(model);
    }


    [HttpPut("UpdateBranch")]
    [SwaggerOperation(Summary = "تعديل بيانات الفرع", OperationId = "UpdateBranch")]
    public async Task<ApiResponse<Branch>> UpdateBranch([FromBody] UpdateBranch model)
    {
        return await _branchService.Update(model);
    }


    [HttpDelete("DeleteBranch")]  // استخدم DELETE بدلاً من POST
    [SwaggerOperation(Summary = "حذف الفرع", OperationId = "DeleteBranch")]
    public async Task<ApiResponse<Branch>> DeleteBranch([FromQuery] string id)
    {
        return await _branchService.Delete(id);
    }

}


