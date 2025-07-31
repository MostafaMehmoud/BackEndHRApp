
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
    public class ReligionService : IReligionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReligionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;   
        }
        public async Task<ApiResponse<Religion>> Add(CreateReligion createReligion)
        {
            try
            {
                var Religion = new Religion
                {
                    Id = "*"
                   ,
                    NameAr = createReligion.ReligionNameAr,
                    NameEn = createReligion.ReligionNameEn
                };

                await _unitOfWork.ReligionRepository.AddWithSPAsync(Religion);

                return new ApiResponse<Religion>
                {
                    Success = true,
                    Message = "Religion added successfully.",
                    Data = Religion,
                    Errors = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Religion>
                {
                    Success = false,
                    Message = "Failed to add Religion.",
                    Data = null,
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponse<Religion>> Delete(string id)
        {
            try
            {
                var existing = await _unitOfWork.ReligionRepository.GetByIdTypeStringAsync(id);
                if (existing == null)
                {
                    return new ApiResponse<Religion>
                    {
                        Success = false,
                        Message = "Religion not found.",
                        Data = null,
                        Errors = new List<string> { "Invalid ID" }
                    };
                }

                await _unitOfWork.ReligionRepository.DeleteAsync(existing);

                return new ApiResponse<Religion>
                {
                    Success = true,
                    Message = "Religion deleted successfully.",
                    Data = null,
                    Errors = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Religion>
                {
                    Success = false,
                    Message = "Failed to deleted Religion.",
                    Data = null,
                    Errors = new List<string> { ex.Message }
                };
            }

        }

        public async Task<ApiResponse<List<Religion>>> GetAll()
        {
            var Religions = await _unitOfWork.ReligionRepository.GetTableNoTracking().ToListAsync();

            return new ApiResponse<List<Religion>>
            {
                Success = true,
                Message = "Religions retrieved successfully.",
                Data = Religions,
                Errors = null
            };
        }

        public async Task<ApiResponse<Religion>> GetById(string id)
        {
            var Religion = await _unitOfWork.ReligionRepository.GetByIdTypeStringAsync(id);

            if (Religion == null)
            {
                return new ApiResponse<Religion>
                {
                    Success = false,
                    Message = "Religion not found.",
                    Data = null,
                    Errors = new List<string> { "Invalid ID" }
                };
            }

            return new ApiResponse<Religion>
            {
                Success = true,
                Message = "Religion retrieved successfully.",
                Data = Religion,
                Errors = null
            };
        }

        public async  Task<ApiResponse<Religion>> Update(UpdateReligion updateReligion)
        {
            try
            {
                var Religion = await _unitOfWork.ReligionRepository.GetByIdTypeStringAsync(updateReligion.Id);

                if (Religion == null)
                {
                    return new ApiResponse<Religion>
                    {
                        Success = false,
                        Message = "Religion not found.",
                        Data = null,
                        Errors = new List<string> { "Invalid ID" }
                    };
                }

                Religion.NameAr = updateReligion.ReligionNameAr;
                Religion.NameEn = updateReligion.ReligionNameEn;

                await _unitOfWork.ReligionRepository.UpdateWithSPAsync(Religion);

                return new ApiResponse<Religion>
                {
                    Success = true,
                    Message = "Religion updated successfully.",
                    Data = Religion,
                    Errors = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Religion>
                {
                    Success = false,
                    Message = "Failed to updated Religion.",
                    Data = null,
                    Errors = new List<string> { ex.Message }
                };
            }
        }
    }
}
