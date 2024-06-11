using Iotask.Models;

namespace Iotask.Services;

public class RequestMessageService
{
    static List<RequestMessage> _data = new();
    private TimeProvider _timeProvider;

    public RequestMessageService(TimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }

    public string AddMessage(string message)
    {
        _data.Add(new RequestMessage
        {
            Request = message,
            RequestTime = _timeProvider.GetUtcNow(),
        });

        return "You send: " + message;
    }

    public IEnumerable<RequestMessage> Get()
    {
        return _data;
    }
}
