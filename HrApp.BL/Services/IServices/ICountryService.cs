using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrApp.DAL.Dtos;
using HrApp.DAL.Entities;

namespace HrApp.BL.Services.IServices
{
    public interface ICountryService
    {
        Task<ApiResponse<Country>> Add(CreateCountry createCountry);
        Task<ApiResponse<Country>> Update(UpdateCountry updateCountry);
        Task<ApiResponse<Country>> Delete(string id);
        Task<ApiResponse<Country>> GetById(string id);
        Task<ApiResponse<List<Country>>> GetAll();
       
    }
}
