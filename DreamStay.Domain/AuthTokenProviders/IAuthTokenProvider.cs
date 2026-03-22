using DreamStay.Domain.Models;

namespace DreamStay.Domain.AuthTokenProviders;

public interface IAuthTokenProvider
{
	public string GenerateToken(User user);
}
