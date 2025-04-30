namespace HrApp.DAL.Dtos;

public class GetEmployeeAttendanceOutputDto
{
    public List<EmployeeAttendanceOutputDto> EmployeeAttendances { get; set; } = new();
}

public class EmployeeAttendanceOutputDto
{
    public int Id { get; set; }
    public string? EmployeeId { get; set; }
    public string? EmployeeNameAr { get; set; }
    public string? EmployeeNameEn { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly? Start { get; set; }
    public TimeOnly? Leave { get; set; }
    public int EarlyByMinute { get; set; } = 0;
    public int LateByMinute { get; set; } = 0;
    public int WorkingTimeByMinute { get; set; } = 0;
    public int ProjectId { get; set; }
    public bool Absent { get; set; }
}