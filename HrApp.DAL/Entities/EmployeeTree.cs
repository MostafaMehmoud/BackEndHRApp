
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrApp.DAL.Entities;
[Keyless]
[Table("HR_V_EMP_TREE")]
public class EmployeeTree
{
    [Column("EMP_ID")]
    public string? EmployeeId { get; set; }
    [Column("EMP_NAME")]
    public string? EmployeeNameAr { get; set; }
    [Column("EMP_NAME_E")]
    public string? EmployeeNameEn { get; set; }
    [Column("BLOOD")]
    public string? Blood { get; set; }
    [Column("PERSON_ID")]
    public string? EmployeePersonId { get; set; }
    [Column("PERSON_EXP")]
    public string? EmployeePersonExpireDate { get; set; }
    [Column("MOBIL")]
    public string? Mobil { get; set; }
    [Column("SPECIALIZ")]
    public string? MobileEmergency { get; set; }
    [Column("CNT_ID")]
    public string? CompanyId { get; set; }
    [Column("CNT_NAME")]
    public string? CompanyNameAr { get; set; }
    [Column("CNT_NAME_E")]
    public string? CompanyNameEn { get; set; }
    [Column("BRANCH_ID")]
    public string? BranchId { get; set; }
    [Column("BRANCH_NAME")]
    public string? BranchNameAr { get; set; }
    [Column("BRANCH_NAME_E")]
    public string? BranchNameEn { get; set; }
    [Column("MNG_ID")]
    public string? ManageId { get; set; }
    [Column("MNG_NAME")]
    public string? ManageNameAr { get; set; }
    [Column("MNG_NAME_E")]
    public string? ManageNameEn { get; set; }
    [Column("NATION_ID")]
    public string? NationId { get; set; }
    [Column("NATION_NAME")]
    public string? NationNameAr { get; set; }
    [Column("NATION_NAME_E")]
    public string? NationNameEn { get; set; }
    [Column("DEPT_ID")]
    public string? DepartmentId { get; set; }
    [Column("DEPT_NAME")]
    public string? DepartmentNameAr { get; set; }
    [Column("DEPT_NAME_E")]
    public string? DepartmentNameEn { get; set; }
    [Column("JOB_ID")]
    public string? JobId { get; set; }
    [Column("JOB_NAME")]
    public string? JobNameAr { get; set; }
    [Column("JOB_NAME_E")]
    public string? JobNameEn { get; set; }
    [Column("KAFIL_ID")]
    public string? KafilId { get; set; }
    [Column("KAFIL_NAME")]
    public string? KafilNameAr { get; set; }
    [Column("KAFIL_NAME_E")]
    public string? KafilNameEn { get; set; }
}

