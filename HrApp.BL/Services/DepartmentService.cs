using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HrApp.BL.Services.IServices;
using HrApp.DAL.Dtos;
using HrApp.DAL.Entities;
using HrApp.DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HrApp.BL.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<Department>> Add(CreateDepartment createDepartment)
        {
            try
            {
                var department = new Department
                {
                    Id = "*",
                    NameAr = createDepartment.DepartmentNameAr,
                    NameEn = createDepartment.DepartmentNameEn,
                    ManagerId = createDepartment.ManageId,
                };

                await _unitOfWork.DepartmentRepository.AddWithSPAsync(department);

                return new ApiResponse<Department>
                {
                    Success = true,
                    Message = "Department added successfully.",
                    Data = department,
                    Errors = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Department>
                {
                    Success = false,
                    Message = "Failed to add department.",
                    Data = null,
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponse<Department>> Delete(string id)
        {
            try
            {
                var existing = await _unitOfWork.DepartmentRepository.GetByIdAsync(id);
                if (existing == null)
                {
                    return new ApiResponse<Department>
                    {
                        Success = false,
                        Message = "Department not found.",
                        Data = null,
                        Errors = new List<string> { "Invalid ID" }
                    };
                }

                await _unitOfWork.DepartmentRepository.DeleteAsync(existing);

                return new ApiResponse<Department>
                {
                    Success = true,
                    Message = "Department deleted successfully.",
                    Data = null,
                    Errors = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Department>
                {
                    Success = false,
                    Message = "Failed to delete department.",
                    Data = null,
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponse<List<Department>>> GetAll()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetTableNoTracking().ToListAsync();

            return new ApiResponse<List<Department>>
            {
                Success = true,
                Message = "Departments retrieved successfully.",
                Data = departments,
                Errors = null
            };
        }

        public async Task<ApiResponse<Department>> GetById(string id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id);

            if (department == null)
            {
                return new ApiResponse<Department>
                {
                    Success = false,
                    Message = "Department not found.",
                    Data = null,
                    Errors = new List<string> { "Invalid ID" }
                };
            }

            return new ApiResponse<Department>
            {
                Success = true,
                Message = "Department retrieved successfully.",
                Data = department,
                Errors = null
            };
        }

        public async Task<ApiResponse<Department>> Update(UpdateDepartment updateDepartment)
        {
            try
            {
                var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(updateDepartment.Id);

                if (department == null)
                {
                    return new ApiResponse<Department>
                    {
                        Success = false,
                        Message = "Department not found.",
                        Data = null,
                        Errors = new List<string> { "Invalid ID" }
                    };
                }

                department.NameAr = updateDepartment.DepartmentNameAr;
                department.NameEn = updateDepartment.DepartmentNameEn;
                department.ManagerId = updateDepartment.ManageId;

                await _unitOfWork.DepartmentRepository.UpdateWithSPAsync(department);

                return new ApiResponse<Department>
                {
                    Success = true,
                    Message = "Department updated successfully.",
                    Data = department,
                    Errors = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Department>
                {
                    Success = false,
                    Message = "Failed to update department.",
                    Data = null,
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public ApiResponse<IEnumerable<Department>> GetAllInculdeDepartmentes()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAllInclude();

            return new ApiResponse<IEnumerable<Department>>
            {
                Success = true,
                Message = "Departments retrieved successfully.",
                Data = departments,
                Errors = null
            };
        }
    }
}
