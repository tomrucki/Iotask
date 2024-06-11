using FluentValidation;
using Iotask.Dto;
using Iotask.Models;
using Iotask.Services;
using Microsoft.AspNetCore.Mvc;

namespace Iotask.Controllers;

[ApiController]
[Route("")]
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
    public IEnumerable<RequestMessage> GetMessages()
    {
        return _requestMessageService.Get();
    }

    [HttpPost("/saveMessage")]
    public ActionResult SaveMessage([FromBody]AddMessage message, [FromServices]IValidator<AddMessage> validator)
    {
        var validation = validator.Validate(message);
        if (validation.IsValid == false)
        {
            return BadRequest(validation.Errors);
        }

        var result = _requestMessageService.AddMessage(message.Request);
        _logger.LogTrace("{Message}", message);
        return Ok(result);
    }
}
