using HrApp.BL.Services.IServices;
using HrApp.DAL.Dtos;
using HrApp.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HrApp.Api.Controllers
{
    public class CompanyController : AppControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        
        [HttpPost("GetAllCompanies")]
        [SwaggerOperation(Summary = "عرض جميع الشركات", OperationId = "GetAllCompanies")]
        public async Task<ApiResponse<List<Company>>> GetAllCompanies()
        {
            return await _companyService.GetAll();
        }

       
        [HttpPost("GetCompanyById")]
        [SwaggerOperation(Summary = "عرض شركة عن طريق ID", OperationId = "GetCompanyById")]
        public async Task<ApiResponse<Company>> GetCompanyById([FromQuery] string id)
        {
            return await _companyService.GetById(id);
        }

        
        [HttpPost("AddCompany")]
        [SwaggerOperation(Summary = "إضافة شركة جديدة", OperationId = "AddCompany")]
        public async Task<ApiResponse<Company>> AddCompany([FromBody] CreateCompany model)
        {
            return await _companyService.Add(model);
        }

       
        [HttpPut("UpdateCompany")]
        [SwaggerOperation(Summary = "تعديل بيانات شركة", OperationId = "UpdateCompany")]
        public async Task<ApiResponse<Company>> UpdateCompany([FromBody] UpdateCompany model)
        {
            return await _companyService.Update(model);
        }

       
        [HttpDelete("DeleteCompany")]  // استخدم DELETE بدلاً من POST
        [SwaggerOperation(Summary = "حذف شركة", OperationId = "DeleteCompany")]
        public async Task<ApiResponse<Company>> DeleteCompany([FromQuery] string id)
        {
            return await _companyService.Delete(id);
        }

    }
}
