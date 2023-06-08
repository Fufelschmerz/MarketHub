namespace Infrastructure.Application.Authentication.Options;

using System.Text;
using Microsoft.IdentityModel.Tokens;

public sealed record JwtOptions
{
    public string Secret { get; set; }
    
    public string Issuer { get; set; }
    
    public string Audience { get; set; }

    public TimeSpan AccessTokenExpires { get; set; }

    public TimeSpan RefreshTokenExpires { get; set; }

    public SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
    }
}