using HrApp.DAL.Enums;

namespace HrApp.DAL.Dtos;

public class GetPindingPermissionsOutputDto
{
    public int Count { get; set; } = 0;
    public List<PindingPermissionOutputDto> RequestPermissions { get; set; } = new();

}
public class PindingPermissionOutputDto
{

    public int Id { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Details { get; set; }
    public string? UploadFileId { get; set; }

    public string EmployeeId { get; set; }
    public string EmployeeNameAr { get; set; }
    public string EmployeeNameEn { get; set; }


}
