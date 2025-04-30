using HrApp.DAL.Repository.IRepository;
using HrApp.DAL.Entities;
using HrApp.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace HrApp.API.Repos;

public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(HrAppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<AdvancePaymentTotal> GetEmpAdvancePaymentTotal(string empId)
    {
      return await _dbContext.AdvancePaymentTotals.Where(m=>m.EmployeeId == empId).FirstOrDefaultAsync();
    }
}
