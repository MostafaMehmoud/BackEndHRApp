using HrApp.DAL.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrApp.DAL.Entities;

[Table("REQUEST_VACATION")]
public class RequestVacation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID")]
    public int Id { get; set; }
    [Column("START_DATE")]
    public DateTime? StartDate { get; set; }
    [Column("NUMBER_OF_DAYS")]
    public int? NumberOfDays { get; set; }
    [Column("DETAILS")]
    public string? Details { get; set; }
    [Column("VACATION_TYPE")]
    public VacationType? VacationType { get; set; }
    [Column("APPROVE_STATUS")]
    public ApproveType ApproveStatus { get; set; } = ApproveType.Pinding;
    [Column("APPROVE_DETAILS")]
    public string? ApproveDetails { get; set; }
    [Column("EMP_ID")]
    [ForeignKey("Emp_Id")]
    public string? EmployeeId { get; set; }
    public virtual Employee? Employee { get; set; }
    [Column("UPLOAD_FILE_ID")]
    public string? UploadFileId { get; set; }
}
