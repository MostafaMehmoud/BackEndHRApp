using HrApp.Api.Attributes;
using HrApp.BL.Services.IServices;
using HrApp.DAL.Dtos;
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

}


