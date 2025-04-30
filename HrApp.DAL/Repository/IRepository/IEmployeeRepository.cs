using HrApp.DAL.Entities;

namespace HrApp.DAL.Repository.IRepository;

public interface IEmployeeRepository:IGenericRepository<Employee>
{
    Task<AdvancePaymentTotal> GetEmpAdvancePaymentTotal(string empId);

}
