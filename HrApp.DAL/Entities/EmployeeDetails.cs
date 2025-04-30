using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrApp.DAL.Entities;
[Keyless]
[Table("HR_V_EMP_PROFILE")]
public class EmployeeDetails
{
    [Column("EMP_ID")]
    public string? EMP_ID { get; set; }
    [Column("EMP_NAME")]
    public string? EMP_NAME { get; set; }
    [Column("EMP_NAME_E")]
    public string? EMP_NAME_E { get; set; }
    [Column("CARD_ID")]
    public string? CARD_ID { get; set; }
    [Column("CARD_EXP")]
    public string? CARD_EXP { get; set; }
    [Column("PASS_ID")]
    public string? PASS_ID { get; set; }
    [Column("PASS_EXP")]
    public string? PASS_EXP { get; set; }
    [Column("CONTRACT_DATE")]
    public string? CONTRACT_DATE { get; set; }
    [Column("CONTRACT_ENDDATE")]
    public string? CONTRACT_ENDDATE { get; set; }
    [Column("ENDDATE")]
    public string? ENDDATE { get; set; }
    [Column("CHECKDATE")]
    public string? CHECKDATE { get; set; }
    [Column("V5")]
    public string? V5 { get; set; }

    [Column("BLOOD")]
    public string? BLOOD { get; set; }

    [Column("PERSON_ID")]
    public string? PERSON_ID { get; set; }
    [Column("PERSON_EXP")]
    public string? PERSON_EXP { get; set; }
    [Column("MOBIL")]
    public string? MOBIL { get; set; }
    [Column("SPECIALIZ")]
    public string? SPECIALIZ { get; set; }

    [Column("JOB_NAME")]
    public string? JOB_NAME { get; set; }
    [Column("JOB_NAME_E")]
    public string? JOB_NAME_E { get; set; }
    [Column("DEPT_NAME")]
    public string? DEPT_NAME { get; set; }
    [Column("DEPT_NAME_E")]
    public string? DEPT_NAME_E { get; set; }


}
