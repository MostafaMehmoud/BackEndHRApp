using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrApp.DAL.Dtos;
using HrApp.DAL.Entities;

namespace HrApp.BL.Services.IServices
{
    public interface IReligionService
    {
        Task<ApiResponse<Religion>> Add(CreateReligion createReligion);
        Task<ApiResponse<Religion>> Update(UpdateReligion updateReligion);
        Task<ApiResponse<Religion>> Delete(string id);
        Task<ApiResponse<Religion>> GetById(string id);
        Task<ApiResponse<List<Religion>>> GetAll();
    }
}
