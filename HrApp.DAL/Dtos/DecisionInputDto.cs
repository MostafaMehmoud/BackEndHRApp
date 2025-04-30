using System.ComponentModel.DataAnnotations;

namespace HrApp.DAL.Dtos;

public class DecisionInputDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public bool Accepted { get; set; }
    public string Note { get; set; }
}
