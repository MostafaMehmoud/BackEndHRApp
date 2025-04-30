using AutoMapper;

using HrApp.API.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using HrApp.BL.Services.IServices;
using HrApp.DAL.Repository.IRepository;
using HrApp.DAL.Configratins;
using HrApp.DAL.Dtos;
using HrApp.BL.Helpers;
using HrApp.DAL.Entities;
using HrApp.DAL.Enums;


namespace HrApp.BL.Services;

public class LetterService : ILetterService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly BusinessSettingsConfig _businessSettingsConfig;
    public LetterService(IMapper mapper, IUnitOfWork unitOfWork, IOptions<BusinessSettingsConfig> options)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _businessSettingsConfig = options.Value;
    }

    public async Task<bool> CreateRequestLetterAsync(CreateRequestLetterInputDto dto, string empId)
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

            var requestVacation = new RequestLetter()
            {
                DestinationName = dto.DestinationName,
                LetterType = dto.LetterType,
                IsRequiredCCC = dto.IsRequiredCCC ? 1 : 0,
                Details = dto.Details,
                EmployeeId = empId,
                UploadFileId = uploadFileId
            };
            var res = await _unitOfWork.LetterRepository.AddAsync(requestVacation);
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


    public async Task<GetEmployeeLettersOutputDto> GetEmployeeLettersAsync(string empId)
    {
        var list = await _unitOfWork.LetterRepository.GetTableNoTracking().Where(m => m.EmployeeId == empId).ToListAsync();

        var result = new GetEmployeeLettersOutputDto();

        result.RequestLetters.AddRange(_mapper.Map<List<RequestLetter>, List<EmpRequestLetterDto>>(list));

        return result;
    }

    public async Task<GetPindingLettersOutputDto> GetLetterPindingListAsync(InputDto dto)
    {
        IQueryable<RequestLetter> q;
        q = _unitOfWork.LetterRepository.GetTableNoTracking().Where(m => m.ApproveStatus == ApproveType.Pinding)
           .Include(m => m.Employee).AsQueryable();

        if (!string.IsNullOrEmpty(dto.Search))
            q = q.Where(m => m.EmployeeId.Contains(dto.Search));

        q = q.OrderByDescending(q => q.Id);

        var count = q.Count();
        var res = await q.ToListAsync();

        var result = new GetPindingLettersOutputDto()
        {
            RequestLetters = _mapper.Map<List<RequestLetter>, List<PindingLetterOutputDto>>(res),
            Count = count
        };
        return result;
    }


    public async Task<bool> DecisionOfLetterAsync(DecisionInputDto dto)
    {

        var res = await _unitOfWork.LetterRepository.GetTableAsTracking()
            .Where(m => m.Id == dto.Id).FirstOrDefaultAsync();
        if (res == null) throw new AppException($"Letter Not found");

        if (dto.Accepted)
        {
            res.ApproveStatus = ApproveType.Approved;
        }
        else
        {
            res.ApproveStatus = ApproveType.NotApproved;
        }


        return await _unitOfWork.LetterRepository.UpdateAsync(res);

    }

    public async Task<BasicLetterInfoOutputDto> GetBasicLetterInfo(string empId)
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
