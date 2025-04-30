using System.ComponentModel.DataAnnotations.Schema;

namespace HrApp.DAL.Dtos;

public class GetUserByUsernameOrEmpIdOutputDto
{
    public string UserId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; }
    public string EmployeeId { get; set; }

}
