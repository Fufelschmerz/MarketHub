namespace MarketHub.API.DI.ConfigureServices;

using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using Application.Infrastructure.Cache;
using Application.Infrastructure.Exceptions.Factories;
using Infrastructure.Application.Authentication.Constants;
using Infrastructure.Application.Authentication.Data;
using Infrastructure.Cache.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

internal static class ConfigureServiceAuthentication
{
    internal static IServiceCollection ConfigureAuthentication(this IServiceCollection services)
    {
        JwtSecurityTokenHandler.DefaultMapInboundClaims  = false;
        
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            opt.RequireHttpsMetadata = true;
            opt.SaveToken = true;
            opt.TokenValidationParameters = services.BuildServiceProvider()
                .GetRequiredService<IOptions<TokenValidationParameters>>().Value;
            
            opt.Events = new JwtBearerEvents
            {
                OnChallenge = async context =>
                {
                    context.HandleResponse();
                    
                    int statusCode = StatusCodes.Status401Unauthorized;
                    
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = statusCode;

                    await context.Response.WriteAsync(JsonSerializer.Serialize(new ProblemDetails
                    {
                        Title = "API error",
                        Detail = context.AuthenticateFailure?.Message,
                        Status = statusCode
                    }));
                },
                
                OnTokenValidated = async context =>
                {
                    string? jti = context.Principal?.Claims.FirstOrDefault(x => x.Type == ClaimNames.Jti)?.Value;
                    string? sid = context.Principal?.Claims.FirstOrDefault(x => x.Type == ClaimNames.Sid)?.Value;
                    
                    if (string.IsNullOrWhiteSpace(jti))
                        context.Fail(ApiExceptionFactory.InvalidClaim(nameof(ClaimNames.Jti)));
                    
                    if(string.IsNullOrWhiteSpace(sid) ||  !long.TryParse(sid, out long _))
                        context.Fail(ApiExceptionFactory.InvalidClaim(nameof(ClaimNames.Sid)));
                    
                    ICacheService<AccessToken> accessTokenCacheService = context.HttpContext.RequestServices
                        .GetRequiredService<ICacheService<AccessToken>>();

                    AccessToken? accessToken = await accessTokenCacheService.GetAsync(Keys.GetAccessTokenKey(jti!));

                    if (accessToken is null)
                        context.Fail(ApiExceptionFactory.InvalidToken(nameof(AccessToken.Jwt)));
                }
            };
        });

        return services;
    }
}