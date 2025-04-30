using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrApp.DAL.Entities;
[Keyless]
[Table("VWEMPLOANSREQ")]
public class AdvancePaymentTotal
{
    [Column("EMP_ID")]
    public string? EmployeeId { get; set; }
    [Column("EMP_NAME")] 
    public string? NameAr { get; set; }
    [Column("EMP_NAME_E")]
    public string? NameEn { get; set; }
    [Column("TOTLOANVALUE")]
    public string? LOANSTOT { get; set; }
    
    [Column("SUMLOANPAY")]
    public double? SUMLOANPAY { get; set; }
    [Column("LOANSBAL")]
    public double? LOANSBAL { get; set; }
}
