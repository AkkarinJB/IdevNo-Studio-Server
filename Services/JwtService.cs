using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdevNoStudio.Api.Models;
using Microsoft.IdentityModel.Tokens;

namespace IdevNoStudio.Api.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        // Priority: Environment Variable > JwtSettings:SecretKey
        var secretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY") 
            ?? jwtSettings["SecretKey"] 
            ?? throw new InvalidOperationException("JWT SecretKey is not configured");
        var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER") 
            ?? jwtSettings["Issuer"] 
            ?? "IdevNoStudio";
        var audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") 
            ?? jwtSettings["Audience"] 
            ?? "IdevNoStudioUsers";
        var expirationMinutes = int.Parse(Environment.GetEnvironmentVariable("JWT_EXPIRATION_MINUTES") 
            ?? jwtSettings["ExpirationInMinutes"] 
            ?? "60");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.FirstName ?? ""),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

