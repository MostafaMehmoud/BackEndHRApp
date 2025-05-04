using HrApp.BL.Services.IServices;
using HrApp.DAL.Dtos;
using HrApp.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HrApp.Api.Controllers
{
   
    public class JobController : AppControllerBase
    {
        private readonly IJobService _jobService;
        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }
        [HttpPost("GetAllJobs")]
        [SwaggerOperation(Summary = "عرض جميع الوظائف", OperationId = "GetAllJobs")]
        public async Task<ApiResponse<List<Job>>> GetAllJobs()
        {
            return await _jobService.GetAll();
        }


        [HttpPost("GetJobById")]
        [SwaggerOperation(Summary = "عرض الوظيفة عن طريق ID", OperationId = "GetJobById")]
        public async Task<ApiResponse<Job>> GetJobById([FromQuery] string id)
        {
            return await _jobService.GetById(id);
        }


        [HttpPost("AddJob")]
        [SwaggerOperation(Summary = "إضافة الوظيفة جديدة", OperationId = "AddJob")]
        public async Task<ApiResponse<Job>> AddJob([FromBody] CreateJob model)
        {
            return await _jobService.Add(model);
        }


        [HttpPut("UpdateJob")]
        [SwaggerOperation(Summary = "تعديل بيانات الوظيفة", OperationId = "UpdateJob")]
        public async Task<ApiResponse<Job>> UpdateJob([FromBody] UpdateJob model)
        {
            return await _jobService.Update(model);
        }


        [HttpDelete("DeleteJob")]  // استخدم DELETE بدلاً من POST
        [SwaggerOperation(Summary = "حذف الوظيفة", OperationId = "DeleteJob")]
        public async Task<ApiResponse<Job>> DeleteJob([FromQuery] string id)
        {
            return await _jobService.Delete(id);
        }
    }
}
