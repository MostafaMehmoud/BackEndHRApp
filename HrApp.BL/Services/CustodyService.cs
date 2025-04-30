using AutoMapper;

using HrApp.API.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.IO;

using HrApp.BL.Services.IServices;
using HrApp.DAL.Repository.IRepository;
using HrApp.DAL.Configratins;
using HrApp.DAL.Dtos;
using HrApp.BL.Helpers;
using HrApp.DAL.Entities;
using HrApp.DAL.Enums;


namespace HrApp.BL.Services;

public class CustodyService : ICustodyService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly BusinessSettingsConfig _businessSettingsConfig;
    public CustodyService(IMapper mapper, IUnitOfWork unitOfWork, IOptions<BusinessSettingsConfig> options)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _businessSettingsConfig = options.Value;
    }

    public async Task<bool> CreateCustodyAsync(CreateRequestCustodyInputDto dto, string empId)
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

            var requestVacation = new RequestCustody()
            {
                Details=dto.Details,
                IsEmployeeAgree=dto.IsEmployeeAgree ? 1:0,
                EmployeeId = empId,
                UploadFileId = uploadFileId
            };
            var res = await _unitOfWork.CustodyRepository.AddAsync(requestVacation);
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


    public async Task<GetEmployeeCustodiesOutputDto> GetEmployeeCustodiesAsync(string empId)
    {
        var list = await _unitOfWork.CustodyRepository.GetTableNoTracking().Where(m => m.EmployeeId == empId).ToListAsync();

        var result = new GetEmployeeCustodiesOutputDto();

        result.RequestCustodies.AddRange(_mapper.Map<List<RequestCustody>, List<EmpRequestCustodyDto>>(list));

        return result;
    }

    public async Task<GetPindingCustodiesOutputDto> GetCustodyPindingListAsync(InputDto dto)
    {
        IQueryable<RequestCustody> q;
         q = _unitOfWork.CustodyRepository.GetTableNoTracking().Where(m => m.ApproveStatus == ApproveType.Pinding)
            .Include(m=>m.Employee).AsQueryable();

        if (!string.IsNullOrEmpty(dto.Search))
          q =  q.Where(m => m.EmployeeId.Contains(dto.Search));

        q = q.OrderByDescending(q => q.Id);

        var count = q.Count();
        var res = await q.ToListAsync();

        var result = new GetPindingCustodiesOutputDto()
        {
            RequestCustodies = _mapper.Map<List<RequestCustody>, List<PindingCustodyOutputDto>>(res),
            Count = count
        };
        return result;
    }


    public async Task<bool> DecisionOfCustodyAsync(DecisionInputDto dto)
    {

        var res = await _unitOfWork.CustodyRepository.GetTableAsTracking()
            .Where(m => m.Id == dto.Id).FirstOrDefaultAsync();
        if(res == null) throw new AppException($"Custody Not found");

        if (dto.Accepted)
        {
            res.ApproveStatus = ApproveType.Approved ;
        }
        else
        {
            res.ApproveStatus = ApproveType.NotApproved;
        }


        return await _unitOfWork.CustodyRepository.UpdateAsync(res);

    }

    public async Task<BasicCustodyInfoOutputDto> GetBasicCustodyInfo(string empId)
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
