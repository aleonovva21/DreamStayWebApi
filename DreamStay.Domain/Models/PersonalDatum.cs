namespace DreamStay.Domain.Models;

public partial class PersonalDatum
{
    public int IdData { get; set; }

    public int? IdDocument { get; set; }

    public int? IdAddress { get; set; }

    public string Email { get; set; } = null!;

    public string MobilePhone { get; set; } = null!;

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual Address? IdAddressNavigation { get; set; }

    public virtual Document? IdDocumentNavigation { get; set; }
}
