namespace Iotask.Data;

public static class DbInitializerHostExt
{
    public static void CreateDbIfNotExists(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var pizzaContext = scope.ServiceProvider.GetRequiredService<RequestMessageContext>();
            pizzaContext.Database.EnsureCreated();
        }
    }
}