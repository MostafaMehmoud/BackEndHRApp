
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrApp.DAL.Entities;

[Table("HR_JOBS")]
public class Job
{
    [Key]
    [Column("JOB_ID")]
    public string? Id { get; set; }
    [Column("JOB_NAME")]
    public string? NameAr { get; set; }
    [Column("JOB_NAME_E")]
    public string? NameEn { get; set; }
}
