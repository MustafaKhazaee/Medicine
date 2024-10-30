
using MediatR;
using Medicine.Domain.Enums;
using Medicine.Domain.Entities;
using Medicine.Domain.Interfaces;
using Medicine.Application.Common;

namespace Medicine.Application.Commands;

public class AddMedicationCommand : IRequest<Response<int>>
{
    public string? Name { get; set; }
    public int? Quantity { get; set; }
    public MedicationType? MedicationType { get; set; }

    public static explicit operator Medication(AddMedicationCommand command)
        => Medication.Create(command.Name, command.Quantity.Value, command.MedicationType.Value);
}

public class AddMedicationCommandHandler (IApplicationDbContext dbContext) : IRequestHandler<AddMedicationCommand, Response<int>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Response<int>> Handle(AddMedicationCommand request, CancellationToken cancellationToken)
    {
        var medication = (Medication) request;

        await _dbContext.Medications.AddAsync(medication, cancellationToken);

        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        return Response<int>.Create(result, result > 0);
    }
}