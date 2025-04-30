using HrApp.DAL.Repository.IRepository;
using HrApp.DAL.Entities;
using HrApp.DAL.Data;

namespace HrApp.API.Repos;

public class AuthLogRepository : GenericRepository<AuthLog>, IAuthLogRepository
{
    public AuthLogRepository(HrAppDbContext dbContext) : base(dbContext)
    {
    }
}
