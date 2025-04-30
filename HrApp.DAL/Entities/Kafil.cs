
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HrApp.DAL.Entities;

[Table("HR_KAFIL")]
public class Kafil
{
    [Key]
    [Column("KAFIL_ID")]
    public string? Id { get; set; }
    [Column("KAFIL_NAME")]
    public string? NameAr { get; set; }
    [Column("KAFIL_NAME_E")]
    public string? NameEn { get; set; }
    [Column("KAFIL_IDNUM")]
    public string? IdNumber { get; set; }
    [Column("KAFIL_MOBIL")]
    public string? Mobile { get; set; }
}
