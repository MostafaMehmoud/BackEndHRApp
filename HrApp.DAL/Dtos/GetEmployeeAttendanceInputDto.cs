namespace HrApp.DAL.Dtos;

public class GetEmployeeAttendanceInputDto
{
    public string? EmployeeId { get; set; }
    public int? ProjectId { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }

}
