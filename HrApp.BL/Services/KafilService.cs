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
    public class KafilService : IKafilService
    {
        private readonly IUnitOfWork _unitOfWork;
        public KafilService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<Kafil>> Add(CreateKafil createKafil)
        {
            try
            {
                var Kafil = new Kafil
                {
                    Id = "*"
                   ,
                    NameAr = createKafil.KafilNameAr,
                    NameEn = createKafil.KafilNameEn,
                    Mobile=createKafil.Mobile,
                    IdNumber=createKafil.IdNumber,
                };

                await _unitOfWork.KafilRepository.AddWithSPAsync(Kafil);

                return new ApiResponse<Kafil>
                {
                    Success = true,
                    Message = "Kafil added successfully.",
                    Data = Kafil,
                    Errors = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Kafil>
                {
                    Success = false,
                    Message = "Failed to add Kafil.",
                    Data = null,
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponse<Kafil>> Delete(string id)
        {
            try
            {
                var existing = await _unitOfWork.KafilRepository.GetByIdTypeStringAsync(id);
                if (existing == null)
                {
                    return new ApiResponse<Kafil>
                    {
                        Success = false,
                        Message = "Kafil not found.",
                        Data = null,
                        Errors = new List<string> { "Invalid ID" }
                    };
                }

                await _unitOfWork.KafilRepository.DeleteAsync(existing);

                return new ApiResponse<Kafil>
                {
                    Success = true,
                    Message = "Kafil deleted successfully.",
                    Data = null,
                    Errors = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Kafil>
                {
                    Success = false,
                    Message = "Failed to deleted Kafil.",
                    Data = null,
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponse<List<Kafil>>> GetAll()
        {
            var Kafils = await _unitOfWork.KafilRepository.GetTableNoTracking().ToListAsync();

            return new ApiResponse<List<Kafil>>
            {
                Success = true,
                Message = "Kafils retrieved successfully.",
                Data = Kafils,
                Errors = null
            };
        }

        public async Task<ApiResponse<Kafil>> GetById(string id)
        {
            var Kafil = await _unitOfWork.KafilRepository.GetByIdTypeStringAsync(id);

            if (Kafil == null)
            {
                return new ApiResponse<Kafil>
                {
                    Success = false,
                    Message = "Kafil not found.",
                    Data = null,
                    Errors = new List<string> { "Invalid ID" }
                };
            }

            return new ApiResponse<Kafil>
            {
                Success = true,
                Message = "Kafil retrieved successfully.",
                Data = Kafil,
                Errors = null
            };
        }

        public async Task<ApiResponse<Kafil>> Update(UpdateKafil updateKafil)
        {
            try
            {
                var Kafil = await _unitOfWork.KafilRepository.GetByIdTypeStringAsync(updateKafil.Id);

                if (Kafil == null)
                {
                    return new ApiResponse<Kafil>
                    {
                        Success = false,
                        Message = "Kafil not found.",
                        Data = null,
                        Errors = new List<string> { "Invalid ID" }
                    };
                }

                Kafil.NameAr = updateKafil.KafilNameAr;
                Kafil.NameEn = updateKafil.KafilNameEn;
                Kafil.IdNumber = updateKafil.IdNumber;  
                Kafil.Mobile= updateKafil.Mobile;   

                await _unitOfWork.KafilRepository.UpdateWithSPAsync(Kafil);

                return new ApiResponse<Kafil>
                {
                    Success = true,
                    Message = "Kafil updated successfully.",
                    Data = Kafil,
                    Errors = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Kafil>
                {
                    Success = false,
                    Message = "Failed to updated Kafil.",
                    Data = null,
                    Errors = new List<string> { ex.Message }
                };
            }

        }
    }
}
