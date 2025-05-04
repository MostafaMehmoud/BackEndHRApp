using AutoMapper;

using Microsoft.EntityFrameworkCore;

using HrApp.BL.Services.IServices;
using HrApp.DAL.Data;
using HrApp.DAL.Dtos;
using HrApp.DAL.Entities;
using HrApp.DAL.Repository.IRepository;
using HrApp.DAL.Repository;

namespace HrApp.BL.Services;

public class ManageService : IManageService
{
    private readonly IMapper _mapper;
    private readonly HrAppDbContext _dbcontext;
    private readonly IUnitOfWork _unitOfWork;
    public ManageService(IMapper mapper, HrAppDbContext dbcontext,IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _dbcontext = dbcontext;
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse<Manage>> Add(CreateManage createManage)
    {
        try
        {
            var manage = new Manage
            {
                Id = ""
               ,
                NameAr = createManage.ManageNameAr,
                NameEn = createManage.ManageNameEn
            };

            await _unitOfWork.ManageRepository.AddWithSPAsync(manage);

            return new ApiResponse<Manage>
            {
                Success = true,
                Message = "Manage added successfully.",
                Data = manage,
                Errors = null
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<Manage>
            {
                Success = false,
                Message = "Failed to add Manage.",
                Data = null,
                Errors = new List<string> { ex.Message }
            };
        }
    }

    public async Task<ApiResponse<Manage>> Delete(string id)
    {
        try
        {
            var existing = await _unitOfWork.ManageRepository.GetByIdTypeStringAsync(id);
            if (existing == null)
            {
                return new ApiResponse<Manage>
                {
                    Success = false,
                    Message = "Manage not found.",
                    Data = null,
                    Errors = new List<string> { "Invalid ID" }
                };
            }

            await _unitOfWork.ManageRepository.DeleteAsync(existing);

            return new ApiResponse<Manage>
            {
                Success = true,
                Message = "Manage deleted successfully.",
                Data = null,
                Errors = null
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<Manage>
            {
                Success = false,
                Message = "Failed to deleted Manage.",
                Data = null,
                Errors = new List<string> { ex.Message }
            };
        }

    }

    public async Task<ApiResponse<List<Manage>>> GetAll()
    {
        var Manages = await _unitOfWork.ManageRepository.GetTableNoTracking().ToListAsync();

        return new ApiResponse<List<Manage>>
        {
            Success = true,
            Message = "Manages retrieved successfully.",
            Data = Manages,
            Errors = null
        };
    }

    public async Task<List<ModelOutputDto>> GetAllManageAsync()
    {
        var list =await _dbcontext.Manages.Select(m => new ModelOutputDto()
        {
            Id = m.Id,
            NameAr = m.NameAr,
            NameEn = m.NameEn,
        }).ToListAsync();

        return list;
    }

    public async Task<ApiResponse<Manage>> GetById(string id)
    {
        var Manage = await _unitOfWork.ManageRepository.GetByIdTypeStringAsync(id);

        if (Manage == null)
        {
            return new ApiResponse<Manage>
            {
                Success = false,
                Message = "manage not found.",
                Data = null,
                Errors = new List<string> { "Invalid ID" }
            };
        }

        return new ApiResponse<Manage>
        {
            Success = true,
            Message = "manage retrieved successfully.",
            Data = Manage,
            Errors = null
        };
    }

    public async Task<ApiResponse<Manage>> Update(UpdateManage updateManage)
    {
        try
        {
            var manage = await _unitOfWork.ManageRepository.GetByIdTypeStringAsync(updateManage.Id);

            if (manage == null)
            {
                return new ApiResponse<Manage>
                {
                    Success = false,
                    Message = "manage not found.",
                    Data = null,
                    Errors = new List<string> { "Invalid ID" }
                };
            }

            manage.NameAr = updateManage.ManageNameAr;
            manage.NameEn = updateManage.ManageNameEn;

            await _unitOfWork.ManageRepository.UpdateWithSPAsync(manage);

            return new ApiResponse<Manage>
            {
                Success = true,
                Message = "Manage updated successfully.",
                Data = manage,
                Errors = null
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<Manage>
            {
                Success = false,
                Message = "Failed to updated manage.",
                Data = null,
                Errors = new List<string> { ex.Message }
            };
        }

    }
}
