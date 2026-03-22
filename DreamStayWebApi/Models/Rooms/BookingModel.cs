using DreamStay.Domain.Models;

namespace DreamStayWebApi.Models;

public partial class BookingModel
{
	public int? IdBooking { get; init; }

	public int? IdNumber { get; init; }

	public DateOnly CheckInDate { get; init; }

	public DateOnly DepartureDate { get; init; }

	public int? IdClient { get; init; }

	public int? IdEmployee { get; init; }

	public List<Client>? Clients { get; set; }
	public List<Employee>? Employees { get; set; }
}
