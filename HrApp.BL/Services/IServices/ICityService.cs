using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrApp.DAL.Dtos;
using HrApp.DAL.Entities;

namespace HrApp.BL.Services.IServices
{
    public interface ICityService
    {
        Task<ApiResponse<City>> Add(CreateCity createCity);
        Task<ApiResponse<City>> Update(UpdateCity updateCity);
        Task<ApiResponse<City>> Delete(string id);
        Task<ApiResponse<City>> GetById(string id);
        Task<ApiResponse<List<City>>> GetAll();
        ApiResponse<IEnumerable<City>> GetAllInculdeCityes();
    }
}
