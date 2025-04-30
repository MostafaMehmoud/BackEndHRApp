using HrApp.DAL.Repository.IRepository;
using HrApp.DAL.Entities;
using HrApp.DAL.Data;

namespace HrApp.API.Repos;

public class UploadFileRepository : GenericRepository<UploadFile>, IUploadFileRepository
{
    public UploadFileRepository(HrAppDbContext dbContext) : base(dbContext)
    {
    }
}
