using HrApp.DAL.Repository.IRepository;
using HrApp.DAL.Entities;
using HrApp.DAL.Data;

namespace HrApp.API.Repos;

public class PermissionRepository : GenericRepository<RequestPermission>, IPermissionRepository
{
    public PermissionRepository(HrAppDbContext dbContext) : base(dbContext)
    {
    }
}
