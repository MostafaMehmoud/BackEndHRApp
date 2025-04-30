

using HrApp.DAL.Dtos;

namespace HrApp.BL.Interfaces.Helpers;

public interface IJwtUtils
{
    public string GenerateJwtToken(GetUserByUsernameOrEmpIdOutputDto user);
    public Tuple<int?,bool> ValidateJwtToken(string token);
}
