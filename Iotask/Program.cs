
using FluentValidation;
using Iotask.Data;
using Iotask.Dto;
using Iotask.Infrastructure.Authentication;
using Iotask.Services;

namespace Iotask;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddSingleton<TimeProvider>(TimeProvider.System);
        builder.Services.AddScoped<RequestMessageService>();
        builder.Services.AddScoped<IValidator<AddMessage>, AddMessageValidator>();

        builder.Services.AddSqlite<RequestMessageContext>("Data Source=RequestMessage.db");

        builder.Services.AddControllers();
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.AddKeycloakAuthorization();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        if (app.Environment.IsDevelopment() == false)
        {
            app.UseHttpsRedirection();
        }

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.CreateDbIfNotExists();

        app.Run();
    }
}
