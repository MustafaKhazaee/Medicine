
using Medicine.Domain.Enums;
using Medicine.Domain.Entities;

namespace Medicine.Application.Dtos;

public record MedicationDto
{
    private MedicationDto() { }

    public int Id { get; private init; }
    public string Name { get; private init; }
    public int Quantity { get; private init; }
    public MedicationType MedicationType { get; private init; }
    public string CreatedBy { get; private init; }
    public DateTime CreationDate { get; private init; }

    public static explicit operator MedicationDto(Medication medication)
        => new()
        {
            Id = medication.Id,
            Name = medication.Name,
            Quantity = medication.Quantity,
            MedicationType = medication.MedicationType,
            CreatedBy = medication.CreatedBy,
            CreationDate = medication.CreationDate,
        };
}
