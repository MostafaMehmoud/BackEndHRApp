using HrApp.DAL.Dtos;

namespace HrApp.BL.Services.IServices;

public interface IAuthenticationService
{
    Task<SignInOutputDto> SignIn(SignInInputDto dto);
    Task<bool> SignOut(string empId);
}
