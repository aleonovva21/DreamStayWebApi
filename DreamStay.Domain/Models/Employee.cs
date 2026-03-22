namespace DreamStay.Domain.Models;

public partial class Employee
{
    public int IdEmployee { get; set; }

    public string Fio { get; set; } = null!;

    public int? IdRole { get; set; }

    public int? IdCredential { get; set; }

    public int? IdData { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Credential? IdCredentialNavigation { get; set; }

    public virtual PersonalDatum? IdDataNavigation { get; set; }

    public virtual Role? IdRoleNavigation { get; set; }
}
