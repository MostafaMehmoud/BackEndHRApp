using HrApp.BL.Services;
using HrApp.BL.Services.IServices;
using HrApp.DAL.Dtos;
using HrApp.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HrApp.Api.Controllers
{
    public class DepartmentController : AppControllerBase
    {
        private readonly IDepartmentService _DepartmentService;
        public DepartmentController(IDepartmentService DepartmentService)
        {
            _DepartmentService = DepartmentService;
        }
        [HttpPost("GetAllDepartments")]

        [SwaggerOperation(Summary = "عرض جميع الأقسام", OperationId = "GetAllDepartments")]

        public async Task<ApiResponse<List<Department>>> GetAllDepartmentes()
        {
            return await _DepartmentService.GetAll();
        }
        [HttpPost("GetAllDepartmentsWithManage")]
        [SwaggerOperation(Summary = "عرض جميع الأقسام مع الادرات", OperationId = "GetAllDepartmentsWithManage")]
        public ApiResponse<IEnumerable<Department>> GetAllDepartmentsWithManage()
        {
            return _DepartmentService.GetAllInculdeDepartmentes();
        }

       

        [HttpGet("GetDepartmentById")]
        [SwaggerOperation(Summary = "عرض القسم عن طريق ID", OperationId = "GetDepartmentById")]
        public async Task<ApiResponse<Department>> GetDepartmentById([FromQuery] string id)
        {
            return await _DepartmentService.GetById(id);
        }

        [HttpPost("AddDepartment")]
        [SwaggerOperation(Summary = "إضافة قسم جديد", OperationId = "AddDepartment")]
        public async Task<ApiResponse<Department>> AddDepartment([FromBody] CreateDepartment model)
        {
            return await _DepartmentService.Add(model);
        }

        [HttpPut("UpdateDepartment")]
        [SwaggerOperation(Summary = "تعديل بيانات القسم", OperationId = "UpdateDepartment")]
        public async Task<ApiResponse<Department>> UpdateDepartment([FromBody] UpdateDepartment model)
        {
            return await _DepartmentService.Update(model);
        }

        [HttpDelete("DeleteDepartment")]
        [SwaggerOperation(Summary = "حذف القسم", OperationId = "DeleteDepartment")]
        public async Task<ApiResponse<Department>> DeleteDepartment([FromQuery] string id)
        {
            return await _DepartmentService.Delete(id);
        }
    }
}