using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrApp.BL.Services.IServices;
using HrApp.DAL.Dtos;
using HrApp.DAL.Entities;
using HrApp.DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HrApp.BL.Services
{
    public class CollegeService : ICollegeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CollegeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<College>> Add(CreateCollege createCollege)
        {
            try
            {
                var College = new College
                {
                    Id = ""
                   ,
                    NameAr = createCollege.CollegeNameAr,
                    NameEn = createCollege.CollegeNameEn
                };

                await _unitOfWork.CollegeRepository.AddWithSPAsync(College);

                return new ApiResponse<College>
                {
                    Success = true,
                    Message = "College added successfully.",
                    Data = College,
                    Errors = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<College>
                {
                    Success = false,
                    Message = "Failed to add College.",
                    Data = null,
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponse<College>> Delete(string id)
        {
            try
            {
                var existing = await _unitOfWork.CollegeRepository.GetByIdTypeStringAsync(id);
                if (existing == null)
                {
                    return new ApiResponse<College>
                    {
                        Success = false,
                        Message = "College not found.",
                        Data = null,
                        Errors = new List<string> { "Invalid ID" }
                    };
                }

                await _unitOfWork.CollegeRepository.DeleteAsync(existing);

                return new ApiResponse<College>
                {
                    Success = true,
                    Message = "College deleted successfully.",
                    Data = null,
                    Errors = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<College>
                {
                    Success = false,
                    Message = "Failed to deleted College.",
                    Data = null,
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponse<List<College>>> GetAll()
        {
            var Colleges = await _unitOfWork.CollegeRepository.GetTableNoTracking().ToListAsync();

            return new ApiResponse<List<College>>
            {
                Success = true,
                Message = "Colleges retrieved successfully.",
                Data = Colleges,
                Errors = null
            };
        }

        public async Task<ApiResponse<College>> GetById(string id)
        {
            var College = await _unitOfWork.CollegeRepository.GetByIdTypeStringAsync(id);

            if (College == null)
            {
                return new ApiResponse<College>
                {
                    Success = false,
                    Message = "College not found.",
                    Data = null,
                    Errors = new List<string> { "Invalid ID" }
                };
            }

            return new ApiResponse<College>
            {
                Success = true,
                Message = "College retrieved successfully.",
                Data = College,
                Errors = null
            };
        }

        public async Task<ApiResponse<College>> Update(UpdateCollege updateCollege)
        {
            try
            {
                var College = await _unitOfWork.CollegeRepository.GetByIdTypeStringAsync(updateCollege.Id);

                if (College == null)
                {
                    return new ApiResponse<College>
                    {
                        Success = false,
                        Message = "College not found.",
                        Data = null,
                        Errors = new List<string> { "Invalid ID" }
                    };
                }

                College.NameAr = updateCollege.CollegeNameAr;
                College.NameEn = updateCollege.CollegeNameEn;

                await _unitOfWork.CollegeRepository.UpdateWithSPAsync(College);

                return new ApiResponse<College>
                {
                    Success = true,
                    Message = "College updated successfully.",
                    Data = College,
                    Errors = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<College>
                {
                    Success = false,
                    Message = "Failed to updated College.",
                    Data = null,
                    Errors = new List<string> { ex.Message }
                };
            }

        }
    }
}
