using HrApp.DAL.Enums;

namespace HrApp.DAL.Dtos;

public class GetEmployeePermissionsOutputDto
{
    public List<EmpRequestPermissionDto> RequestPermissions { get; set; } = new();
}
public class EmpRequestPermissionDto
{
    public int Id { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Details { get; set; }
    public ApproveType ApproveStatus { get; set; }
    public string ApproveDetails { get; set; }
    public string? UploadFileId { get; set; }



}