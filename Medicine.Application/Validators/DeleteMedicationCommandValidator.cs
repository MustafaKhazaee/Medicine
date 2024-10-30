
using FluentValidation;
using Medicine.Domain.Interfaces;
using Medicine.Application.Commands;
using Microsoft.EntityFrameworkCore;

namespace Medicine.Application.Validators;

public class DeleteMedicationCommandValidator : AbstractValidator<DeleteMedicationCommand>
{
    public DeleteMedicationCommandValidator(IApplicationDbContext dbContext)
    {
        RuleFor(command => command.MedicationId)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .MustAsync(
                async (medicationId, CancellationToken) =>
                    await dbContext.Medications.AnyAsync(m => m.Id == medicationId.Value, cancellationToken: CancellationToken)
            )
            .WithMessage("No Medication found with the specified Id");
    }
}
