
using MediatR;
using Medicine.Domain.Interfaces;
using Medicine.Application.Common;
using Microsoft.EntityFrameworkCore;

namespace Medicine.Application.Commands;

public class DeleteMedicationCommand : IRequest<Response<int>>
{
    public int? MedicationId { get; set; }
}

public class DeleteMedicationCommandHandler (IApplicationDbContext dbContext) : IRequestHandler<DeleteMedicationCommand, Response<int>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Response<int>> Handle(DeleteMedicationCommand request, CancellationToken cancellationToken)
    {
        var result = await 
                     _dbContext
                     .Medications
                     .Where(m => m.Id == request.MedicationId.Value)
                     .ExecuteUpdateAsync(
                         setters => setters.SetProperty(m => m.IsDeleted, true),
                         cancellationToken
                     );

        return Response<int>.Create(result, result > 0);
    }
}