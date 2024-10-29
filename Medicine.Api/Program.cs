
using Medicine.Application;
using Medicine.Infrastructure;

namespace Medicine.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var environment = builder.Environment.EnvironmentName;
        builder.Configuration.AddJsonFile($"appsettings.{environment}.json");

        builder.Services.AddControllers();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddApplication();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}