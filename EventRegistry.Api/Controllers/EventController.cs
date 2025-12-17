using EventRegistry.Api.Dtos;
using EventRegistry.Business.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventRegistry.Api.Controllers;

[ApiController]
[Route("api/events")]
[Produces("application/json")]
[Tags("Events")]
public class EventController(IEventLoggerService eventLoggerService) : ControllerBase
{
    [HttpPost]
    [Authorize]
    [EndpointSummary("Log event")]
    [EndpointDescription(
        "Logs an event for the authenticated application. The event is queued for asynchronous processing.")]
    [ProducesResponseType<LogEventResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> LogEvent([FromBody] LogEventRequestDto request)
    {
        var applicationIdClaim = User.FindFirst("ApplicationId")?.Value;
        if (string.IsNullOrWhiteSpace(applicationIdClaim))
            return Unauthorized(new { error = "Missing 'ApplicationId' claim in authentication token" });

        if (!Guid.TryParse(applicationIdClaim, out var applicationId))
            return Unauthorized(new { error = "Invalid 'ApplicationId' claim format, expected a valid GUID" });

        var (eventId, status) = await eventLoggerService.LogEventAsync(
            applicationId,
            request.Category,
            request.Message,
            request.Timestamp.Value,
            request.Metadata);

        return Ok(new LogEventResponseDto
        {
            EventId = eventId,
            Status = status
        });
    }
}
