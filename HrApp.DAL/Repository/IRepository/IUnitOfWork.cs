
namespace HrApp.DAL.Repository.IRepository;

public interface IUnitOfWork
{
    IEmployeeRepository EmployeeRepository { get; }
    IVacationRepository VacationRepository { get; }
    IAdvancePaymentRepository AdvancePaymentRepository { get; }
    ILetterRepository LetterRepository { get; }
    ICustodyRepository CustodyRepository { get; }
    IPermissionRepository PermissionRepository { get; }
    IUploadFileRepository UploadFileRepository { get; }
    IEmployeeAttendanceRepository EmployeeAttendanceRepository { get; }
    ICompanyRepository CompanyRepository { get; }   
    IManageRepository ManageRepository { get; } 
    IJobRepository JobRepository { get; }   
    IReligionRepository ReligionRepository { get; } 
    INationRepository NationRepository { get; }
    IBranchRepository BranchRepository { get; }
    ICityRepository CityRepository { get; }
    ICountryRepository CountryRepository { get; }
    ICollegeRepository CollegeRepository { get; }
    INeighborRepository NeighborRepository { get; }  
    IKafilRepository KafilRepository { get; }   
    public Task StartTransaction();
    public Task CommitTransaction();
    public Task RollbackTransaction();
}
