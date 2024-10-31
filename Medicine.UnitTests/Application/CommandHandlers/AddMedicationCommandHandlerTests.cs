
using Moq;
using FluentAssertions;
using Medicine.Domain.Enums;
using Medicine.Domain.Entities;
using Medicine.Domain.Interfaces;
using Medicine.Application.Commands;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Medicine.UnitTests.Application.CommandHandlers;

public class AddMedicationCommandHandlerTests
{
    private readonly Mock<IApplicationDbContext> _dbContextMock;
    private readonly AddMedicationCommandHandler _handler;

    public AddMedicationCommandHandlerTests()
    {
        _dbContextMock = new Mock<IApplicationDbContext>();
        _handler = new AddMedicationCommandHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Should_AddMedication_ValidCommand()
    {
        // Arrange:
        var command = new AddMedicationCommand
        {
            Name = "New Medicine",
            MedicationType = MedicationType.Pills,
            Quantity = 10,
        };

        _dbContextMock.Setup(dbContext =>
            dbContext.Medications.AddAsync(It.IsAny<Medication>(), It.IsAny<CancellationToken>())
        )
        .Callback((Medication medication, CancellationToken cancellation) => { })
        .Returns((Medication medication, CancellationToken cancellation)
            => ValueTask.FromResult((EntityEntry<Medication>)null));

        _dbContextMock.Setup(dbContext =>
            dbContext.SaveChangesAsync(It.IsAny<CancellationToken>())
        )
        .Callback((CancellationToken CancellationToken) => { })
        .Returns((CancellationToken cancellationToken)
            => Task.FromResult(1));


        // Act:
        var response = await _handler.Handle(command, CancellationToken.None);


        // Assert:
        response.Should().NotBeNull();
        response.Result.Should().BeTrue();
        response.Data.Should().Be(1);
    }
}