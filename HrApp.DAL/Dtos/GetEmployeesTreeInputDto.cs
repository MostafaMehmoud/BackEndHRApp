using System.ComponentModel.DataAnnotations.Schema;

namespace HrApp.DAL.Dtos;

public class GetEmployeesTreeInputDto
{
    public string? SearchKey { get; set; }
    public string? EmployeeId { get; set; }
    // public string? CompanyId { get; set; }
    public string? BranchId { get; set; }
    public string? ManageId { get; set; }
    // public string? DepartmentId { get; set; }
    // public string? NationId { get; set; }
    // public string? KafilId { get; set; }
    public string? JobId { get; set; }
}
