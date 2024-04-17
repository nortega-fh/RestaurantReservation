using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.AuthHandlers;
using RestaurantReservation.API.Contracts.Requests.Users;
using RestaurantReservation.API.Contracts.Responses.API;
using RestaurantReservation.Domain.Users;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("/auth")]
public class AuthController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IUserService _userService;

    public AuthController(IUserService userService, ITokenGenerator tokenGenerator, IMapper mapper)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _tokenGenerator = tokenGenerator ?? throw new ArgumentNullException(nameof(tokenGenerator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserCreate createdUser)
    {
        if (await _userService.UserExistsAsync(createdUser.Username))
        {
            var error = new ErrorResponse
            {
                RequestPath = Request.Path,
                Errors = new Dictionary<string, string[]>
                {
                    { "username", new[] { $"The username {createdUser.Username} is already registered" } }
                }
            };
            return BadRequest(error);
        }
        var user = await _userService.CreateAsync(_mapper.Map<User>(createdUser));
        return Created(Request.Path, _tokenGenerator.GenerateTokenAsync(user));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLogin loginCredentials)
    {
        var user = await _userService.GetByCredentialsAsync(loginCredentials.Username, loginCredentials.Password);
        if (user is null)
        {
            return Unauthorized(new ErrorResponse
            {
                RequestPath = Request.Path,
                Errors = new Dictionary<string, string[]>
                {
                    { "authentication", new[] { "Invalid credentials" } }
                }
            });
        }
        return Created(Request.Path, _tokenGenerator.GenerateTokenAsync(user));
    }
}