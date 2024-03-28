using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Dtos.Requests;
using RestaurantReservation.API.Entities;
using RestaurantReservation.API.Services;

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
        var user = await _userService.CreateAsync(_mapper.Map<User>(createdUser));
        return Created(Request.Path, _tokenGenerator.GenerateTokenAsync(user));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLogin loginCredentials)
    {
        var user = await _userService.GetAsync(loginCredentials.Username, loginCredentials.Password);
        if (user is null)
        {
            return Unauthorized("Invalid credentials");
        }
        return Created(Request.Path, _tokenGenerator.GenerateTokenAsync(user));
    }
}