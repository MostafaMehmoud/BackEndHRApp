
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrApp.DAL.Dtos;
using HrApp.DAL.Entities;

namespace HrApp.BL.Services.IServices
{
    public interface IJobService
    {
        Task<ApiResponse<Job>> Add(CreateJob createJob);
        Task<ApiResponse<Job>> Update(UpdateJob updateJob);
        Task<ApiResponse<Job>> Delete(string id);
        Task<ApiResponse<Job>> GetById(string id);
        Task<ApiResponse<List<Job>>> GetAll();
    }
}
