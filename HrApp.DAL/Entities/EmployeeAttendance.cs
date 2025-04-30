using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrApp.DAL.Entities;

[Table("EMPLOYEE_ATTENDANCE")]
public class EmployeeAttendance
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }


    [Column("ATTENDANCE_DATE")]
    public DateTime Date { get; set; }

    [Column("ATTENDANCE_START")]
    public DateTime? Start { get; set; }
    [Column("ATTENDANCE_LEAVE")]
    public DateTime? Leave { get; set; }
    [Column("ATTENDANCE_EARLY_MINUTE")]
    public int EarlyByMinute { get; set; } = 0;
    [Column("ATTENDANCE_LATE_MINUTE")]
    public int LateByMinute { get; set; } = 0;
    [Column("WORKING_TIME_MINUTE")]
    public int WorkingTimeByMinute { get; set; } = 0;
    [Column("PROJECT_ID")]
    public int? ProjectId { get; set; }
    [Column("ABSENT")]
    public int Absent { get; set; }
    [Column("BY_EMP_ID")]
    public string? ByEmployeeId { get; set; }

    [ForeignKey("EMP_ID")]
    [Column("EMP_ID")]
    public string? EmployeeId { get; set; }
    public virtual Employee? Employee { get; set; }
}
