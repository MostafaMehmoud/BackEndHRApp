using HrApp.DAL.Repository.IRepository;
using HrApp.DAL.Data;
using HrApp.DAL.Repository;

namespace HrApp.API.Repos;

public class UnitOfWork : IUnitOfWork
{
    private readonly HrAppDbContext _dbContext;
    public IEmployeeRepository EmployeeRepository { get; private set; }
    public IVacationRepository VacationRepository { get; private set; }
    public IAdvancePaymentRepository AdvancePaymentRepository { get; private set; }
    public ILetterRepository LetterRepository { get; private set; }
    public ICustodyRepository CustodyRepository { get; private set; }
    public IPermissionRepository PermissionRepository { get; private set; }
    public IUploadFileRepository UploadFileRepository { get; private set; }

    public IEmployeeAttendanceRepository EmployeeAttendanceRepository { get; private set; }

    public ICompanyRepository CompanyRepository { get; private set; }

    public UnitOfWork(HrAppDbContext dbContext)
    {
        _dbContext = dbContext;
        EmployeeRepository = EmployeeRepository ?? new EmployeeRepository(dbContext);
        VacationRepository = VacationRepository ?? new VacationRepository(dbContext);
        AdvancePaymentRepository = AdvancePaymentRepository ?? new AdvancePaymentRepository(dbContext);
        LetterRepository = LetterRepository ?? new LetterRepository(dbContext);
        PermissionRepository = PermissionRepository ?? new PermissionRepository(dbContext); 
        CustodyRepository   = CustodyRepository ?? new CustodyRepository(dbContext);
        UploadFileRepository = UploadFileRepository ?? new UploadFileRepository(dbContext);
        EmployeeAttendanceRepository = EmployeeAttendanceRepository ?? new EmployeeAttendanceRepository(dbContext);
        CompanyRepository=CompanyRepository??new CompanyRepository(dbContext);
    }
    public async Task CommitTransaction()
    {
        await _dbContext.Database.CommitTransactionAsync();
    }

    public async Task RollbackTransaction()
    {
        await _dbContext.Database.RollbackTransactionAsync();
    }


    public async Task StartTransaction()
    {
       await _dbContext.Database.BeginTransactionAsync();
    }
}
