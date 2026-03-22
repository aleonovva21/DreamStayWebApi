namespace DreamStay.Domain.Models;

public partial class Address
{
    public int IdAddress { get; set; }

    public string City { get; set; } = null!;

    public string Stret { get; set; } = null!;

    public int House { get; set; }

    public int Apartment { get; set; }

    public virtual ICollection<PersonalDatum> PersonalData { get; set; } = new List<PersonalDatum>();
}
