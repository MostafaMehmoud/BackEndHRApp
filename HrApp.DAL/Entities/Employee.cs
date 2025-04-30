using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrApp.DAL.Entities;

[Table("EMP")]
public class Employee
{
    public Employee()
    {
        RequestAdvancePayments = new HashSet<RequestAdvancePayment>();
        RequestVacations = new HashSet<RequestVacation>();
        EmployeeAttendances = new HashSet<EmployeeAttendance>();
    }
    [Key]
    [Column("EMP_ID")]
    public string? EmployeeId { get; set; }
    [Column("EMP_NAME")]
    public string? NameAr { get; set; }
    [Column("EMP_NAME_E")]
    public string? NameEn { get; set; }
    [Column("MOBIL")]
    public string? Mobile { get; set; }
    [Column("BLOOD")]
    public string? BLOOD { get; set; }

    [Column("PERSON_ID")]
    public string? PERSON_ID { get; set; }
    [Column("PERSON_EXP")]
    public string? PERSON_EXP { get; set; }

    [Column("SPECIALIZ")]
    public string? SPECIALIZ { get; set; }
    [Column("CNT_ID")]
    public string? CompanyId { get; set; }
    [Column("BRANCH_ID")]
    [ForeignKey("BRANCH_ID")]
    public string? BranchId { get; set; }
    public virtual  Branch Branch { get; set; }
    [Column("MNG_ID")]
    [ForeignKey("MNG_ID")]

    public string? ManageId { get; set; }
    public virtual Manage Manage { get; set; }
    [Column("DEPT_ID")]
    public string? DepartmentId { get; set; }
    [Column("NATION_ID")]
    public string? NationId { get; set; }
    [Column("KAFIL_ID")]
    public string? KafilId { get; set; }
    [Column("JOB_ID")]
    [ForeignKey("JOB_ID")]
    public string? JobId { get; set; }
    public virtual Job Job { get; set; }



    public ICollection<RequestAdvancePayment> RequestAdvancePayments { get; set; }
    public ICollection<RequestVacation> RequestVacations { get; set; }
    public ICollection<EmployeeAttendance> EmployeeAttendances { get; set; }
}
