using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HrApp.BL.Services.IServices;
using HrApp.DAL.Data;
using HrApp.DAL.Dtos;
using HrApp.DAL.Entities;
using HrApp.DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace HrApp.BL.Services
{
    public class CityService: ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CityService( IUnitOfWork unitOfWork)
        {
            
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<City>> Add(CreateCity createCity)
        {
            try
            {
                var City = new City
                {
                    Id = "*"
                   ,
                    NameAr = createCity.CityNameAr,
                    NameEn = createCity.CityNameEn,
                    CompanyId = createCity.CompanyId
                };

                await _unitOfWork.CityRepository.AddWithSPAsync(City);

                return new ApiResponse<City>
                {
                    Success = true,
                    Message = "City added successfully.",
                    Data = City,
                    Errors = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<City>
                {
                    Success = false,
                    Message = "Failed to add City.",
                    Data = null,
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponse<City>> Delete(string id)
        {
            try
            {
                var existing = await _unitOfWork.CityRepository.GetByIdTypeStringAsync(id);
                if (existing == null)
                {
                    return new ApiResponse<City>
                    {
                        Success = false,
                        Message = "City not found.",
                        Data = null,
                        Errors = new List<string> { "Invalid ID" }
                    };
                }

                await _unitOfWork.CityRepository.DeleteAsync(existing);

                return new ApiResponse<City>
                {
                    Success = true,
                    Message = "City deleted successfully.",
                    Data = null,
                    Errors = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<City>
                {
                    Success = false,
                    Message = "Failed to deleted City.",
                    Data = null,
                    Errors = new List<string> { ex.Message }
                };
            }

        }

        public async Task<ApiResponse<List<City>>> GetAll()
        {
            var Cities = await _unitOfWork.CityRepository.GetTableNoTracking().ToListAsync();

            return new ApiResponse<List<City>>
            {
                Success = true,
                Message = "Cities retrieved successfully.",
                Data = Cities,
                Errors = null
            };
        }

        public async Task<ApiResponse<City>> GetById(string id)
        {
            var City = await _unitOfWork.CityRepository.GetByIdTypeStringAsync(id);

            if (City == null)
            {
                return new ApiResponse<City>
                {
                    Success = false,
                    Message = "City not found.",
                    Data = null,
                    Errors = new List<string> { "Invalid ID" }
                };
            }

            return new ApiResponse<City>
            {
                Success = true,
                Message = "City retrieved successfully.",
                Data = City,
                Errors = null
            };
        }

        public async Task<ApiResponse<City>> Update(UpdateCity updateCity)
        {
            try
            {
                var City = await _unitOfWork.CityRepository.GetByIdTypeStringAsync(updateCity.Id);

                if (City == null)
                {
                    return new ApiResponse<City>
                    {
                        Success = false,
                        Message = "City not found.",
                        Data = null,
                        Errors = new List<string> { "Invalid ID" }
                    };
                }

                City.NameAr = updateCity.CityNameAr;
                City.NameEn = updateCity.CityNameEn;
                City.CompanyId = updateCity.CompanyId;

                await _unitOfWork.CityRepository.UpdateWithSPAsync(City);

                return new ApiResponse<City>
                {
                    Success = true,
                    Message = "City updated successfully.",
                    Data = City,
                    Errors = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<City>
                {
                    Success = false,
                    Message = "Failed to updated City.",
                    Data = null,
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public ApiResponse<IEnumerable<City>> GetAllInculdeCityes()
        {
            var Cities = _unitOfWork.CityRepository.GetAllInclude();

            return new ApiResponse<IEnumerable<City>>
            {
                Success = true,
                Message = "Cityes retrieved successfully.",
                Data = Cities,
                Errors = null
            };
        }
    }
}
