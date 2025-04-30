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
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        public async Task<ApiResponse<Company>> Add(CreateCompany createCompany)
        {
            try
            {
                var company = new Company
                {
                    Id=""
                   , NameAr = createCompany.CompanyNameAr,
                    NameEn = createCompany.CompanyNameEn
                };

                await _companyRepository.AddWithSPAsync(company);

                return new ApiResponse<Company>
                {
                    Success = true,
                    Message = "Company added successfully.",
                    Data = company,
                    Errors = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Company>
                {
                    Success = false,
                    Message = "Failed to add company.",
                    Data = null,
                    Errors = new List<string> { ex.Message }
                };
            }
        }


        public async Task<ApiResponse<Company>> Delete(string id)
        {
            try
            {
                var existing = await _companyRepository.GetByIdTypeStringAsync(id);
                if (existing == null)
                {
                    return new ApiResponse<Company>
                    {
                        Success = false,
                        Message = "Company not found.",
                        Data = null,
                        Errors = new List<string> { "Invalid ID" }
                    };
                }

                await _companyRepository.DeleteAsync(existing);

                return new ApiResponse<Company>
                {
                    Success = true,
                    Message = "Company deleted successfully.",
                    Data = null,
                    Errors = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Company>
                {
                    Success = false,
                    Message = "Failed to deleted company.",
                    Data = null,
                    Errors = new List<string> { ex.Message }
                };
            }


        }

        public async Task<ApiResponse<List<Company>>> GetAll()
        {
            var companies = await _companyRepository.GetTableNoTracking().ToListAsync();

            return new ApiResponse<List<Company>>
            {
                Success = true,
                Message = "Companies retrieved successfully.",
                Data = companies,
                Errors = null
            };
        }

        public async Task<ApiResponse<Company>> GetById(string id)
        {
            
            var company = await _companyRepository.GetByIdTypeStringAsync(id);

            if (company == null)
            {
                return new ApiResponse<Company>
                {
                    Success = false,
                    Message = "Company not found.",
                    Data = null,
                    Errors = new List<string> { "Invalid ID" }
                };
            }

            return new ApiResponse<Company>
            {
                Success = true,
                Message = "Company retrieved successfully.",
                Data = company,
                Errors = null
            };
        }

        public async Task<ApiResponse<Company>> Update(UpdateCompany updateCompany)
        {

            try
            {
                var company = await _companyRepository.GetByIdTypeStringAsync(updateCompany.Id);

                if (company == null)
                {
                    return new ApiResponse<Company>
                    {
                        Success = false,
                        Message = "Company not found.",
                        Data = null,
                        Errors = new List<string> { "Invalid ID" }
                    };
                }

                company.NameAr = updateCompany.CompanyNameAr;
                company.NameEn = updateCompany.CompanyNameEn;

                await _companyRepository.UpdateWithSPAsync(company);

                return new ApiResponse<Company>
                {
                    Success = true,
                    Message = "Company updated successfully.",
                    Data = company,
                    Errors = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Company>
                {
                    Success = false,
                    Message = "Failed to updated company.",
                    Data = null,
                    Errors = new List<string> { ex.Message }
                };
            }


        }

    }
}
