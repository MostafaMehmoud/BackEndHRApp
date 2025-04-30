using System.ComponentModel.DataAnnotations;

namespace HrApp.DAL.Dtos;

public class CreateRequestAdvancePaymentInputDto
{
    [Required]
    public double AdvancePaymentValue { get; set; }
    [Required]
    public double InstallmentValue { get; set; }
    [Required]
    public int NumberOfInstallment { get; set; }
    [Required]
    public double LastInstallmentValue { get; set; }
    [Required]
    public DateTime StartDate { get; set; }

}
