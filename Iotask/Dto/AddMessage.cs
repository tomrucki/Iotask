using FluentValidation;

namespace Iotask.Dto;

public class AddMessage
{
    public string Request { get; set; }
}

public class AddMessageValidator : AbstractValidator<AddMessage>
{
    public AddMessageValidator()
    {
        RuleFor(x => x.Request).NotEmpty();
    }
}