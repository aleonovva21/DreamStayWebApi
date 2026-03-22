namespace DreamStay.Domain.Models;

public partial class Document
{
    public int IdDocument { get; set; }

    public int Serial { get; set; }

    public int Number { get; set; }

    public DateOnly DateOfIssue { get; set; }

    public string IssuedByWhom { get; set; } = null!;

    public virtual ICollection<PersonalDatum> PersonalData { get; set; } = new List<PersonalDatum>();
}
