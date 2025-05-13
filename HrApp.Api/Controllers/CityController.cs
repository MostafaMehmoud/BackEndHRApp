using HrApp.BL.Services;
using HrApp.BL.Services.IServices;
using HrApp.DAL.Dtos;
using HrApp.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HrApp.Api.Controllers
{
    public class CityController : AppControllerBase
    {
        private readonly ICityService _cityService;
        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }
        [HttpPost("GetAllCities")]
        [SwaggerOperation(Summary = "عرض جميع المدن", OperationId = "GetAllCities")]
        public async Task<ApiResponse<List<City>>> GetAllCityes()
        {
            return await _cityService.GetAll();
        }
        [HttpPost("GetAllCitiesWithCompany")]
        [SwaggerOperation(Summary = "عرض جميع المدن مع الشركات", OperationId = "GetAllCiiyesWithCompany")]
        public ApiResponse<IEnumerable<City>> GetAllCitiesWithCompany()
        {
            return _cityService.GetAllInculdeCityes();
        }

        [HttpPost("GetCityById")]
        [SwaggerOperation(Summary = "عرض المدينة عن طريق ID", OperationId = "GetCityById")]
        public async Task<ApiResponse<City>> GetCityById([FromQuery] string id)
        {
            return await _cityService.GetById(id);
        }


        [HttpPost("AddCity")]
        [SwaggerOperation(Summary = "إضافة المدينة جديدة", OperationId = "AddCity")]
        public async Task<ApiResponse<City>> AddCity([FromBody] CreateCity model)
        {
            return await _cityService.Add(model);
        }


        [HttpPut("UpdateCity")]
        [SwaggerOperation(Summary = "تعديل بيانات المدينة", OperationId = "UpdateCity")]
        public async Task<ApiResponse<City>> UpdateCity([FromBody] UpdateCity model)
        {
            return await _cityService.Update(model);
        }


        [HttpDelete("DeleteCity")]  // استخدم DELETE بدلاً من POST
        [SwaggerOperation(Summary = "حذف المدينة", OperationId = "DeleteCity")]
        public async Task<ApiResponse<City>> DeleteCity([FromQuery] string id)
        {
            return await _cityService.Delete(id);
        }
    }
}
