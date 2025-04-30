using HrApp.DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace HrApp.DAL.Dtos;

public class GetEmployeeAdvancePaymentsOutputDto
{
    public List<EmpAdvancePaymentDto> AdvancePayments { get; set; } = new();
}
public class EmpAdvancePaymentDto
{
    public string Id { get; set; }
    public double AdvancePaymentValue { get; set; }
    public double InstallmentValue { get; set; }
    public int NumberOfInstallment { get; set; }
    public double LastInstallmentValue { get; set; }
    public string StartDate { get; set; }
    public ApproveType ApproveStatus { get; set; }

}