using HrApp.BL.Services.IServices;
using HrApp.DAL.Dtos;
using HrApp.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HrApp.Api.Controllers
{
    public class CountryController : AppControllerBase
    {
        private readonly ICountryService _CountryService;
        public CountryController(ICountryService CountryService)
        {
            _CountryService = CountryService;
        }
        [HttpPost("GetAllCountries")]
        [SwaggerOperation(Summary = "عرض جميع البلاد", OperationId = "GetAllCounties")]
        public async Task<ApiResponse<List<Country>>> GetAllCounties()
        {
            return await _CountryService.GetAll();
        }


        [HttpPost("GetCountryById")]
        [SwaggerOperation(Summary = "عرض البلد عن طريق ID", OperationId = "GetCountryById")]
        public async Task<ApiResponse<Country>> GetCountryById([FromQuery] string id)
        {
            return await _CountryService.GetById(id);
        }


        [HttpPost("AddCountry")]
        [SwaggerOperation(Summary = "إضافة البلد جديدة", OperationId = "AddCountry")]
        public async Task<ApiResponse<Country>> AddCountry([FromBody] CreateCountry model)
        {
            return await _CountryService.Add(model);
        }


        [HttpPut("UpdateCountry")]
        [SwaggerOperation(Summary = "تعديل بيانات البلد", OperationId = "UpdateCountry")]
        public async Task<ApiResponse<Country>> UpdateCountry([FromBody] UpdateCountry model)
        {
            return await _CountryService.Update(model);
        }


        [HttpDelete("DeleteCountry")]  // استخدم DELETE بدلاً من POST
        [SwaggerOperation(Summary = "حذف البلد", OperationId = "DeleteCountry")]
        public async Task<ApiResponse<Country>> DeleteCountry([FromQuery] string id)
        {
            return await _CountryService.Delete(id);
        }
    }
}
