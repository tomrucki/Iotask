using Iotask.Models;

namespace Iotask.Services;

public class RequestMessageService
{
    static List<RequestMessage> _data = new();
    private TimeProvider _timeProvider;
    private const int DefaultPageSize = 10;

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

    public IEnumerable<RequestMessage> Get(
        int? page = 0, int? pageSize = 0, 
        string search = "")
    {
        var query = _data
            .AsQueryable();

        if (string.IsNullOrWhiteSpace(search) == false)
        {
            query = query.Where(m => m.Request.Contains(search));
        }

        if (page >= 0)
        {
            pageSize ??= DefaultPageSize;
            query = query
                .Skip(page.Value * pageSize.Value)
                .Take(pageSize.Value);
        }

        return query.ToList();
    }
}
