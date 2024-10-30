
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Medicine.Application.Dtos;
using Medicine.Application.Common;
using Medicine.Application.Queries;
using Medicine.Application.Commands;

namespace Medicine.Api.Controllers;

[ApiController, Route("api/[controller]")]
public class MedicationController (ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost(nameof(GetMedication))]
    public async Task<Response<Page<MedicationDto>>> GetMedication(GetMedicationQuery query, CancellationToken cancellationToken)
        => await _sender.Send(query, cancellationToken);

    [HttpPost(nameof(AddMedication))]
    public async Task<Response<int>> AddMedication(AddMedicationCommand command, CancellationToken cancellationToken)
        => await _sender.Send(command, cancellationToken);

    [HttpDelete(nameof(DeleteMedication))]
    public async Task<Response<int>> DeleteMedication(DeleteMedicationCommand command, CancellationToken cancellationToken)
        => await _sender.Send(command, cancellationToken);
}
