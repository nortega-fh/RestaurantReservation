using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using RestaurantReservation.API.Contracts.Responses.API;
using RestaurantReservation.Domain.Users;

namespace RestaurantReservation.API.AuthHandlers;

public class JwtTokenGenerator : ITokenGenerator
{
    private readonly IConfiguration _configuration;

    public JwtTokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public TokenResponse GenerateTokenAsync(User user)
    {
        var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(_configuration["Auth:Secret"]));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            _configuration["Auth:Issuer"],
            _configuration["Auth:Audience"],
            new List<Claim>
            {
                new("sub", user.Id),
                new("username", user.Username)
            },
            DateTime.Now,
            DateTime.Now.AddDays(1),
            signingCredentials
        );
        return new TokenResponse { Token = new JwtSecurityTokenHandler().WriteToken(token) };
    }
}