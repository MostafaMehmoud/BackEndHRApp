using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrApp.DAL.Dtos;
using HrApp.DAL.Entities;

namespace HrApp.BL.Services.IServices
{
    public interface ICompanyService
    {
        Task<ApiResponse<Company>> Add(CreateCompany createCompany);
        Task<ApiResponse<Company>> Update(UpdateCompany updateCompany);
        Task<ApiResponse<Company>> Delete(string id);
        Task<ApiResponse<Company>> GetById(string id);
        Task<ApiResponse<List<Company>>> GetAll();
    }
}
