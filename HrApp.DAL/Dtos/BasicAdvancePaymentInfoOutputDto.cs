namespace HrApp.DAL.Dtos;

public class BasicAdvancePaymentInfoOutputDto
{
    public string EmpId { get; set; }
    public string? NameAr { get; set; }
    public string? NameEn { get; set; }
    public double LOANSBAL { get; set; } = 0;

}