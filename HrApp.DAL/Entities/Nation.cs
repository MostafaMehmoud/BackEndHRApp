
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrApp.DAL.Entities;

[Table("HR_NATION")]
public class Nation
{
    [Key]
    [Column("NATION_ID")]
    public string? Id { get; set; }
    [Column("NATION_NAME")]
    public string? NameAr { get; set; }
    [Column("NATION_NAME_E")]
    public string? NameEn { get; set; }
}
