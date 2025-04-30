using HrApp.DAL.Repository.IRepository;
using HrApp.DAL.Entities;
using HrApp.DAL.Data;

namespace HrApp.API.Repos;

public class LetterRepository : GenericRepository<RequestLetter>, ILetterRepository
{
    public LetterRepository(HrAppDbContext dbContext) : base(dbContext)
    {
    }
}
