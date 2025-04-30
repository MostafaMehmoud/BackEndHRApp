
using HrApp.DAL.Dtos;

namespace HrApp.BL.Services.IServices;

public interface ILetterService
{
    Task<bool> CreateRequestLetterAsync(CreateRequestLetterInputDto dto, string empId);
    Task<GetPindingLettersOutputDto> GetLetterPindingListAsync(InputDto dto);
    Task<bool> DecisionOfLetterAsync(DecisionInputDto dto);
    Task<GetEmployeeLettersOutputDto> GetEmployeeLettersAsync(string empId);
    Task<BasicLetterInfoOutputDto> GetBasicLetterInfo(string empId);
}
