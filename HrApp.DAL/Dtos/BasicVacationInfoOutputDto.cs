namespace HrApp.DAL.Dtos;

public class BasicVacationInfoOutputDto
{
    public string EmpId { get; set; }
    public string? NameAr { get; set; }
    public string? NameEn { get; set; }
    public int TotalAllowanceDays { get; set; }
    public int AvailableDays { get { return TotalAllowanceDays - TakenDays; } }
    public int TakenDays { get; set; }

}