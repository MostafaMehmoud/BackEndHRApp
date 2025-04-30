
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HrApp.DAL.Entities;

[Table("HR_COMPANIES")]
public class Company
{
    [Key]
    [Column("CNT_ID")]
    public string? Id { get; set; }
    [Column("CNT_NAME")]
    public string? NameAr { get; set; }
    [Column("CNT_NAME_E")]
    public string? NameEn { get; set; }
}
