namespace HrApp.DAL.Dtos;

public class GetPindingAdvancePaymentsOutputDto
{
    public int Count { get; set; } = 0;
    public List<PindingRequestAdvancePaymentDto> RequestAdvancePayments { get; set; } = new();

}
public class PindingRequestAdvancePaymentDto
{
    public int Id { get; set; }

    public double AdvancePaymentValue { get; set; }
    public double InstallmentValue { get; set; }
    public int NumberOfInstallment { get; set; }
    public double LastInstallmentValue { get; set; }
    public string StartDate { get; set; }
    public string EmployeeId { get; set; }
    public string EmployeeNameAr { get; set; }
    public string EmployeeNameEn { get; set; }

}