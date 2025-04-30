using HrApp.DAL.Enums;

namespace HrApp.DAL.Dtos;

public class GetEmployeeVacationsOutputDto
{
    public List<EmpRequestVacationDto> RequestVacations { get; set; } = new();
}
public class EmpRequestVacationDto
{
    public int Id { get; set; }

    public DateTime StartDate { get; set; }

    public int NumberOfDays { get; set; }

    public string Details { get; set; }
    public VacationType VacationType { get; set; }
    public ApproveType ApproveStatus { get; set; }
    public string ApproveDetails { get; set; }
    public string? UploadFileId { get; set; }

}