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
    public class JobService : IJobService
    {
        private readonly IUnitOfWork _unitOfWork; 
        public JobService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;   
        }
        public async Task<ApiResponse<Job>> Add(CreateJob createJob)
        {
            try
            {
                var Job = new Job
                {
                    Id = ""
                   ,
                    NameAr = createJob.JobNameAr,
                    NameEn = createJob.JobNameEn
                };

                await _unitOfWork.JobRepository.AddWithSPAsync(Job);

                return new ApiResponse<Job>
                {
                    Success = true,
                    Message = "job added successfully.",
                    Data = Job,
                    Errors = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Job>
                {
                    Success = false,
                    Message = "Failed to add job.",
                    Data = null,
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponse<Job>> Delete(string id)
        {
            try
            {
                var existing = await _unitOfWork.JobRepository.GetByIdTypeStringAsync(id);
                if (existing == null)
                {
                    return new ApiResponse<Job>
                    {
                        Success = false,
                        Message = "Job not found.",
                        Data = null,
                        Errors = new List<string> { "Invalid ID" }
                    };
                }

                await _unitOfWork.JobRepository.DeleteAsync(existing);

                return new ApiResponse<Job>
                {
                    Success = true,
                    Message = "Job deleted successfully.",
                    Data = null,
                    Errors = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Job>
                {
                    Success = false,
                    Message = "Failed to deleted Job.",
                    Data = null,
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<ApiResponse<List<Job>>> GetAll()
        {
            var Jobs = await _unitOfWork.JobRepository.GetTableNoTracking().ToListAsync();

            return new ApiResponse<List<Job>>
            {
                Success = true,
                Message = "Jobs retrieved successfully.",
                Data = Jobs,
                Errors = null
            };
        }

        public async Task<ApiResponse<Job>> GetById(string id)
        {
            var Job = await _unitOfWork.JobRepository.GetByIdTypeStringAsync(id);

            if (Job == null)
            {
                return new ApiResponse<Job>
                {
                    Success = false,
                    Message = "Job not found.",
                    Data = null,
                    Errors = new List<string> { "Invalid ID" }
                };
            }

            return new ApiResponse<Job>
            {
                Success = true,
                Message = "Job retrieved successfully.",
                Data = Job,
                Errors = null
            };
        }

        public async Task<ApiResponse<Job>> Update(UpdateJob updateJob)
        {
            try
            {
                var Job = await _unitOfWork.JobRepository.GetByIdTypeStringAsync(updateJob.Id);

                if (Job == null)
                {
                    return new ApiResponse<Job>
                    {
                        Success = false,
                        Message = "Job not found.",
                        Data = null,
                        Errors = new List<string> { "Invalid ID" }
                    };
                }

                Job.NameAr = updateJob.JobNameAr;
                Job.NameEn = updateJob.JobNameEn;

                await _unitOfWork.JobRepository.UpdateWithSPAsync(Job);

                return new ApiResponse<Job>
                {
                    Success = true,
                    Message = "Job updated successfully.",
                    Data = Job,
                    Errors = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Job>
                {
                    Success = false,
                    Message = "Failed to updated Job.",
                    Data = null,
                    Errors = new List<string> { ex.Message }
                };
            }

        }
    }
}
