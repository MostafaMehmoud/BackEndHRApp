using HrApp.DAL.Enums;

namespace HrApp.DAL.Dtos;

public class GetEmployeeCustodiesOutputDto
{
    public List<EmpRequestCustodyDto> RequestCustodies { get; set; } = new();
}
public class EmpRequestCustodyDto
{
    public int Id { get; set; }
    public string Details { get; set; }
    public bool IsEmployeeAgree { get; set; }
    public ApproveType ApproveStatus { get; set; }
    public string ApproveDetails { get; set; }
    public string? UploadFileId { get; set; }

}