namespace DreamStayWebApi;

public partial class BookingView
{
    public int? IdNumber { get; set; }

    public string? RoomType { get; set; }

    public string? Photo { get; set; }

    public DateOnly? CheckInDate { get; set; }

    public DateOnly? DepartureDate { get; set; }
}
