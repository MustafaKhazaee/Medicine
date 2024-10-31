
using FluentAssertions;
using Medicine.Domain.Entities;
using Medicine.Application.Dtos;

namespace Medicine.UnitTests.Domain.Entities;

public class MedicationTests
{
    [Fact]
    public void OperatorTest_Medication ()
    {
        // Arrange: 
        var medication = Medication.CreateSample();

        // Act:
        var medicationDto = (MedicationDto) medication;

        // Assert:
        medicationDto.Should().NotBeNull();
        medicationDto.Should().BeOfType<MedicationDto>();
    }
}
