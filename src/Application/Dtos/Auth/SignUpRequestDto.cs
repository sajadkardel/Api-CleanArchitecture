namespace Application.Dtos.Auth;

public class SignUpRequestDto
{
    public string UserName { get; set; }
    public string FisrtName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
