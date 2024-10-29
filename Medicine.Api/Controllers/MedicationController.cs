
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Medicine.Api.Controllers;

[ApiController, Route("api/[controller]")]
public class MedicationController (ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost(nameof(AddMedication))]
    public async Task<Response> AddMedication(AddMedicationCommand command, CancellationToken cancellationToken)
        => await _sender.Send(command, cancellationToken);
}
