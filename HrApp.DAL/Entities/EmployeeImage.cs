using HrApp.DAL.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrApp.DAL.Entities;
[Table("HR_EMPPICS")]
public class EmployeeImage
{ 
    [Column("EMP_ID")]
    public string? EmployeeId { get; set; }
    [Column("EMPIMG")]
    public byte[]?  Image { get; set; }
    [Column("EMPIMGTYPE")]
    public ImageType? ImageType { get; set; }
    [Key]
    [Column("EMPPICSID")]
    public string? EMPPICSID { get; set; }
}
