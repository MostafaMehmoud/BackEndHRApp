using HrApp.DAL.Repository.IRepository;
using HrApp.DAL.Entities;
using HrApp.DAL.Data;

namespace HrApp.API.Repos;

public class VacationRepository : GenericRepository<RequestVacation>, IVacationRepository
{
    public VacationRepository(HrAppDbContext dbContext) : base(dbContext)
    {
    }
}
