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
    public class NeighborService : INeighborService
    {
        private readonly IUnitOfWork _unitOfWork;
        public NeighborService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<Neighbor>> Add(CreateNeighbor createNeighbor)
        {
            try
            {
                var Neighbor = new Neighbor
                {
                    Id = ""
                   ,
                    NameAr = createNeighbor.NeighborNameAr,
                    NameEn = createNeighbor.NeighborNameEn
                };

                await _unitOfWork.NeighborRepository.AddWithSPAsync(Neighbor);

                return new ApiResponse<Neighbor>
                {
                    Success = true,
                    Message = "Neighbor added successfully.",
                    Data = Neighbor,
                    Errors = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Neighbor>
                {
                    Success = false,
                    Message = "Failed to add Neighbor.",
                    Data = null,
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponse<Neighbor>> Delete(string id)
        {
            try
            {
                var existing = await _unitOfWork.NeighborRepository.GetByIdTypeStringAsync(id);
                if (existing == null)
                {
                    return new ApiResponse<Neighbor>
                    {
                        Success = false,
                        Message = "Neighbor not found.",
                        Data = null,
                        Errors = new List<string> { "Invalid ID" }
                    };
                }

                await _unitOfWork.NeighborRepository.DeleteAsync(existing);

                return new ApiResponse<Neighbor>
                {
                    Success = true,
                    Message = "Neighbor deleted successfully.",
                    Data = null,
                    Errors = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Neighbor>
                {
                    Success = false,
                    Message = "Failed to deleted Neighbor.",
                    Data = null,
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponse<List<Neighbor>>> GetAll()
        {
            var Neighbors = await _unitOfWork.NeighborRepository.GetTableNoTracking().ToListAsync();

            return new ApiResponse<List<Neighbor>>
            {
                Success = true,
                Message = "Neighbors retrieved successfully.",
                Data = Neighbors,
                Errors = null
            };
        }

        public async Task<ApiResponse<Neighbor>> GetById(string id)
        {
            var Neighbor = await _unitOfWork.NeighborRepository.GetByIdTypeStringAsync(id);

            if (Neighbor == null)
            {
                return new ApiResponse<Neighbor>
                {
                    Success = false,
                    Message = "Neighbor not found.",
                    Data = null,
                    Errors = new List<string> { "Invalid ID" }
                };
            }

            return new ApiResponse<Neighbor>
            {
                Success = true,
                Message = "Neighbor retrieved successfully.",
                Data = Neighbor,
                Errors = null
            };
        }

        public async Task<ApiResponse<Neighbor>> Update(UpdateNeighbor updateNeighbor)
        {
            try
            {
                var Neighbor = await _unitOfWork.NeighborRepository.GetByIdTypeStringAsync(updateNeighbor.Id);

                if (Neighbor == null)
                {
                    return new ApiResponse<Neighbor>
                    {
                        Success = false,
                        Message = "Neighbor not found.",
                        Data = null,
                        Errors = new List<string> { "Invalid ID" }
                    };
                }

                Neighbor.NameAr = updateNeighbor.NeighborNameAr;
                Neighbor.NameEn = updateNeighbor.NeighborNameEn;

                await _unitOfWork.NeighborRepository.UpdateWithSPAsync(Neighbor);

                return new ApiResponse<Neighbor>
                {
                    Success = true,
                    Message = "Neighbor updated successfully.",
                    Data = Neighbor,
                    Errors = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Neighbor>
                {
                    Success = false,
                    Message = "Failed to updated Neighbor.",
                    Data = null,
                    Errors = new List<string> { ex.Message }
                };
            }

        }
    }
}
