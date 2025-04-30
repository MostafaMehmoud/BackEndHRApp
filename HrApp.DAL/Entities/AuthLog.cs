using HrApp.DAL.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrApp.DAL.Entities;

[Table("AUTH_LOG")]
public class AuthLog
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }
    [Column("EMP_ID")]
    public string? EmpId { get; set; }
    [Column("AUTH_TYPE")]
    public AuthTypeType AuthType { get; set; }
    [Column("AUTH_DATE")]
    public DateTime? AuthDate { get; set; }
}
