namespace DreamStayWebApi.Models;

public sealed class RegisterModel
{
	public string FIO { get; set; } = default!;
	public string Login { get; set; } = default!;
	public string Password { get; set; } = default!;
	public string ConfirmPassword { get; set; } = default!;
}
