
using FluentValidation;
using Medicine.Application.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace Medicine.Application;

public static class DependencyInjection
{
    public static void AddApplication (this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection), includeInternalTypes: true);

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
    }
}