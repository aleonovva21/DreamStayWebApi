using DreamStay.Domain.Models;

namespace DreamStayWebApi.Models;

public sealed class BookingHistory
{
	public int? IdNumber { get; init; }
	public int? IdClient { get; init; }
	public string? RoomType { get; init; }

	public List<Room>? Rooms { get; set; }
	public List<Client>? Clients { get; set; }
	public string[]? RoomTypes { get; set; }
	public List<Booking>? Bookings { get; set; }
}
