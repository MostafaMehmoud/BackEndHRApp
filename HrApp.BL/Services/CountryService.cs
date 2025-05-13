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
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CountryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<Country>> Add(CreateCountry createCountry)
        {
            try
            {
                var Country = new Country
                {
                    Id = "*"
                   ,
                    NameAr = createCountry.NameArCountry,
                    NameEn = createCountry.NameEnCountry,
                    VATValue=createCountry.VATValue,
                    Currency=createCountry.Currency,
                    ExchangeRate = createCountry.ExchangeRate,
                   CurrencyId = createCountry.CurrencyId,   
                };

                await _unitOfWork.CountryRepository.AddWithSPAsync(Country);

                return new ApiResponse<Country>
                {
                    Success = true,
                    Message = "Country added successfully.",
                    Data = Country,
                    Errors = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Country>
                {
                    Success = false,
                    Message = "Failed to add Country.",
                    Data = null,
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponse<Country>> Delete(string id)
        {
            try
            {
                var existing = await _unitOfWork.CountryRepository.GetByIdTypeStringAsync(id);
                if (existing == null)
                {
                    return new ApiResponse<Country>
                    {
                        Success = false,
                        Message = "Country not found.",
                        Data = null,
                        Errors = new List<string> { "Invalid ID" }
                    };
                }

                await _unitOfWork.CountryRepository.DeleteAsync(existing);

                return new ApiResponse<Country>
                {
                    Success = true,
                    Message = "Country deleted successfully.",
                    Data = null,
                    Errors = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Country>
                {
                    Success = false,
                    Message = "Failed to deleted Country.",
                    Data = null,
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponse<List<Country>>> GetAll()
        {
            var Countries = await _unitOfWork.CountryRepository.GetTableNoTracking().ToListAsync();

            return new ApiResponse<List<Country>>
            {
                Success = true,
                Message = "Countries retrieved successfully.",
                Data = Countries,
                Errors = null
            };
        }

       
        public async Task<ApiResponse<Country>> GetById(string id)
        {
            var Country = await _unitOfWork.CountryRepository.GetByIdTypeStringAsync(id);

            if (Country == null)
            {
                return new ApiResponse<Country>
                {
                    Success = false,
                    Message = "Country not found.",
                    Data = null,
                    Errors = new List<string> { "Invalid ID" }
                };
            }

            return new ApiResponse<Country>
            {
                Success = true,
                Message = "Country retrieved successfully.",
                Data = Country,
                Errors = null
            };
        }

        public async Task<ApiResponse<Country>> Update(UpdateCountry updateCountry)
        {
            try
            {
                var Country = await _unitOfWork.CountryRepository.GetByIdTypeStringAsync(updateCountry.Id);

                if (Country == null)
                {
                    return new ApiResponse<Country>
                    {
                        Success = false,
                        Message = "Country not found.",
                        Data = null,
                        Errors = new List<string> { "Invalid ID" }
                    };
                }

                Country.NameAr = updateCountry.NameArCountry;
                Country.NameEn = updateCountry.NameEnCountry;
                Country.Id = updateCountry.Id;
                Country.VATValue = updateCountry.VATValue;
                Country.Currency = updateCountry.Currency;
                Country.ExchangeRate = updateCountry.ExchangeRate;
                Country.CurrencyId = updateCountry.CurrencyId;  

                await _unitOfWork.CountryRepository.UpdateWithSPAsync(Country);

                return new ApiResponse<Country>
                {
                    Success = true,
                    Message = "Country updated successfully.",
                    Data = Country,
                    Errors = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Country>
                {
                    Success = false,
                    Message = "Failed to updated Country.",
                    Data = null,
                    Errors = new List<string> { ex.Message }
                };
            }
        }
    }
}
