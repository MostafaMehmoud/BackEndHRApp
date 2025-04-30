using HrApp.DAL.Repository.IRepository;
using HrApp.DAL.Entities;
using HrApp.DAL.Data;

namespace HrApp.API.Repos;

public class CustodyRepository : GenericRepository<RequestCustody>, ICustodyRepository
{
    public CustodyRepository(HrAppDbContext dbContext) : base(dbContext)
    {
    }
}
