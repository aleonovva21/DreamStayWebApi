using DreamStay.Domain.Models;

namespace DreamStayWebApi.Models;

public class ReportModel
{
	public int? IdNumber { get; set; }
	public int? IdClient { get; set; }
	public int? IdEmployee { get; set; }
	public string? RoomType { get; set; }

	public DateOnly StartDate { get; set; }
	public DateOnly EndDate { get; set; }

	public List<Room>? Rooms { get; set; }
	public List<Client>? Clients { get; set; }
	public List<Employee>? Employees { get; set; }
	public string[]? RoomTypes { get; set; }
	public List<Booking>? Bookings { get; set; }
}
