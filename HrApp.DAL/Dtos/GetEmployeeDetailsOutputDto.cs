namespace HrApp.DAL.Dtos;

public class GetEmployeeDetailsOutputDto
{
    public EmployeeInfoDto EmployeeInfo { get; set; }
    public EmployeeContractInfoDto ContractInfo { get; set; }
    public EmployeeIdentityInfoDto IdentityInfo { get; set; }
    public EmployeePassportInfoDto PassportInfo { get; set; }
    public EmployeeCarInfoDto CarInfo { get; set; }
}
public class EmployeeInfoDto
{
    public string Id { get; set; }
    public string NameAr { get; set; }
    public string NameEn { get; set; }
    public string BloodType { get; set; }
    public string Mobile { get; set; }
    public string MobileEmergency { get; set; }
    public string JobNameAr { get; set; }
    public string JobNameEn { get; set; }
    public string DepatmentNameAr { get; set; }
    public string DepatmentNameEn { get; set; }
    public string Image { get; set; }
}
public class EmployeeContractInfoDto
{
    public string StartDate { get; set; }
    public string EndDate { get; set; }
}
public class EmployeeIdentityInfoDto
{
    public string IdentityNumber { get; set; }
    public string ExpiredDate { get; set; }
}
public class EmployeePassportInfoDto
{
    public string PassportNumber { get; set; }
    public string ExpiredDate { get; set; }
}
public class EmployeeCarInfoDto
{
    public string CarNumber { get; set; }
    public string ContractCarEndDate { get; set; }
    public string CheckCarEndDate { get; set; }

}