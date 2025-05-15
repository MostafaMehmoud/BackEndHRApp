using HrApp.BL.Services.IServices;
using HrApp.DAL.Dtos;
using HrApp.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HrApp.Api.Controllers
{
    public class KafilController : AppControllerBase
    {
        private readonly IKafilService _KafilService;
        public KafilController(IKafilService KafilService)
        {
            _KafilService = KafilService;
        }
        [HttpPost("GetAllKafils")]
        [SwaggerOperation(Summary = "عرض جميع كفلاء", OperationId = "GetAllKafils")]
        public async Task<ApiResponse<List<Kafil>>> GetAllKafils()
        {
            return await _KafilService.GetAll();
        }


        [HttpPost("GetKafilById")]
        [SwaggerOperation(Summary = "عرض كفيل عن طريق ID", OperationId = "GetKafilById")]
        public async Task<ApiResponse<Kafil>> GetKafilById([FromQuery] string id)
        {
            return await _KafilService.GetById(id);
        }


        [HttpPost("AddKafil")]
        [SwaggerOperation(Summary = "إضافة كفيل جديدة", OperationId = "AddKafil")]
        public async Task<ApiResponse<Kafil>> AddKafil([FromBody] CreateKafil model)
        {
            return await _KafilService.Add(model);
        }


        [HttpPut("UpdateKafil")]
        [SwaggerOperation(Summary = "تعديل بيانات كفيل", OperationId = "UpdateKafil")]
        public async Task<ApiResponse<Kafil>> UpdateKafil([FromBody] UpdateKafil model)
        {
            return await _KafilService.Update(model);
        }


        [HttpDelete("DeleteKafil")]  // استخدم DELETE بدلاً من POST
        [SwaggerOperation(Summary = "حذف كفيل", OperationId = "DeleteKafil")]
        public async Task<ApiResponse<Kafil>> DeleteKafil([FromQuery] string id)
        {
            return await _KafilService.Delete(id);
        }
    }
}
