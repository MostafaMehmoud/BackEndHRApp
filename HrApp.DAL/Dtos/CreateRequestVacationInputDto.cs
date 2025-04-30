using HrApp.DAL.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HrApp.DAL.Dtos;

public class CreateRequestVacationInputDto
{
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public int NumberOfDays { get; set; }
    public string? Details { get; set; }
    [Required]
    public VacationType VacationType { get; set; }
    public IFormFile? File { get; set; }


}
