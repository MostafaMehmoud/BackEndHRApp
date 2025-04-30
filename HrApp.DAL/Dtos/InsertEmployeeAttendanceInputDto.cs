using System.ComponentModel.DataAnnotations;

namespace HrApp.DAL.Dtos;

public class InsertEmployeeAttendanceInputDto
{
    public List<EmployeeAttendanceInputDto> EmployeeAttendances { get; set; } = new();
}
public class EmployeeAttendanceInputDto
{
    [Required]
    public string? EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
    [Required]
    public DateOnly Date { get; set; }
    public TimeOnly? Start { get; set; }
    public TimeOnly? Leave { get; set; }
    public int EarlyByMinute { get; set; } = 0;
    public int LateByMinute { get; set; } = 0;
    public int WorkingTimeByMinute { get; set; } = 0;
    [Required]
    public int ProjectId { get; set; }
    [Required]
    public bool Absent { get; set; }
}