using EventRegistry.Api.Dtos;
using EventRegistry.Business.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EventRegistry.Api.Controllers;

[ApiController]
[Route("api/applications")]
[Produces("application/json")]
[Tags("Applications")]
public class ApplicationController(IApplicationService applicationService) : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Register application")]
    [EndpointDescription("Registers a new application and returns its credentials. " +
                         "Store the API key securely - it cannot be retrieved again.")]
    [ProducesResponseType<RegisterApplicationResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register([FromBody] RegisterApplicationRequestDto request)
    {
        var (applicationId, apiKey) = await applicationService.RegisterAsync(
            request.Name,
            request.Description);

        return Ok(new RegisterApplicationResponseDto
        {
            ApplicationId = applicationId,
            ApiKey = apiKey
        });
    }
}
