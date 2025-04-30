using HrApp.Api.Attributes;
using HrApp.BL.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HrApp.Api.Controllers.Admin;
public class FileController : AppControllerBase
{
    private readonly IUploadFileService _uploadFileService;

    public FileController(IUploadFileService uploadFileService)
    {
        _uploadFileService = uploadFileService;
    }

    [Authorize()]
    [HttpGet("GetFile/{fileId}")]
    [SwaggerOperation(Summary = "تحميل الملف", OperationId = "GetFile")]
    public async Task<IActionResult> GetFile([FromRoute]string fileId)
    {
       var res = await _uploadFileService.GetFileById(fileId);
        return new FileContentResult(res.FileData,res.FileType);

    }
}


