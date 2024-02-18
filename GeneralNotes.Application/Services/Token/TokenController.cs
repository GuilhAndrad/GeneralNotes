using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GeneralNotes.Application.Services.Token;
public class TokenController(double tokenLifetimeInMinutes, string securityKey)
{
    private const string EmailAlias = "eml";
    private readonly double _tokenLifetimeInMinutes = tokenLifetimeInMinutes;
    private readonly string _securityKey = securityKey;

    public string GenerateToken(string emailOfUser)
    {
        var claims = new List<Claim>
        {
            new(EmailAlias, emailOfUser),
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_tokenLifetimeInMinutes),
            SigningCredentials = new SigningCredentials(SimetricKey(), SecurityAlgorithms.HmacSha256Signature)
        };

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }

    public ClaimsPrincipal ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var parametersValidation = new TokenValidationParameters
        {
            RequireExpirationTime = true,
            IssuerSigningKey = SimetricKey(),
            ClockSkew = new TimeSpan(0),
            ValidateIssuer = false,
            ValidateAudience = false,
        };

        var claims = tokenHandler.ValidateToken(token, parametersValidation, out _);

        return claims;
    }
    [return: MaybeNull]
    public string RecoverEmail(string token)
    {
        var claims = ValidateToken(token);
        var claim = claims.FindFirst(EmailAlias);

        return claim?.Value;
    }

    private SymmetricSecurityKey SimetricKey()
    {
        var symmetricKey = Convert.FromBase64String(_securityKey);
        return new SymmetricSecurityKey(symmetricKey);
    }
}
