using HrApp.DAL.Dtos;

namespace HrApp.BL.Services.IServices;

public interface IVacationService
{
    Task<bool> CreateRequestVacationAsync(CreateRequestVacationInputDto dto, string empId);
    Task<GetPindingVacationsOutputDto> GetRequestVacationPindingListAsync(InputDto dto);
    Task<bool> DecisionOfVacationAsync(DecisionInputDto dto);
    Task<GetEmployeeVacationsOutputDto> GetEmployeeVacationsAsync(string empId);
    Task<BasicVacationInfoOutputDto> GetBasicVacationInfo(string empId);
}
