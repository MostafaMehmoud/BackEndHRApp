using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrApp.DAL.Dtos;
using HrApp.DAL.Entities;

namespace HrApp.BL.Services.IServices
{
    public interface ICollegeService
    {
        Task<ApiResponse<College>> Add(CreateCollege createCollege);
        Task<ApiResponse<College>> Update(UpdateCollege updateCollege);
        Task<ApiResponse<College>> Delete(string id);
        Task<ApiResponse<College>> GetById(string id);
        Task<ApiResponse<List<College>>> GetAll();
    }
}
