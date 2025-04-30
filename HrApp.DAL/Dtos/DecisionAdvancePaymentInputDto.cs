using System.ComponentModel.DataAnnotations;

namespace HrApp.DAL.Dtos;

public class DecisionAdvancePaymentInputDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public bool Accepted { get; set; }
}
