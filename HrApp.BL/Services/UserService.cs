using AutoMapper;

using Microsoft.EntityFrameworkCore;

using HrApp.BL.Services.IServices;
using HrApp.DAL.Repository.IRepository;
using HrApp.DAL.Dtos;
using HrApp.DAL.Entities;


namespace HrApp.BL.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public UserService(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }


    public async Task<GetUserByUsernameOrEmpIdOutputDto> GetUserByUsernameOrEmpIdAsync(string username)
    {
        var user = await _userRepository.GetTableNoTracking()
           .Where(m => m.Username == username ||m.EmployeeId == username).FirstOrDefaultAsync();
        return _mapper.Map<User, GetUserByUsernameOrEmpIdOutputDto>(user);
    }
}
