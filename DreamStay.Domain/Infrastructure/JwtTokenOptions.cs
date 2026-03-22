namespace DreamStay.Domain.Infrastructure;

public sealed class JwtTokenOptions
{
	public const string Position = "JWT";

	public required string SecretKey { get; init; }
	public required string Issuer { get; init; }
	public required string[] ValidIssuers { get; init; }
}
