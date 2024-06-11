using Iotask.Data;
using Iotask.Models;
using Microsoft.EntityFrameworkCore;

namespace Iotask.Services;

public class RequestMessageService
{
    private readonly RequestMessageContext _requestMessageContext;
    private readonly TimeProvider _timeProvider;
    private const int DefaultPageSize = 10;

    public RequestMessageService(
        RequestMessageContext requestMessageContext,
        TimeProvider timeProvider)
    {
        _requestMessageContext = requestMessageContext;
        _timeProvider = timeProvider;
    }

    public async Task<string> AddMessage(string message)
    {
        var newMessage = new RequestMessage
        {
            Request = message,
            RequestTime = _timeProvider.GetUtcNow(),
        };

        _requestMessageContext.Add(newMessage);
        await _requestMessageContext.SaveChangesAsync();

        return "You send: " + message;
    }

    public async Task<IEnumerable<RequestMessage>> Get(
        int? page = 0, int? pageSize = 0, 
        string search = "")
    {
        var query = _requestMessageContext.RequestMessage
            .AsNoTracking()
            .AsQueryable();

        if (string.IsNullOrWhiteSpace(search) == false)
        {
            query = query.Where(m => m.Request.Contains(search));
        }

        if (page >= 0)
        {
            var adjustedPageSize = pageSize ?? 1;
            if (adjustedPageSize < 1)
            {
                adjustedPageSize = DefaultPageSize;
            }

            query = query
                .Skip(page.Value * adjustedPageSize)
                .Take(adjustedPageSize);
        }

        var result = await query.ToListAsync();
        return result;
    }
}
