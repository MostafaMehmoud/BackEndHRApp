
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrApp.DAL.Entities;

[Table("HR_BRANCH")]
public class Branch
{
    public Branch()
    {
        company=new Company();  
    }
    [Key]
    [Column("BRANCH_ID")]
    public string? Id { get; set; }
    [Column("BRANCH_NAME")]
    public string? NameAr { get; set; }
    [Column("BRANCH_NAME_E")]
    public string? NameEn { get; set; }
    [Column("CNT_ID")]
    [ForeignKey("CNT_ID")]
    public string? CompanyId { get; set; }
    public virtual Company company { get; set; }
}
