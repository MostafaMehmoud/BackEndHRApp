using AutoMapper;
using HrApp.BL.Helpers;
using HrApp.BL.Services.IServices;
using HrApp.DAL.Configratins;
using HrApp.DAL.Dtos;
using HrApp.DAL.Entities;
using HrApp.DAL.Enums;
using HrApp.DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;



namespace HrApp.BL.Services;

public class PermissionService : IPermissionService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly BusinessSettingsConfig _businessSettingsConfig;
    public PermissionService(IMapper mapper, IUnitOfWork unitOfWork, IOptions<BusinessSettingsConfig> options)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _businessSettingsConfig = options.Value;
    }

    public async Task<bool> CreateRequestPermissionAsync(CreateRequestPermissionInputDto dto, string empId)
    {
        await _unitOfWork.StartTransaction();
        try
        {
            string? uploadFileId = null;
            if (dto.File != null)
            {
                if (dto.File.Length > 2097152)
                    throw new AppException("File Must be Less than  2 Mb");
                uploadFileId = (await _unitOfWork.UploadFileRepository.AddAsync(FileHelper.CreateUploadFile(dto.File)))?.Id;
            }

            var requestVacation = new RequestPermission()
            {
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,                
                Details = dto.Details,
                EmployeeId = empId,
                UploadFileId = uploadFileId
            };
            var res = await _unitOfWork.PermissionRepository.AddAsync(requestVacation);
            if (res != null)
                await _unitOfWork.CommitTransaction();
            else
                await _unitOfWork.RollbackTransaction();
            return res != null;
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransaction();
            throw new AppException(e.Message);
        }
    }


    public async Task<GetEmployeePermissionsOutputDto> GetEmployeePermissionsAsync(string empId)
    {
        var list = await _unitOfWork.PermissionRepository.GetTableNoTracking().Where(m => m.EmployeeId == empId).ToListAsync();

        var result = new GetEmployeePermissionsOutputDto();

        result.RequestPermissions.AddRange(_mapper.Map<List<RequestPermission>, List<EmpRequestPermissionDto>>(list));

        return result;
    }

    public async Task<GetPindingPermissionsOutputDto> GetPermissionPindingListAsync(InputDto dto)
    {
        IQueryable<RequestPermission> q;
        q = _unitOfWork.PermissionRepository.GetTableNoTracking().Where(m => m.ApproveStatus == ApproveType.Pinding)
           .Include(m => m.Employee).AsQueryable();

        if (!string.IsNullOrEmpty(dto.Search))
            q = q.Where(m => m.EmployeeId.Contains(dto.Search));

        q = q.OrderByDescending(q => q.Id);

        var count = q.Count();
        var res = await q.ToListAsync();

        var result = new GetPindingPermissionsOutputDto()
        {
            RequestPermissions = _mapper.Map<List<RequestPermission>, List<PindingPermissionOutputDto>>(res),
            Count = count
        };
        return result;
    }


    public async Task<bool> DecisionOfPermissionAsync(DecisionInputDto dto)
    {

        var res = await _unitOfWork.PermissionRepository.GetTableAsTracking()
            .Where(m => m.Id == dto.Id).FirstOrDefaultAsync();
        if (res == null) throw new AppException($"Permission Not found");

        if (dto.Accepted)
        {
            res.ApproveStatus = ApproveType.Approved;
        }
        else
        {
            res.ApproveStatus = ApproveType.NotApproved;
        }


        return await _unitOfWork.PermissionRepository.UpdateAsync(res);

    }

    public async Task<BasicPermissionInfoOutputDto> GetBasicPermissionInfo(string empId)
    {
        var emptotal = await _unitOfWork.EmployeeRepository.GetEmpAdvancePaymentTotal(empId);

        return new()
        {
            EmpId = empId,
            NameAr = emptotal.NameAr,
            NameEn = emptotal.NameEn
        };

    }
}
