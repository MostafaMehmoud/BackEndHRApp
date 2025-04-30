

using HrApp.DAL.Dtos;

namespace HrApp.BL.Services.IServices;

public interface ICustodyService
{
    Task<bool> CreateCustodyAsync(CreateRequestCustodyInputDto dto, string empId);
    Task<GetPindingCustodiesOutputDto> GetCustodyPindingListAsync(InputDto dto);
    Task<bool> DecisionOfCustodyAsync(DecisionInputDto dto);
    Task<GetEmployeeCustodiesOutputDto> GetEmployeeCustodiesAsync(string empId);
    Task<BasicCustodyInfoOutputDto> GetBasicCustodyInfo(string empId);
}
