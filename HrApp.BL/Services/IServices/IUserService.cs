using HrApp.DAL.Dtos;

namespace HrApp.BL.Services.IServices;

public interface IUserService
{
    Task<GetUserByUsernameOrEmpIdOutputDto> GetUserByUsernameOrEmpIdAsync(string username);
}
