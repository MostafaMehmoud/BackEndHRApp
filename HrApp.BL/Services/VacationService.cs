using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using HrApp.DAL.Enums;
using HrApp.DAL.Dtos;
using HrApp.BL.Helpers;
using HrApp.DAL.Entities;
using HrApp.DAL.Configratins;
using HrApp.DAL.Repository.IRepository;
using HrApp.BL.Services.IServices;


namespace HrApp.BL.Services;

public class VacationService : IVacationService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly BusinessSettingsConfig _businessSettingsConfig;
    public VacationService(IMapper mapper, IUnitOfWork unitOfWork, IOptions<BusinessSettingsConfig> options)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _businessSettingsConfig = options.Value;
    }


    public async Task<bool> CreateRequestVacationAsync(CreateRequestVacationInputDto dto, string empId)
    {
        if (dto.VacationType == VacationType.Annual)
        {
            var numberOfDaysTake = await _unitOfWork.VacationRepository.GetTableNoTracking()
                 .Where(m => m.EmployeeId == empId && m.ApproveStatus == ApproveType.Approved && m.VacationType == VacationType.Annual && m.StartDate > DateTime.Now.AddYears(-1)).SumAsync(m => m.NumberOfDays);

            if ((numberOfDaysTake + dto.NumberOfDays) > _businessSettingsConfig.TotalAllowanceDays)
                throw new AppException($"Total Allowance Days : {_businessSettingsConfig.TotalAllowanceDays} , Employee {empId} took {numberOfDaysTake} Before");
        }
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
            uploadFileId = (await _unitOfWork.UploadFileRepository.AddAsync(FileHelper.CreateUploadFile(dto.File)))?.Id;

            var requestVacation = new RequestVacation()
            {
                NumberOfDays = dto.NumberOfDays,
                StartDate = dto.StartDate,
                VacationType = dto.VacationType,
                Details = dto.Details,
                EmployeeId = empId,
                UploadFileId = uploadFileId
            };
            var res = await _unitOfWork.VacationRepository.AddAsync(requestVacation);
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

    public async Task<GetEmployeeVacationsOutputDto> GetEmployeeVacationsAsync(string empId)
    {
        var list = await _unitOfWork.VacationRepository.GetTableNoTracking().Where(m => m.EmployeeId == empId && m.StartDate > DateTime.Now.AddYears(-1)).ToListAsync();

        var result = new GetEmployeeVacationsOutputDto();

        result.RequestVacations.AddRange(_mapper.Map<List<RequestVacation>, List<EmpRequestVacationDto>>(list));

        return result;
    }

    public async Task<GetPindingVacationsOutputDto> GetRequestVacationPindingListAsync(InputDto dto)
    {
        IQueryable<RequestVacation> q;

         q = _unitOfWork.VacationRepository.GetTableNoTracking()
            .Include(m => m.Employee).ThenInclude(m=>m.RequestVacations)
            .Where(m => m.ApproveStatus == ApproveType.Pinding).AsQueryable();

        if (!string.IsNullOrEmpty(dto.Search))
            q = q.Where(m => m.EmployeeId.Contains(dto.Search));

        q = q.OrderByDescending(q => q.StartDate);

        var count = q.Count();
        var res = await q.AsNoTrackingWithIdentityResolution().ToListAsync();
        var list = _mapper.Map<List<RequestVacation>, List<PindingVacationOutputDto>>(res);
        list.ForEach(m => m.TotalAllowanceDays = _businessSettingsConfig.TotalAllowanceDays);
        var result = new GetPindingVacationsOutputDto()
        {
            RequestVacations = list,
            Count = count
        };
        return result;
    }

    public async Task<bool> DecisionOfVacationAsync(DecisionInputDto dto)
    {

        var res = await _unitOfWork.VacationRepository.GetByIdAsync(dto.Id);

        if (dto.Accepted)
        {
            if (res.VacationType == VacationType.Annual)
            {
                var numberOfDaysTake = await _unitOfWork.VacationRepository.GetTableNoTracking()
                .Where(m => m.EmployeeId == res.EmployeeId && m.ApproveStatus == ApproveType.Approved && m.VacationType == VacationType.Annual && m.StartDate > DateTime.Now.AddYears(-1)).SumAsync(m => m.NumberOfDays);

                if ((numberOfDaysTake + res.NumberOfDays) > _businessSettingsConfig.TotalAllowanceDays)
                    throw new AppException($"Total Allowance Days : {_businessSettingsConfig.TotalAllowanceDays} , Employee {res.EmployeeId} took {numberOfDaysTake} Before");
            }
            res.ApproveStatus = ApproveType.Approved ;
        }
        else
        {
            res.ApproveStatus = ApproveType.NotApproved;
        }

        res.ApproveDetails = dto.Note;

        return await _unitOfWork.VacationRepository.UpdateAsync(res);

    }

    public async Task<BasicVacationInfoOutputDto> GetBasicVacationInfo(string empId)
    {
        var numberOfDaysTake = await _unitOfWork.VacationRepository.GetTableNoTracking()
            .Where(m => m.EmployeeId == empId && m.ApproveStatus == ApproveType.Approved && m.VacationType == VacationType.Annual && m.StartDate > DateTime.Now.AddYears(-1)).SumAsync(m => m.NumberOfDays);
        var emp = await _unitOfWork.EmployeeRepository.GetTableNoTracking().Where(m=>m.EmployeeId == empId).FirstOrDefaultAsync();
        return new BasicVacationInfoOutputDto()
        {
            EmpId = empId,
            NameAr = emp?.NameAr,
            NameEn = emp?.NameEn,
            TotalAllowanceDays = _businessSettingsConfig.TotalAllowanceDays,
            TakenDays = numberOfDaysTake ?? 0
        };
    }
}
