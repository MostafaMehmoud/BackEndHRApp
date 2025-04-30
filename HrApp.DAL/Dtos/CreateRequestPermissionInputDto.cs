using HrApp.DAL.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HrApp.DAL.Dtos;

public class CreateRequestPermissionInputDto
{
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime? StartDate { get; set; }
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime? EndDate { get; set; }
    public string? Details { get; set; }
    public IFormFile? File { get; set; }


}
