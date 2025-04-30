using HrApp.DAL.Repository.IRepository;
using HrApp.DAL.Entities;
using HrApp.DAL.Data;

namespace HrApp.API.Repos;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(HrAppDbContext dbContext) : base(dbContext)
    {
    }
}
