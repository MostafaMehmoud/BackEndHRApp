using AutoMapper;
using HrApp.BL.Interfaces;
using HrApp.BL.Interfaces.Helpers;
using HrApp.BL.Services.IServices;
using HrApp.DAL.Dtos;
using HrApp.DAL.Enums;
using HrApp.DAL.Repository.IRepository;



namespace HrApp.BL.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IEmployeeService _employeeService;
    private readonly IUserService _userService;
    private readonly IAuthLogRepository _authLogRepository;
    private readonly IJwtUtils _jwtUtils;
    private readonly IMapper _mapper;

    public AuthenticationService(IUserService userService, IEmployeeService employeeService, IJwtUtils jwtUtils, IMapper mapper, IAuthLogRepository authLogRepository)
    {
        _userService = userService;
        _employeeService = employeeService;
        _jwtUtils = jwtUtils;
        _mapper = mapper;
        _authLogRepository = authLogRepository;
    }
    public async Task<SignInOutputDto> SignIn(SignInInputDto dto)
    {
        var userFound = await _userService.GetUserByUsernameOrEmpIdAsync(dto.Username);

        // validate
        if (userFound == null || dto.Password != userFound.Password)
           throw new 
                ("username or password is incorrect");

        // authentication successful so generate jwt and refresh tokens

        var jwtToken = _jwtUtils.GenerateJwtToken(userFound);

        var emp = await _employeeService.GetEmployeeInfoByIdAsync(userFound.EmployeeId);
        emp.EmpId = emp.EmpId ?? userFound.EmployeeId;
        emp.NameAr = emp.NameAr ?? userFound.Username;
        emp.NameEn = emp.NameEn;
        emp.IsAdmin = userFound.IsAdmin;
        var authLoginDateList = _authLogRepository.GetTableNoTracking().Where(m => m.EmpId == userFound.EmployeeId && m.AuthType== AuthTypeType.Login).OrderByDescending(m => m.AuthDate).Take(3).Select(m =>m.AuthDate).ToList();
        await _authLogRepository.AddAsync(new() { EmpId = userFound.EmployeeId, AuthType = AuthTypeType.Login, AuthDate = DateTime.Now });
        var response = new SignInOutputDto();
        response.AccessToken = jwtToken;
        response.EmployeeInfo = emp;
        response.LoginDateList = authLoginDateList ?? [];
        return response;
    }

    public async Task<bool> SignOut(string empId)
    {
       var res = await _authLogRepository.AddAsync(new() { EmpId = empId, AuthType = AuthTypeType.Logout, AuthDate = DateTime.Now });
        return res != null;
    }
}
