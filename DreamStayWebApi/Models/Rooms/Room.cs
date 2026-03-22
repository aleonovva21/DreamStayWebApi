using DreamStay.Domain.Models;

namespace DreamStayWebApi.Models.Rooms;

public sealed record Room(
	int IdNumber,
	decimal PricePerDay,
	string RoomType,
	string Photo,
	ICollection<Booking> Bookings
	);

public sealed record UpdateRoom(
	int? IdNumber,
	decimal? PricePerDay,
	string? RoomType,
	string? Photo,
	ICollection<Booking>? Bookings
	);