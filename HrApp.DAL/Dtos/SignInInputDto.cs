using System.ComponentModel.DataAnnotations;

namespace HrApp.DAL.Dtos;

public class SignInInputDto
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}
