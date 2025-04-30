


using HrApp.DAL.Dtos;

namespace HrApp.BL.Services.IServices;

public interface IAdvancePaymentService
{
    Task<bool> CreateRequestAdvancePaymentAsync(CreateRequestAdvancePaymentInputDto dto, string empId);
    Task<GetPindingAdvancePaymentsOutputDto> GetAdvancePaymentPindingListAsync(InputDto dto);
    Task<bool> DecisionOfAdvancePaymentAsync(DecisionAdvancePaymentInputDto dto);
    Task<GetEmployeeAdvancePaymentsOutputDto> GetEmployeeAdvancePaymentsAsync(string empId);
    Task<BasicAdvancePaymentInfoOutputDto> GetBasicAdvancePaymentInfo(string empId);
}
