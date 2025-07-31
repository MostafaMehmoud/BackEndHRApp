using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrApp.DAL.Dtos;
using HrApp.DAL.Entities;

namespace HrApp.BL.Services.IServices
{
    public interface IDepartmentService
    {
        Task<ApiResponse<Department>> Add(CreateDepartment createDepartment);
        Task<ApiResponse<Department>> Update(UpdateDepartment updateDepartment);
        Task<ApiResponse<Department>> Delete(string id);
        Task<ApiResponse<Department>> GetById(string id);
        Task<ApiResponse<List<Department>>> GetAll();
        ApiResponse<IEnumerable<Department>> GetAllInculdeDepartmentes();
    }
}
