
using AutoMapper;
using HrApp.API.Repos;
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

public class AdvancePaymentService : IAdvancePaymentService
{
    private readonly IMapper _mapper;
    private readonly IAdvancePaymentRepository _advancePaymentRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly BusinessSettingsConfig _businessSettingsConfig;
    public AdvancePaymentService(IMapper mapper, IAdvancePaymentRepository advancePaymentRepository,IEmployeeRepository employeeRepository, IOptions<BusinessSettingsConfig> options)
    {
        _mapper = mapper;
        _advancePaymentRepository = advancePaymentRepository;
        _employeeRepository = employeeRepository;
        _businessSettingsConfig = options.Value;
    }

    public async Task<bool> CreateRequestAdvancePaymentAsync(CreateRequestAdvancePaymentInputDto dto, string empId)
    {
        var id = await _advancePaymentRepository.GetTableNoTracking().MaxAsync(m => m.Id) ?? "0";
        var requestAdvancePayment = new RequestAdvancePayment()
        {
            Id = (int.Parse(id) + 1).ToString(),
            AdvancePaymentValue = dto.AdvancePaymentValue,
            InstallmentValue = dto.InstallmentValue,
            NumberOfInstallment = dto.NumberOfInstallment,
            LastInstallmentValue = dto.LastInstallmentValue,
            StartDate = dto.StartDate.ToShortDateString(),
            EmployeeId = empId
        };
        var res = await _advancePaymentRepository.AddAsync(requestAdvancePayment);
        return res != null;
    }


    public async Task<GetEmployeeAdvancePaymentsOutputDto> GetEmployeeAdvancePaymentsAsync(string empId)
    {
        var list = await _advancePaymentRepository.GetTableNoTracking().Where(m => m.EmployeeId == empId).ToListAsync();

        var result = new GetEmployeeAdvancePaymentsOutputDto();

        result.AdvancePayments.AddRange(_mapper.Map<List<RequestAdvancePayment>, List<EmpAdvancePaymentDto>>(list));

        return result;
    }

    public async Task<GetPindingAdvancePaymentsOutputDto> GetAdvancePaymentPindingListAsync(InputDto dto)
    {
        IQueryable<RequestAdvancePayment> q;
         q = _advancePaymentRepository.GetTableNoTracking().Where(m => m.ApproveStatus == ApproveType.Pinding)
            .Include(m=>m.Employee).AsQueryable();

        if (!string.IsNullOrEmpty(dto.Search))
          q =  q.Where(m => m.EmployeeId.Contains(dto.Search));

        q = q.OrderByDescending(q => q.StartDate);

        var count = q.Count();
        var res = await q.ToListAsync();

        var result = new GetPindingAdvancePaymentsOutputDto()
        {
            RequestAdvancePayments = _mapper.Map<List<RequestAdvancePayment>, List<PindingRequestAdvancePaymentDto>>(res),
            Count = count
        };
        return result;
    }


    public async Task<bool> DecisionOfAdvancePaymentAsync(DecisionAdvancePaymentInputDto dto)
    {

        var res = await _advancePaymentRepository.GetTableAsTracking()
            .Where(m => m.Id == dto.Id.ToString()).FirstOrDefaultAsync();
        if(res == null) throw new AppException($"Advance Payment Not found");

        if (dto.Accepted)
        {
            res.ApproveStatus = ApproveType.Approved ;
        }
        else
        {
            res.ApproveStatus = ApproveType.NotApproved;
        }


        return await _advancePaymentRepository.UpdateAsync(res);

    }

    public async Task<BasicAdvancePaymentInfoOutputDto> GetBasicAdvancePaymentInfo(string empId)
    {
        var emptotal = await _employeeRepository.GetEmpAdvancePaymentTotal(empId);

        return new()
        {
            EmpId = empId,
            NameAr = emptotal.NameAr,
            NameEn = emptotal.NameEn,
            LOANSBAL =emptotal.LOANSBAL ?? 0
        };

    }
}
