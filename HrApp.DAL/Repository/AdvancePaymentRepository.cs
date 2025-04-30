using HrApp.DAL.Repository.IRepository;
using HrApp.DAL.Entities;
using HrApp.DAL.Data;

namespace HrApp.API.Repos;

public class AdvancePaymentRepository : GenericRepository<RequestAdvancePayment>, IAdvancePaymentRepository
{
    public AdvancePaymentRepository(HrAppDbContext dbContext) : base(dbContext)
    {
    }
}
