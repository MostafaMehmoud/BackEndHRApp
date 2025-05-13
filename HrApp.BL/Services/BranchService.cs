using AutoMapper;

using Microsoft.EntityFrameworkCore;


using HrApp.BL.Services.IServices;
using HrApp.DAL.Data;
using HrApp.DAL.Dtos;
using HrApp.DAL.Entities;
using HrApp.DAL.Repository.IRepository;


namespace HrApp.BL.Services;

public class BranchService : IBranchService
{
    private readonly IMapper _mapper;
    private readonly HrAppDbContext _dbcontext;
    private readonly IUnitOfWork _unitOfWork;   
    public BranchService(IMapper mapper, HrAppDbContext dbcontext,IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _dbcontext = dbcontext;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<ModelOutputDto>> GetAllBranchAsync()
    {
        var list =await _dbcontext.Branches.Select(m => new ModelOutputDto()
        {
            Id = m.Id,
            NameAr = m.NameAr,
            NameEn = m.NameEn,
        }).ToListAsync();

        return list;
    }
    public async Task<ApiResponse<Branch>> Add(CreateBranch createBranch)
    {
        try
        {
            var Branch = new Branch
            {
                Id = ""
               ,
                NameAr = createBranch.BranchNameAr,
                NameEn = createBranch.BranchNameEn,
                CompanyId = createBranch.CompanyId
            };

            await _unitOfWork.BranchRepository.AddWithSPAsync(Branch);

            return new ApiResponse<Branch>
            {
                Success = true,
                Message = "Branch added successfully.",
                Data = Branch,
                Errors = null
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<Branch>
            {
                Success = false,
                Message = "Failed to add Branch.",
                Data = null,
                Errors = new List<string> { ex.Message }
            };
        }
    }

    public async Task<ApiResponse<Branch>> Delete(string id)
    {
        try
        {
            var existing = await _unitOfWork.BranchRepository.GetByIdTypeStringAsync(id);
            if (existing == null)
            {
                return new ApiResponse<Branch>
                {
                    Success = false,
                    Message = "Branch not found.",
                    Data = null,
                    Errors = new List<string> { "Invalid ID" }
                };
            }

            await _unitOfWork.BranchRepository.DeleteAsync(existing);

            return new ApiResponse<Branch>
            {
                Success = true,
                Message = "Branch deleted successfully.",
                Data = null,
                Errors = null
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<Branch>
            {
                Success = false,
                Message = "Failed to deleted Branch.",
                Data = null,
                Errors = new List<string> { ex.Message }
            };
        }

    }

    public async Task<ApiResponse<List<Branch>>> GetAll()
    {
        var Branchs = await _unitOfWork.BranchRepository.GetTableNoTracking().ToListAsync();

        return new ApiResponse<List<Branch>>
        {
            Success = true,
            Message = "Branchs retrieved successfully.",
            Data = Branchs,
            Errors = null
        };
    }

    public async Task<ApiResponse<Branch>> GetById(string id)
    {
        var Branch = await _unitOfWork.BranchRepository.GetByIdTypeStringAsync(id);

        if (Branch == null)
        {
            return new ApiResponse<Branch>
            {
                Success = false,
                Message = "Branch not found.",
                Data = null,
                Errors = new List<string> { "Invalid ID" }
            };
        }

        return new ApiResponse<Branch>
        {
            Success = true,
            Message = "Branch retrieved successfully.",
            Data = Branch,
            Errors = null
        };
    }

    public async Task<ApiResponse<Branch>> Update(UpdateBranch updateBranch)
    {
        try
        {
            var Branch = await _unitOfWork.BranchRepository.GetByIdTypeStringAsync(updateBranch.Id);

            if (Branch == null)
            {
                return new ApiResponse<Branch>
                {
                    Success = false,
                    Message = "Branch not found.",
                    Data = null,
                    Errors = new List<string> { "Invalid ID" }
                };
            }

            Branch.NameAr = updateBranch.BranchNameAr;
            Branch.NameEn = updateBranch.BranchNameEn;
            Branch.CompanyId = updateBranch.CompanyId;

            await _unitOfWork.BranchRepository.UpdateWithSPAsync(Branch);

            return new ApiResponse<Branch>
            {
                Success = true,
                Message = "Branch updated successfully.",
                Data = Branch,
                Errors = null
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<Branch>
            {
                Success = false,
                Message = "Failed to updated Branch.",
                Data = null,
                Errors = new List<string> { ex.Message }
            };
        }
    }

    public ApiResponse<IEnumerable<Branch>> GetAllInculdeBranches()
    {
        var Branchs = _unitOfWork.BranchRepository.GetAllInclude();

        return new ApiResponse<IEnumerable<Branch>>
        {
            Success = true,
            Message = "Branches retrieved successfully.",
            Data = Branchs,
            Errors = null
        };
    }
}
