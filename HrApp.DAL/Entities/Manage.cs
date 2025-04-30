
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrApp.DAL.Entities;

[Table("HR_MANAGE")]
public class Manage
{
    [Key]
    [Column("MNG_ID")]
    public string? Id { get; set; }
    [Column("MNG_NAME")]
    public string? NameAr { get; set; }
    [Column("MNG_NAME_E")]
    public string? NameEn { get; set; }
}
