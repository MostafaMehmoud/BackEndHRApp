
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrApp.DAL.Dtos;
using HrApp.DAL.Entities;

namespace HrApp.BL.Services.IServices
{
    public interface IKafilService
    {
        Task<ApiResponse<Kafil>> Add(CreateKafil createKafil);
        Task<ApiResponse<Kafil>> Update(UpdateKafil updateKafil);
        Task<ApiResponse<Kafil>> Delete(string id);
        Task<ApiResponse<Kafil>> GetById(string id);
        Task<ApiResponse<List<Kafil>>> GetAll();
    }
}
