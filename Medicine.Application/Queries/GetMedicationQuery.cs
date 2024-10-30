
using MediatR;
using Medicine.Application.Dtos;
using Medicine.Domain.Interfaces;
using Medicine.Application.Common;
using Microsoft.EntityFrameworkCore;

namespace Medicine.Application.Queries;

public class GetMedicationQuery : IRequest<Response<Page<MedicationDto>>>
{
    public int? PageSize { get; set; } = Constants.Paging.DefaultPageSize;
    public int? PageNumber { get; set; } = Constants.Paging.DefaultPageNumber;
}

public class GetMedicationQueryHandler (IApplicationDbContext dbContext) : IRequestHandler<GetMedicationQuery, Response<Page<MedicationDto>>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Response<Page<MedicationDto>>> Handle(GetMedicationQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Medications.AsNoTracking().Select(m => (MedicationDto) m);

        var page = await Page<MedicationDto>.CreateAsync(
            query, 
            request.PageSize.Value, 
            request.PageNumber.Value, 
            cancellationToken
        );

        return Response<Page<MedicationDto>>.Success(page);
    }
}