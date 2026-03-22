namespace DreamStay.Domain.Models;

public partial class Booking
{
    public int IdBooking { get; set; }

    public int? IdNumber { get; set; }

    public DateOnly CheckInDate { get; set; }

    public DateOnly DepartureDate { get; set; }

    public int? IdClient { get; set; }

    public int? IdEmployee { get; set; }

    public virtual Client? IdClientNavigation { get; set; }

    public virtual Employee? IdEmployeeNavigation { get; set; }

    public virtual Room? IdNumberNavigation { get; set; }
}
