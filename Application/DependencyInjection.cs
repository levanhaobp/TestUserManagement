using Application.Common.Middlewares;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddFluentValidation(fv =>
            {
                fv.AutomaticValidationEnabled = true;
                fv.DisableDataAnnotationsValidation = true;
                fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            });
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }

        public static IApplicationBuilder AddApplication(this IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            return app;
        }
    }
}