using HrApp.DAL.Repository.IRepository;
using HrApp.DAL.Entities;
using HrApp.DAL.Data;

namespace HrApp.API.Repos;

public class EmployeeDetailsRepository : GenericRepository<EmployeeDetails>, IEmployeeDetailsRepository
{
    public EmployeeDetailsRepository(HrAppDbContext dbContext) : base(dbContext)
    {
    }
}
