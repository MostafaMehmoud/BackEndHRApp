
using HrApp.BL.Services.IServices;
using HrApp.DAL.Dtos;
using HrApp.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HrApp.Api.Controllers
{
   
    public class ReligionController : AppControllerBase
    {
        private readonly IReligionService _ReligionService;

        public ReligionController(IReligionService ReligionService)
        {
            _ReligionService = ReligionService;
        }

        
        
        [HttpPost("GetAllReligions")]
        [SwaggerOperation(Summary = "عرض جميع الديانات", OperationId = "GetAllReligions")]
        public async Task<ApiResponse<List<Religion>>> GetAllReligions()
        {
            return await _ReligionService.GetAll();
        }


        [HttpPost("GetReligionById")]
        [SwaggerOperation(Summary = "عرض الديانة عن طريق ID", OperationId = "GetReligionById")]
        public async Task<ApiResponse<Religion>> GetReligionById([FromQuery] string id)
        {
            return await _ReligionService.GetById(id);
        }


        [HttpPost("AddReligion")]
        [SwaggerOperation(Summary = "إضافة الديانة جديدة", OperationId = "AddReligion")]
        public async Task<ApiResponse<Religion>> AddReligion([FromBody] CreateReligion model)
        {
            return await _ReligionService.Add(model);
        }


        [HttpPut("UpdateReligion")]
        [SwaggerOperation(Summary = "تعديل بيانات الديانة", OperationId = "UpdateReligion")]
        public async Task<ApiResponse<Religion>> UpdateReligion([FromBody] UpdateReligion model)
        {
            return await _ReligionService.Update(model);
        }


        [HttpDelete("DeleteReligion")]  // استخدم DELETE بدلاً من POST
        [SwaggerOperation(Summary = "حذف الديانة", OperationId = "DeleteReligion")]
        public async Task<ApiResponse<Religion>> DeleteReligion([FromQuery] string id)
        {
            return await _ReligionService.Delete(id);
        }

    }
}
