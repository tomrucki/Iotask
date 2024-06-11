using FluentValidation;
using Iotask.Dto;
using Iotask.Models;
using Iotask.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Iotask.Controllers;

[ApiController]
[Route("")]
[Authorize]
public class MessageController : ControllerBase
{
    private readonly ILogger<MessageController> _logger;
    private readonly RequestMessageService _requestMessageService;

    public MessageController(
        ILogger<MessageController> logger,
        RequestMessageService requestMessageService)
    {
        _logger = logger;
        _requestMessageService = requestMessageService;
    }

    [HttpGet("/getMessages")]
    public Task<IEnumerable<RequestMessage>> GetMessages(
        int? page, int? pageSize, 
        string search)
    {
        return _requestMessageService.Get(page, pageSize, search);
    }

    [ProducesResponseType<string>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("/saveMessage")]
    public async Task<IActionResult> SaveMessage([FromBody]AddMessage message, [FromServices]IValidator<AddMessage> validator)
    {
        var validation = validator.Validate(message);
        if (validation.IsValid == false)
        {
            return BadRequest(validation.Errors);
        }

        var result = await _requestMessageService.AddMessage(message.Request);
        _logger.LogTrace("{Message}", message);
        return Ok(result);
    }
}
