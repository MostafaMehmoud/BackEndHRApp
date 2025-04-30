using AutoMapper;
using HrApp.API.Helpers;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using HrApp.BL.Services.IServices;
using HrApp.DAL.Data;
using HrApp.DAL.Dtos;
using HrApp.DAL.Entities;
using HrApp.BL.Helpers;
using HrApp.DAL.Enums;


namespace HrApp.BL.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IMapper _mapper;
    private readonly HrAppDbContext _dbcontext;

    public EmployeeService(IMapper mapper, HrAppDbContext dbcontext)
    {
        _mapper = mapper;
        _dbcontext = dbcontext;
    }

    public async Task<EmployeeInfoOutputDto> GetEmployeeInfoByIdAsync(string employeeId)
    {
        var emp = await _dbcontext.Employees.AsNoTracking()
            .Where(m => m.EmployeeId == employeeId).FirstOrDefaultAsync();


        var empInfo = new EmployeeInfoOutputDto()
        {
            EmpId = employeeId,
            NameAr = emp?.NameAr,
            NameEn = emp?.NameEn
        };
        return empInfo;
    }
    public async Task<GetEmployeeDetailsOutputDto> GetEmployeeDetailsAsync(string employeeId)
    {
        var emp = await _dbcontext.EmployeeDetails.AsNoTracking()
           .Where(m => m.EMP_ID == employeeId).FirstOrDefaultAsync();
        var empImage = await _dbcontext.EmployeeImages.AsNoTracking()
           .Where(m => m.EmployeeId == employeeId).FirstOrDefaultAsync();

        if (emp == null) return null;
        var res = _mapper.Map<EmployeeDetails, GetEmployeeDetailsOutputDto>(emp);

        if (empImage?.Image != null)
            res.EmployeeInfo.Image = Convert.ToBase64String(empImage.Image);

        return res;
    }

    public async Task<bool> UploadEmployeeImageAsync(string employeeId, IFormFile image)
    {
        if (image == null)
            throw new AppException("Must Upload Image");

        var uploadImage = FileHelper.CreateUploadFile(image);
        if (uploadImage?.FileType?.Split('/')?.FirstOrDefault()?.ToLower() != "image")
            throw new AppException("Must Upload Kind of Image");

        if (uploadImage != null && Enum.TryParse(uploadImage?.FileType?.Split('/').Last(), true, out ImageType imgType))
        {
            var foundImage = await _dbcontext.EmployeeImages.Where(m => m.EmployeeId == employeeId).FirstOrDefaultAsync();
            if (foundImage != null)
            {
                foundImage.ImageType = imgType;
                foundImage.Image = uploadImage.FileData;

                _dbcontext.EmployeeImages.Update(foundImage);
                return await _dbcontext.SaveChangesAsync() > 0;
            }
            else
            {
                var id = await _dbcontext.EmployeeImages.AsNoTracking().MaxAsync(m => m.EMPPICSID) ?? "0";
                var res = await _dbcontext.EmployeeImages.AddAsync(new()
                {
                    EMPPICSID = (int.Parse(id) + 1).ToString(),
                    EmployeeId = employeeId,
                    Image = uploadImage.FileData,
                    ImageType = imgType
                });
                return await _dbcontext.SaveChangesAsync() > 0;
            }
        }

        return false;
    }

    public async Task<string> GetEmployeeName(string employeeId)
    {
        var res = await _dbcontext.Employees.AsNoTracking()
            .Where(m => m.EmployeeId == employeeId).Select(m => m.NameAr).FirstOrDefaultAsync();

        return res is null ? throw new AppException("Employee Not Found") : res;
    }

    public async Task<GetEmployeeSearchOutputDto> EmployeeSearchAsync(string? searchKey)
    {
        IQueryable<Employee> q = _dbcontext.Employees.AsNoTracking().AsQueryable();

        if (!string.IsNullOrEmpty(searchKey))
            q = q.Where(m => (m.EmployeeId != null && m.EmployeeId.Contains(searchKey)) ||
                          (m.NameAr != null && m.NameAr.Contains(searchKey)) ||
                          (m.NameEn != null && m.NameEn.Contains(searchKey))
                          );

        q = q.OrderBy(q => q.NameAr);

        var count = q.Count();
        var res = await q.Select(m => new EmployeeSearchOutputDto()
        {
            EmployeeId = m.EmployeeId,
            NameAr = m.NameAr,
            NameEn = m.NameEn,
        }).ToListAsync();

        return new GetEmployeeSearchOutputDto()
        {
            Employees = res,
            Count = count
        };

    }
    public async Task<GetEmployeesTreeOutputDto> GetEmployeeTreeAsync(GetEmployeesTreeInputDto dto)
    {
        IQueryable<Employee> q = _dbcontext.Employees.Include(m => m.Job)
            .Include(m => m.Branch).Include(m => m.Manage)
            .AsNoTracking().AsQueryable();

        if (!string.IsNullOrEmpty(dto.SearchKey))
            q = q.Where(m => (m.NameAr != null && m.NameAr.Contains(dto.SearchKey)) ||
                          (m.NameEn != null && m.NameEn.Contains(dto.SearchKey)) ||
                          (m.Branch != null && m.Branch.NameAr != null && m.Branch.NameAr.Contains(dto.SearchKey)) ||
                          (m.Branch != null && m.Branch.NameEn != null && m.Branch.NameEn.Contains(dto.SearchKey)) ||
                          (m.Manage != null && m.Manage.NameAr != null && m.Manage.NameAr.Contains(dto.SearchKey)) ||
                          (m.Manage != null && m.Manage.NameEn != null && m.Manage.NameEn.Contains(dto.SearchKey)) ||
                          (m.Job != null && m.Job.NameAr != null && m.Job.NameAr.Contains(dto.SearchKey)) ||
                          (m.Job != null && m.Job.NameEn != null && m.Job.NameEn.Contains(dto.SearchKey))
                          );
        if (!string.IsNullOrEmpty(dto.EmployeeId))
            q = q.Where(m => m.EmployeeId != null && m.EmployeeId == dto.EmployeeId);
        //if (!string.IsNullOrEmpty(dto.CompanyId))
        //    q = q.Where(m => m.CompanyId != null && m.CompanyId == dto.CompanyId);
        if (!string.IsNullOrEmpty(dto.BranchId))
            q = q.Where(m => m.BranchId != null && m.BranchId == dto.BranchId);
        //if (!string.IsNullOrEmpty(dto.DepartmentId))
        //    q = q.Where(m => m.DepartmentId != null && m.DepartmentId == dto.DepartmentId);
        if (!string.IsNullOrEmpty(dto.ManageId))
            q = q.Where(m => m.ManageId != null && m.ManageId == dto.ManageId);
        //if (!string.IsNullOrEmpty(dto.NationId))
        //    q = q.Where(m => m.NationId != null && m.NationId == dto.NationId);
        //if (!string.IsNullOrEmpty(dto.KafilId))
        //    q = q.Where(m => m.KafilId != null && m.KafilId == dto.KafilId);
        if (!string.IsNullOrEmpty(dto.JobId))
            q = q.Where(m => m.JobId != null && m.JobId == dto.JobId);

        q = q.OrderBy(q => q.NameAr);

        var count = q.Count();
        var res = await q.Select(m => new EmployeeTreeOutputDto()
        {
            EmployeeId = m.EmployeeId,
            EmployeeNameAr = m.NameAr,
            EmployeeNameEn = m.NameEn,
            BranchId = m.BranchId,
            ManageId = m.ManageId,
            JobId = m.JobId,
            JobNameAr = m.Job != null ? m.Job.NameAr : null,
            JobNameEn = m.Job != null ? m.Job.NameEn : null,

        }).ToListAsync();

        return new GetEmployeesTreeOutputDto()
        {
            Employees = res,
            Count = count
        };
    }

    public async Task<GetEmployeeTreeByIdOutputDto> GetEmployeeTreeByIdAsync(string employeeId)
    {

        var employee = await _dbcontext.Employees.Include(m => m.Job)
            .Include(m => m.Branch).Include(m => m.Manage)
            .AsNoTracking().Where(m => m.EmployeeId == employeeId)
            .Select(m => new GetEmployeeTreeByIdOutputDto()
            {
                EmployeeId = m.EmployeeId,
                EmployeeNameAr = m.NameAr,
                EmployeeNameEn = m.NameEn,
                Blood = m.BLOOD,
                EmployeePersonId = m.PERSON_ID,
                EmployeePersonExpireDate = m.PERSON_EXP,
                Mobile = m.Mobile,
                MobileEmergency = m.SPECIALIZ,
                JobId = m.JobId,
                JobNameAr = m.Job != null ? m.Job.NameAr : null,
                JobNameEn = m.Job != null ? m.Job.NameEn : null,
                BranchId = m.BranchId,
                BranchNameAr = m.Branch != null ? m.Branch.NameAr : null,
                BranchNameEn = m.Branch != null ? m.Branch.NameEn : null,
                ManageId = m.ManageId,
                ManageNameAr = m.Manage != null ? m.Manage.NameAr : null,
                ManageNameEn = m.Manage != null ? m.Manage.NameEn : null,

            })
            .FirstOrDefaultAsync();
        return employee;
    }
}
