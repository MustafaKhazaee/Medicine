
using Medicine.Domain.Enums;
using Medicine.Domain.Common;

namespace Medicine.Domain.Entities;

public class Medication : AuditableEntity
{
    private Medication() { }

    public string Name { get; set; }
    public int Quantity { get; set; }
    public MedicationType MedicationType { get; set; }

    public static Medication Create (string name, int quantity, MedicationType medicationType)
        => new ()
        {
            Name = name,
            Quantity = quantity,
            MedicationType = medicationType
        };

    public override string ToString() => $"{Quantity} item of {Name} (type: {MedicationType})";
}
