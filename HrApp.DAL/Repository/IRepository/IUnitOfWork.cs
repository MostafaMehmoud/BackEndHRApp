
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
    public Task StartTransaction();
    public Task CommitTransaction();
    public Task RollbackTransaction();
}
