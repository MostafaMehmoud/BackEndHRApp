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
    public class NationService : INationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public NationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<Nation>> Add(CreateNation createNation)
        {
            try
            {
                var Nation = new Nation
                {
                    Id = ""
                   ,
                    NameAr = createNation.NationNameAr,
                    NameEn = createNation.NationNameEn
                };

                await _unitOfWork.NationRepository.AddWithSPAsync(Nation);

                return new ApiResponse<Nation>
                {
                    Success = true,
                    Message = "Nation added successfully.",
                    Data = Nation,
                    Errors = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Nation>
                {
                    Success = false,
                    Message = "Failed to add Nation.",
                    Data = null,
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponse<Nation>> Delete(string id)
        {
            try
            {
                var existing = await _unitOfWork.NationRepository.GetByIdTypeStringAsync(id);
                if (existing == null)
                {
                    return new ApiResponse<Nation>
                    {
                        Success = false,
                        Message = "Nation not found.",
                        Data = null,
                        Errors = new List<string> { "Invalid ID" }
                    };
                }

                await _unitOfWork.NationRepository.DeleteAsync(existing);

                return new ApiResponse<Nation>
                {
                    Success = true,
                    Message = "Nation deleted successfully.",
                    Data = null,
                    Errors = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Nation>
                {
                    Success = false,
                    Message = "Failed to deleted Nation.",
                    Data = null,
                    Errors = new List<string> { ex.Message }
                };
            }

        }

        public async Task<ApiResponse<List<Nation>>> GetAll()
        {
            var Nations = await _unitOfWork.NationRepository.GetTableNoTracking().ToListAsync();

            return new ApiResponse<List<Nation>>
            {
                Success = true,
                Message = "Nations retrieved successfully.",
                Data = Nations,
                Errors = null
            };
        }

        public async Task<ApiResponse<Nation>> GetById(string id)
        {
            var Nation = await _unitOfWork.NationRepository.GetByIdTypeStringAsync(id);

            if (Nation == null)
            {
                return new ApiResponse<Nation>
                {
                    Success = false,
                    Message = "Nation not found.",
                    Data = null,
                    Errors = new List<string> { "Invalid ID" }
                };
            }

            return new ApiResponse<Nation>
            {
                Success = true,
                Message = "Nation retrieved successfully.",
                Data = Nation,
                Errors = null
            };
        }

        public async Task<ApiResponse<Nation>> Update(UpdateNation updateNation)
        {
            try
            {
                var Nation = await _unitOfWork.NationRepository.GetByIdTypeStringAsync(updateNation.Id);

                if (Nation == null)
                {
                    return new ApiResponse<Nation>
                    {
                        Success = false,
                        Message = "Nation not found.",
                        Data = null,
                        Errors = new List<string> { "Invalid ID" }
                    };
                }

                Nation.NameAr = updateNation.NationNameAr;
                Nation.NameEn = updateNation.NationNameEn;

                await _unitOfWork.NationRepository.UpdateWithSPAsync(Nation);

                return new ApiResponse<Nation>
                {
                    Success = true,
                    Message = "Nation updated successfully.",
                    Data = Nation,
                    Errors = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Nation>
                {
                    Success = false,
                    Message = "Failed to updated Nation.",
                    Data = null,
                    Errors = new List<string> { ex.Message }
                };
            }
        }
    }
}
