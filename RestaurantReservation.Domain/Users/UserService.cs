﻿using Microsoft.AspNetCore.Identity;

namespace RestaurantReservation.Domain.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<User?> GetByCredentialsAsync(string username, string password)
    {
        var user = await _repository.GetByUsernameAsync(username);
        if (user is null)
        {
            return null;
        }
        var passwordMatchResult = new PasswordHasher<User>().VerifyHashedPassword(user, user.Password, password);
        return passwordMatchResult is PasswordVerificationResult.Success ? user : null;
    }

    public async Task<User> CreateAsync(User user)
    {
        user.Password = new PasswordHasher<User>().HashPassword(user, user.Password);
        return await _repository.CreateUserAsync(user);
    }

    public async Task<bool> UserExistsAsync(string username)
    {
        return await _repository.GetByUsernameAsync(username) is not null;
    }
}