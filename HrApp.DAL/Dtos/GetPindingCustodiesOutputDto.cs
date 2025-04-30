using HrApp.DAL.Enums;

namespace HrApp.DAL.Dtos;
public class GetPindingCustodiesOutputDto
{
    public int Count { get; set; } = 0;
    public List<PindingCustodyOutputDto> RequestCustodies { get; set; } = new();

}
public class PindingCustodyOutputDto
{
    public int Id { get; set; }
    public string Details { get; set; }
    public bool IsEmployeeAgree { get; set; }
    public string? UploadFileId { get; set; }

    public string EmployeeId { get; set; }
    public string EmployeeNameAr { get; set; }
    public string EmployeeNameEn { get; set; }

}
