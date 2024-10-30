
using FluentValidation;
using Medicine.Domain.Enums;
using Medicine.Application.Commands;

namespace Medicine.Application.Validators;

public class AddMedicationCommandValidator : AbstractValidator<AddMedicationCommand>
{
    public AddMedicationCommandValidator()
    {
        RuleFor(command => command.Name)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty();

        RuleFor(command => command.Quantity)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .Must(q => q.Value > 0)
            .WithMessage("Quantity must be bigger than 0");

        RuleFor(command => command.MedicationType)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .Must(medicationType => medicationType >= MedicationType.Pills
                                 || medicationType <= MedicationType.Others
            )
            .WithMessage("Invalid Medication Type");
    }
}