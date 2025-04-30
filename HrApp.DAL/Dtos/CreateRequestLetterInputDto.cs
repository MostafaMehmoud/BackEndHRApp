using HrApp.DAL.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HrApp.DAL.Dtos;

public class CreateRequestLetterInputDto
{
    [Required]
    public string? DestinationName { get; set; }
    [Required]
    public bool IsRequiredCCC { get; set; }
    public string? Details { get; set; }
    [Required]
    public LetterType LetterType { get; set; }

    public IFormFile? File { get; set; }


}
