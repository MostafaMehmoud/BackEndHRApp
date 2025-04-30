using AutoMapper;
using HrApp.BL.Services.IServices;
using HrApp.DAL.Configratins;
using HrApp.DAL.Dtos;
using HrApp.DAL.Entities;
using HrApp.DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;



namespace HrApp.BL.Services;

public class EmployeeAttendanceService : IEmployeeAttendanceService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly BusinessSettingsConfig _businessSettingsConfig;
    public EmployeeAttendanceService(IMapper mapper, IUnitOfWork unitOfWork, IOptions<BusinessSettingsConfig> options)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _businessSettingsConfig = options.Value;
    }

    public async Task<GetEmployeeAttendanceOutputDto> GetEmployeeAttendance(GetEmployeeAttendanceInputDto dto)
    {
        IQueryable<EmployeeAttendance> q;
        q = _unitOfWork.EmployeeAttendanceRepository.GetTableNoTracking()
           .Include(m => m.Employee).AsQueryable();

        if (!string.IsNullOrEmpty(dto.EmployeeId))
            q = q.Where(m => m.EmployeeId == dto.EmployeeId);
        if (dto.ProjectId !=null && dto.ProjectId != 0)
            q = q.Where(m => m.ProjectId == dto.ProjectId);
        if (dto.FromDate != null && dto.FromDate != default)
            q = q.Where(m => m.Date >=dto.FromDate );
        if (dto.ToDate != null && dto.ToDate != default)
            q = q.Where(m => m.Date <= dto.ToDate);

        q = q.OrderByDescending(q => q.Date);

        var count = q.Count();
        var res = await q.ToListAsync();

        var result = new GetEmployeeAttendanceOutputDto()
        {
            EmployeeAttendances = _mapper.Map<List<EmployeeAttendance>, List<EmployeeAttendanceOutputDto>>(res)
        };
        return result;
    }

    public async Task<bool> InsertEmployeeAttendances(InsertEmployeeAttendanceInputDto dto, string empId)
    {
        var insertList = _mapper.Map<List<EmployeeAttendanceInputDto>, List<EmployeeAttendance>>(dto.EmployeeAttendances);
        foreach (var item in insertList)
        {
            item.ByEmployeeId= empId;
            item.Employee= null;
            
        }
        var result = await _unitOfWork.EmployeeAttendanceRepository.AddRangeAsync(insertList);

        return result;
    }
}
