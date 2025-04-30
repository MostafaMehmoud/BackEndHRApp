using AutoMapper;
using HrApp.DAL.Dtos;
using HrApp.DAL.Entities;
using HrApp.DAL.Enums;


namespace HrApp.BL.Mapper;

public class AutoMapperProfile : Profile
{
    // mappings between model and entity objects
    public AutoMapperProfile()
    {
        //employee

        CreateMap<Employee, GetEmployeeByIdOutputDto>().ReverseMap();
        CreateMap<Employee, EmployeeInfoOutputDto>().ReverseMap();
        CreateMap<User, GetUserByUsernameOrEmpIdOutputDto>()
            .ForMember(des => des.IsAdmin, opt => opt.MapFrom(src => src.Admin == 2 ? true : false))
            .ReverseMap();

        //RequestVacation
        CreateMap<RequestVacation, PindingVacationOutputDto>()
            .ForMember(des => des.EmployeeNameAr, opt => opt.MapFrom(src => src.Employee == null ? null : src.Employee.NameAr))
            .ForMember(des => des.EmployeeNameEn, opt => opt.MapFrom(src => src.Employee == null ? null : src.Employee.NameEn))
            .ForMember(des => des.TakenDays, opt => opt.MapFrom(src => src.Employee == null ? null :
                      src.Employee.RequestVacations.Where(m => m.VacationType == VacationType.Annual).Sum(m => m.NumberOfDays)))
            .ReverseMap();
        CreateMap<RequestVacation, EmpRequestVacationDto>().ReverseMap();

        //RequestAdvancePayment
        CreateMap<RequestAdvancePayment, PindingRequestAdvancePaymentDto>()
            .ForMember(des => des.EmployeeNameAr, opt => opt.MapFrom(src => src.Employee == null ? null : src.Employee.NameAr))
            .ForMember(des => des.EmployeeNameEn, opt => opt.MapFrom(src => src.Employee == null ? null : src.Employee.NameEn))
            .ReverseMap();
        CreateMap<RequestAdvancePayment, EmpAdvancePaymentDto>().ReverseMap();

        //RequestCustody
        CreateMap<RequestCustody, PindingCustodyOutputDto>()
            .ForMember(des => des.EmployeeNameAr, opt => opt.MapFrom(src => src.Employee == null ? null : src.Employee.NameAr))
            .ForMember(des => des.EmployeeNameEn, opt => opt.MapFrom(src => src.Employee == null ? null : src.Employee.NameEn))
            .ForMember(des => des.IsEmployeeAgree, opt => opt.MapFrom(src => src.IsEmployeeAgree == 0 ? false : true))
            .ReverseMap();
        CreateMap<RequestCustody, EmpRequestCustodyDto>()
            .ForMember(des => des.IsEmployeeAgree, opt => opt.MapFrom(src => src.IsEmployeeAgree == 0 ? false : true))
            .ReverseMap();

        //RequestLetter
        CreateMap<RequestLetter, PindingLetterOutputDto>()
            .ForMember(des => des.EmployeeNameAr, opt => opt.MapFrom(src => src.Employee == null ? null : src.Employee.NameAr))
            .ForMember(des => des.EmployeeNameEn, opt => opt.MapFrom(src => src.Employee == null ? null : src.Employee.NameEn))
             .ForMember(des => des.IsRequiredCCC, opt => opt.MapFrom(src => src.IsRequiredCCC == 0 ? false : true))
            .ReverseMap();
        CreateMap<RequestLetter, EmpRequestLetterDto>()
             .ForMember(des => des.IsRequiredCCC, opt => opt.MapFrom(src => src.IsRequiredCCC == 0 ? false : true))
            .ReverseMap();

        //RequestLetter
        CreateMap<RequestPermission, PindingPermissionOutputDto>()
            .ForMember(des => des.EmployeeNameAr, opt => opt.MapFrom(src => src.Employee == null ? null : src.Employee.NameAr))
            .ForMember(des => des.EmployeeNameEn, opt => opt.MapFrom(src => src.Employee == null ? null : src.Employee.NameEn))
            .ReverseMap();
        CreateMap<RequestPermission, EmpRequestPermissionDto>().ReverseMap();

        //EmployeeDetails
        CreateMap<EmployeeDetails, GetEmployeeDetailsOutputDto>()
            .ForMember(des => des.EmployeeInfo, opt => opt.MapFrom<EmployeeInfoDto>(src => new() { 
                Id = src.EMP_ID,
                NameAr = src.EMP_NAME,
                NameEn= src.EMP_NAME_E,
                Mobile=src.MOBIL,
                MobileEmergency=src.SPECIALIZ,
                DepatmentNameAr=src.DEPT_NAME,
                DepatmentNameEn=src.DEPT_NAME_E,
                JobNameAr=src.JOB_NAME,
                JobNameEn=src.JOB_NAME_E,
                BloodType=src.BLOOD
            }))
            .ForMember(des => des.ContractInfo, opt => opt.MapFrom<EmployeeContractInfoDto>(src => new() { StartDate = src.CONTRACT_DATE, EndDate = src.CONTRACT_ENDDATE }))
            .ForMember(des => des.IdentityInfo, opt => opt.MapFrom<EmployeeIdentityInfoDto>(src => new() { IdentityNumber = src.CARD_ID, ExpiredDate = src.CARD_EXP }))
            .ForMember(des => des.PassportInfo, opt => opt.MapFrom<EmployeePassportInfoDto>(src => new() { PassportNumber = src.PASS_ID, ExpiredDate = src.PASS_EXP }))
            .ForMember(des => des.CarInfo, opt => opt.MapFrom<EmployeeCarInfoDto>(src => new() { CarNumber = src.V5, CheckCarEndDate = src.CHECKDATE, ContractCarEndDate = src.ENDDATE }))
            .ReverseMap();

        //Employee Attendance YYYYMMDDHH24MISS
        CreateMap<EmployeeAttendance, EmployeeAttendanceOutputDto>()
            .ForMember(des => des.Date, opt => opt.MapFrom(src => GetDate(src.Date)))
            .ForMember(des => des.Start, opt => opt.MapFrom(src => GetTime(src.Start)))
            .ForMember(des => des.Leave, opt => opt.MapFrom(src => GetTime(src.Leave)))
            .ForMember(des => des.Absent, opt => opt.MapFrom(src => src.Absent == 0 ? false : true));

        CreateMap<EmployeeAttendanceInputDto, EmployeeAttendance>()
            .ForMember(des => des.Date, opt => opt.MapFrom(src => GetDateTime(src.Date)))
            .ForMember(des => des.Start, opt => opt.MapFrom(src => GetDateTime(src.Start,src.Date)))
            .ForMember(des => des.Leave, opt => opt.MapFrom(src => GetDateTime(src.Leave,src.Date)))
            .ForMember(des => des.Absent, opt => opt.MapFrom(src => src.Absent ? 1 : 0));
    }

    private TimeOnly? GetTime(DateTime? dateTime) => dateTime == null ? null : TimeOnly.FromDateTime(dateTime.Value);
    private DateOnly? GetDate(DateTime? dateTime) => dateTime == null ? null : DateOnly.FromDateTime(dateTime.Value);
    private DateTime? GetDateTime(TimeOnly? time,DateOnly date) => time == null ? null : date.ToDateTime(time.Value);
    private DateTime GetDateTime(DateOnly date) => DateTime.Parse(date.ToString());

}