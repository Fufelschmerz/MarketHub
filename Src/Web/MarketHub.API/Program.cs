using System.Text;
using Autofac;
using Autofac.ConfiguredModules.Extensions;
using Autofac.Extensions.DependencyInjection;
using Infrastructure.Application.Authentication.Options;
using MarketHub.API.DI.ConfigureServices;
using MarketHub.API.Swagger;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

ConfigurationServices();

ConfigureContainer();

Configure();

void ConfigurationServices()
{
    builder.Services.Configure<TokenValidationParameters>(opt =>
    {
        JwtValidationOptions jwtValidationOptions = builder.Configuration
            .GetSection("Jwt:Authentication:ValidationParameters")
            .Get<JwtValidationOptions>() ?? throw new ArgumentNullException(nameof(jwtValidationOptions));

        opt.ValidateIssuer = jwtValidationOptions.ValidateIssuer;
        opt.ValidIssuer = jwtValidationOptions.Issuer;
        opt.ValidateIssuerSigningKey = jwtValidationOptions.ValidateIssuerSigningKey;
        opt.IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtValidationOptions.Secret));
        opt.ValidAudience = jwtValidationOptions.Audience;
        opt.ValidateAudience = jwtValidationOptions.ValidateAudience;
        opt.ValidateLifetime = jwtValidationOptions.ValidateLifetime;
        opt.ClockSkew = jwtValidationOptions.ClockSkew;
    });

    builder.Services.ConfigureSwagger();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddControllers();
    builder.Services.ConfigureMediatR();
    builder.Services.ConfigureAuthentication();
    builder.Services.ConfigureAuthorization();
    builder.Services.ConfigureDbContext(builder.Configuration);
    builder.Services.ConfigureAutoMapper();
    builder.Services.ConfigureFluentValidation();
}

void ConfigureContainer()
{
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterConfiguredModulesFromCurrentAssembly(builder.Configuration);
    });
}

void Configure()
{
    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
    }

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}