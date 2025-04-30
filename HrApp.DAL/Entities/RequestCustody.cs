using HrApp.DAL.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HrApp.DAL.Entities;

[Table("REQUEST_CUSTODY")]
public class RequestCustody
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID")]
    public int Id { get; set; }
    [Column("DETAILS")]
    public string? Details { get; set; }
    [Column("IS_EMPLOYEE_AGREE")]
    public int IsEmployeeAgree { get; set; }
    [Column("APPROVE_STATUS")]
    public ApproveType ApproveStatus { get; set; } = ApproveType.Pinding;
    [Column("APPROVE_DETAILS")]
    public string? ApproveDetails { get; set; }
    [Column("EMP_ID")]
    public string? EmployeeId { get; set; }
    public virtual Employee? Employee { get; set; }
    [Column("UPLOAD_FILE_ID")]
    public string? UploadFileId { get; set; }
}
