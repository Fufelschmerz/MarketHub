namespace MarketHub.API.Swagger;

using Microsoft.OpenApi.Models;

internal static class SwaggerExtensions
{
    internal static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1",
                new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "MarketHub API",
                    Description = "ASP.NET Core 7.0 Web API"
                });

            opt.AddSecurityDefinition("Bearer",
                new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });

            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });

        return services;
    }

    internal static IApplicationBuilder UseSwagger(this IApplicationBuilder applicationBuilder)
    {
        if (applicationBuilder is null)
            throw new ArgumentNullException(nameof(applicationBuilder));

        applicationBuilder
            .UseSwagger(options => { })
            .UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "MarketHub API");
            });

        return applicationBuilder;
    }
}