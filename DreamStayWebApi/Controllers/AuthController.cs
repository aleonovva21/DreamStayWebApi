using DreamStay.Domain.AuthTokenProviders;
using DreamStayWebApi.Models.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using User = DreamStay.Domain.Models.User;

namespace DreamStayWebApi.Controllers;

[Route("auth")]
[ApiController]
public sealed class AuthController(IAuthTokenProvider tokenProvider, HostelContext context) : ControllerBase
{
	private readonly IAuthTokenProvider _tokenProvider = tokenProvider;
	private readonly HostelContext _context = context;

	[HttpPost("login")]
	public async Task<IActionResult> Login(LoginModel model)
	{
		var credential = await _context.Credentials.SingleOrDefaultAsync(credential =>
			credential.Login == model.Login && 
			credential.Password == model.Password
		);

		if (credential is null)
		{
			return BadRequest("The user is not not exists. Invalid login or password");
		}

		var currentUser = new User(model.Login, model.Password);
		var userToken = _tokenProvider.GenerateToken(currentUser);

		var claims = new List<Claim>
		{
			new(ClaimTypes.Name, model.Login),
			new(ClaimTypes.UserData, model.Password)
		};

		ClaimsIdentity claimsIdentity = new(
			claims,
			"Token",
			ClaimsIdentity.DefaultNameClaimType,
			ClaimsIdentity.DefaultRoleClaimType);

		User.AddIdentity(claimsIdentity);
		return Ok(userToken);
	}

	[HttpPost("logout")]
	public async Task<IActionResult> Logout() => Ok();
}
