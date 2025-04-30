using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HrApp.DAL.Dtos;

public class CreateRequestCustodyInputDto
{
    [Required]
    public string Details { get; set; }
    [Required]
    public bool IsEmployeeAgree { get; set; }

    public IFormFile? File { get; set; }

}
