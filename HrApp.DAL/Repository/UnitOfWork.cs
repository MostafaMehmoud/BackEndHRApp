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

    public IManageRepository ManageRepository { get; private set; }

    public IJobRepository JobRepository { get; private set; }

    public IReligionRepository ReligionRepository { get; private set; }

    public INationRepository NationRepository { get; private set; }

    public IBranchRepository BranchRepository { get; private set; }

    public ICityRepository CityRepository { get; private set; }

    public ICountryRepository CountryRepository { get; private set; }
    public ICollegeRepository CollegeRepository { get; private set; }

    public INeighborRepository NeighborRepository { get; private set; }

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
        ManageRepository= ManageRepository ?? new ManageRepository(dbContext);
        JobRepository=JobRepository ?? new JobRepository(dbContext);
        ReligionRepository = ReligionRepository??new ReligionRepository(dbContext);

        NationRepository=NationRepository??new NationRepository(dbContext);
        BranchRepository= BranchRepository??new BranchRepository(dbContext);
        CityRepository= CityRepository??new CityRepository(dbContext);
        CountryRepository=CountryRepository??new CountryRepository(dbContext); 
        CollegeRepository= CollegeRepository??new CollegeRepository(dbContext);
        NeighborRepository= NeighborRepository??new NeighborRepository(dbContext);
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
