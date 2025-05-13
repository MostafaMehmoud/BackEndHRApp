
using HrApp.BL.Services.IServices;
using HrApp.DAL.Dtos;
using HrApp.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HrApp.Api.Controllers
{
    public class CollegeController : AppControllerBase
    {
        private readonly ICollegeService _CollegeService;
        public CollegeController(ICollegeService CollegeService)
        {
            _CollegeService = CollegeService;
        }
        [HttpPost("GetAllColleges")]
        [SwaggerOperation(Summary = "عرض جميع الوظائف", OperationId = "GetAllColleges")]
        public async Task<ApiResponse<List<College>>> GetAllColleges()
        {
            return await _CollegeService.GetAll();
        }


        [HttpPost("GetCollegeById")]
        [SwaggerOperation(Summary = "عرض المؤهل عن طريق ID", OperationId = "GetCollegeById")]
        public async Task<ApiResponse<College>> GetCollegeById([FromQuery] string id)
        {
            return await _CollegeService.GetById(id);
        }


        [HttpPost("AddCollege")]
        [SwaggerOperation(Summary = "إضافة المؤهل جديدة", OperationId = "AddCollege")]
        public async Task<ApiResponse<College>> AddCollege([FromBody] CreateCollege model)
        {
            return await _CollegeService.Add(model);
        }


        [HttpPut("UpdateCollege")]
        [SwaggerOperation(Summary = "تعديل بيانات المؤهل", OperationId = "UpdateCollege")]
        public async Task<ApiResponse<College>> UpdateCollege([FromBody] UpdateCollege model)
        {
            return await _CollegeService.Update(model);
        }


        [HttpDelete("DeleteCollege")]  // استخدم DELETE بدلاً من POST
        [SwaggerOperation(Summary = "حذف المؤهل", OperationId = "DeleteCollege")]
        public async Task<ApiResponse<College>> DeleteCollege([FromQuery] string id)
        {
            return await _CollegeService.Delete(id);
        }
    }
}
