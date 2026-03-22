namespace DreamStay.Domain.Models;

public partial class Room
{
	public int IdNumber { get; set; }

	public string RoomType { get; set; } = null!;

	public string Photo { get; set; } = null!;

	public decimal PricePerDay { get; set; }

	public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
