namespace DreamStayWebApi.Models;

public record PersonalInfoModel(
	string FIO,
	string? Role,
	string? PhoneNumber,
	string? Email,
	string? City,
	string? Street,
	int? House,
	int? Apartment,
	int? IdData,
	int? IdAddress
	);
