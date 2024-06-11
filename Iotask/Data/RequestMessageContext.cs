using Iotask.Models;
using Microsoft.EntityFrameworkCore;

namespace Iotask.Data;

public class RequestMessageContext : DbContext
{
    public RequestMessageContext(DbContextOptions<RequestMessageContext> options) : base(options)
    {
    }

    public DbSet<RequestMessage> RequestMessage => Set<RequestMessage>();
}
