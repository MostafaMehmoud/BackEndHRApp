using HrApp.DAL.Repository.IRepository;
using HrApp.DAL.Entities;
using HrApp.DAL.Data;

namespace HrApp.API.Repos;

public class EmployeeImageRepository : GenericRepository<EmployeeImage>, IEmployeeImageRepository
{
    public EmployeeImageRepository(HrAppDbContext dbContext) : base(dbContext)
    {
    }
}
