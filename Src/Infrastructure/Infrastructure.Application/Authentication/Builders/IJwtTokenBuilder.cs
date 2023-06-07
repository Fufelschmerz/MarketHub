namespace Infrastructure.Application.Authentication.Builders;

using System.Security.Claims;
using Data;

public interface IJwtTokenBuilder
{
    JwtToken BuildJwtToken(Claim[] claims);
}