using HrApp.BL.Services.IServices;
using HrApp.DAL.Dtos;
using HrApp.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HrApp.Api.Controllers
{
    public class NationController : AppControllerBase
    {
        private readonly INationService _NationService;

        public NationController(INationService NationService)
        {
            _NationService = NationService;
        }



        [HttpPost("GetAllNations")]
        [SwaggerOperation(Summary = "عرض جميع الجنسيات", OperationId = "GetAllNations")]
        public async Task<ApiResponse<List<Nation>>> GetAllNations()
        {
            return await _NationService.GetAll();
        }


        [HttpPost("GetNationById")]
        [SwaggerOperation(Summary = "عرض الجنسية عن طريق ID", OperationId = "GetNationById")]
        public async Task<ApiResponse<Nation>> GetNationById([FromQuery] string id)
        {
            return await _NationService.GetById(id);
        }


        [HttpPost("AddNation")]
        [SwaggerOperation(Summary = "إضافة الجنسية جديدة", OperationId = "AddNation")]
        public async Task<ApiResponse<Nation>> AddNation([FromBody] CreateNation model)
        {
            return await _NationService.Add(model);
        }


        [HttpPut("UpdateNation")]
        [SwaggerOperation(Summary = "تعديل بيانات الجنسية", OperationId = "UpdateNation")]
        public async Task<ApiResponse<Nation>> UpdateNation([FromBody] UpdateNation model)
        {
            return await _NationService.Update(model);
        }


        [HttpDelete("DeleteNation")]  // استخدم DELETE بدلاً من POST
        [SwaggerOperation(Summary = "حذف الجنسية", OperationId = "DeleteNation")]
        public async Task<ApiResponse<Nation>> DeleteNation([FromQuery] string id)
        {
            return await _NationService.Delete(id);
        }

    }
}
