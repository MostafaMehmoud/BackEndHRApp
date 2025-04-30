using HrApp.DAL.Enums;

namespace HrApp.DAL.Dtos;

public class GetPindingVacationsOutputDto
{
    public int Count { get; set; } = 0;
    public List<PindingVacationOutputDto> RequestVacations { get; set; } = new();

}
public class PindingVacationOutputDto
{
    public int Id { get; set; }

    public DateTime StartDate { get; set; }

    public int NumberOfDays { get; set; }

    public string Details { get; set; }
    public VacationType VacationType { get; set; }
    public string EmployeeId { get; set; }
    public string EmployeeNameAr { get; set; }
    public string EmployeeNameEn { get; set; }

    public int TotalAllowanceDays { get; set; }
    public int AvailableDays { get { return TotalAllowanceDays - TakenDays; } }
    public int TakenDays { get; set; }
    public string? UploadFileId { get; set; }

}
