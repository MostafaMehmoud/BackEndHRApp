using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrApp.DAL.Dtos;
using HrApp.DAL.Entities;

namespace HrApp.BL.Services.IServices
{
    public interface INationService
    {
        Task<ApiResponse<Nation>> Add(CreateNation createNation);
        Task<ApiResponse<Nation>> Update(UpdateNation updateNation);
        Task<ApiResponse<Nation>> Delete(string id);
        Task<ApiResponse<Nation>> GetById(string id);
        Task<ApiResponse<List<Nation>>> GetAll();
    }
}
