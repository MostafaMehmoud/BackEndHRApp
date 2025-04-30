namespace HrApp.DAL.Dtos;

public class GetEmployeeSearchOutputDto
{
    public List<EmployeeSearchOutputDto> Employees { get; set; } = new();
    public int Count { get; set; } = 0;
}

public class EmployeeSearchOutputDto
{
    public string? EmployeeId { get; set; }
    public string? NameAr { get; set; }
    public string? NameEn { get; set; }

}
