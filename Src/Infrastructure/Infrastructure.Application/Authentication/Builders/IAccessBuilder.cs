namespace Infrastructure.Application.Authentication.Builders;

using System.Security.Claims;
using Data;

public interface IAccessBuilder
{
    AccessToken BuildAccessToken(Claim[] claims);
}