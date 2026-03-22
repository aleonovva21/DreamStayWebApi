using DreamStay.Domain.Infrastructure;
using DreamStay.Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DreamStay.Domain.AuthTokenProviders;

public sealed class JwtTokenProvider(IOptionsMonitor<JwtTokenOptions> options) : IAuthTokenProvider
{
	private readonly JwtTokenOptions _tokenOptions = options.CurrentValue;

	public string GenerateToken(User user)
	{
		var claims = new[]
		{
			new Claim(ClaimTypes.Name, user.Login),
			new Claim(ClaimTypes.UserData, user.Password)
		};

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecretKey));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			issuer: _tokenOptions.Issuer,
			audience: null,
			claims: claims,
			expires: DateTime.UtcNow.AddHours(1),
			signingCredentials: creds);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}
