
using Medicine.Domain.Enums;
using Medicine.Domain.Common;

namespace Medicine.Domain.Entities;

public class Medication : AuditableEntity
{
    private Medication() { }

    public string Name { get; private init; }
    public int Quantity { get; private init; }
    public MedicationType MedicationType { get; set; }

    public static Medication Create (string name, int quantity, MedicationType medicationType)
        => new ()
        {
            Name = name,
            Quantity = quantity,
            MedicationType = medicationType
        };


    public static Medication CreateSample()
        => new()
        {
            Id = 0,
            CreatedBy = "Me",
            CreationDate = DateTime.UtcNow,
            IsDeleted = false,
            MedicationType = MedicationType.Injections,
            Name = "Name",
            Quantity = 1,
        };

    public override string ToString() => $"{Quantity} item of {Name} (type: {MedicationType})";
}
