namespace HrApp.DAL.Dtos;

public class SignInOutputDto
{
    public string AccessToken { get; set; }
    public EmployeeInfoOutputDto EmployeeInfo { get; set; }
    public List<DateTime?> LoginDateList { get; set; } = new();
}

