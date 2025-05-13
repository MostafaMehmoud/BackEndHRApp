using HrApp.BL.Services.IServices;
using HrApp.DAL.Dtos;
using HrApp.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HrApp.Api.Controllers
{
    public class NeighborController : AppControllerBase
    {
        private readonly INeighborService _NeighborService;
        public NeighborController(INeighborService NeighborService)
        {
            _NeighborService = NeighborService;
        }
        [HttpPost("GetAllNeighbors")]
        [SwaggerOperation(Summary = "عرض جميع القرابات", OperationId = "GetAllNeighbors")]
        public async Task<ApiResponse<List<Neighbor>>> GetAllNeighbors()
        {
            return await _NeighborService.GetAll();
        }


        [HttpPost("GetNeighborById")]
        [SwaggerOperation(Summary = "عرض القرابة عن طريق ID", OperationId = "GetNeighborById")]
        public async Task<ApiResponse<Neighbor>> GetNeighborById([FromQuery] string id)
        {
            return await _NeighborService.GetById(id);
        }


        [HttpPost("AddNeighbor")]
        [SwaggerOperation(Summary = "إضافة القرابة جديدة", OperationId = "AddNeighbor")]
        public async Task<ApiResponse<Neighbor>> AddNeighbor([FromBody] CreateNeighbor model)
        {
            return await _NeighborService.Add(model);
        }


        [HttpPut("UpdateNeighbor")]
        [SwaggerOperation(Summary = "تعديل بيانات القرابة", OperationId = "UpdateNeighbor")]
        public async Task<ApiResponse<Neighbor>> UpdateNeighbor([FromBody] UpdateNeighbor model)
        {
            return await _NeighborService.Update(model);
        }


        [HttpDelete("DeleteNeighbor")]  // استخدم DELETE بدلاً من POST
        [SwaggerOperation(Summary = "حذف القرابة", OperationId = "DeleteNeighbor")]
        public async Task<ApiResponse<Neighbor>> DeleteNeighbor([FromQuery] string id)
        {
            return await _NeighborService.Delete(id);
        }
    }
}
