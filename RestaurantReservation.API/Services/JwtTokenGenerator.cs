using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using RestaurantReservation.API.Dtos.Responses;

namespace RestaurantReservation.API.Services;

public class JwtTokenGenerator : ITokenGenerator
{
    private readonly IConfiguration _configuration;

    public JwtTokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }
    
    public TokenResponse GenerateToken(string username, string password)
    {
        var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(_configuration["Auth:Secret"]));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.Sha256);
        var claims = new List<Claim>
        {
            new("sub", string.Empty),
            new("username", string.Empty),
            new("role", string.Empty),
        };
        var token = new JwtSecurityToken(
            _configuration["Auth:Issuer"],
            _configuration["Auth:Audience"],
            claims,
            DateTime.Now,
            DateTime.Now.AddDays(1),
            signingCredentials
        );
        return new TokenResponse{ Token = new JwtSecurityTokenHandler().WriteToken(token) };
    }
}