using HrApp.DAL.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrApp.DAL.Entities;
[Table("HR_EMPLOANS")]
public class RequestAdvancePayment
{
    [Key]
    [Column("EMPLOANSID")]
    public string? Id { get; set; }
   [Column("LOANVALUE")]
    public double AdvancePaymentValue { get; set; }
    [Column("KESTVALUE")]
    public double InstallmentValue { get; set; }
    [Column("KESTNO")]
    public int NumberOfInstallment { get; set; }
    [Column("LASTKESTVALUE")]
    public double LastInstallmentValue { get; set; }
    [Column("KESTSTART")]
    public string? StartDate { get; set; }
    [Column("ISAPROVED")]
    public ApproveType ApproveStatus { get; set; } = ApproveType.Pinding;

    [Column("EMPID")]
    [ForeignKey("Emp_Id")]
    public string? EmployeeId { get; set; }
    public virtual Employee? Employee { get; set; }



}
