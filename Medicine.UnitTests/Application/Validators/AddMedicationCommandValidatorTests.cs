
using FluentAssertions;
using Medicine.Domain.Enums;
using Medicine.Application.Commands;
using Medicine.Application.Validators;

namespace Medicine.UnitTests.Application.Validators;

public class AddMedicationCommandValidatorTests
{
    private readonly AddMedicationCommandValidator _validator;

    public AddMedicationCommandValidatorTests()
    {
        _validator = new AddMedicationCommandValidator();    
    }

    [Fact]
    public void ShouldReturnFalse_InvalidCommand ()
    {
        // Arrange:
        var command = new AddMedicationCommand
        {
            MedicationType = MedicationType.Injections,
            Name = "any",
            Quantity = 0 // wrong quantity
        };

        // Act:
        var result = _validator.Validate(command);

        // Assert:
        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();
        result.Errors.Select(e => e.ErrorMessage)
                     .Any(message => message.Equals("Quantity must be bigger than 0"))
                     .Should()
                     .BeTrue();
    }
}
