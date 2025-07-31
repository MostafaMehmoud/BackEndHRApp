
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HrApp.DAL.Entities;

public class Department
{
    [Key]
    [Column("DEPT_ID")]
    public string? Id { get; set; }

    [Column("DEPT_NAME")]
    public string? NameAr { get; set; }

    [Column("DEPT_NAME_E")]
    public string? NameEn { get; set; }

    [Column("MNG_ID")]
    public string? ManagerId { get; set; }

    // Example navigation property
    public virtual Manage? Manage { get; set; }
}

