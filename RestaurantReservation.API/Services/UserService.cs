﻿using Microsoft.AspNetCore.Identity;
using RestaurantReservation.API.Entities;
using RestaurantReservation.API.Repositories;

namespace RestaurantReservation.API.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<User?> GetAsync(string username, string password)
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
        return await _repository.CreateUserAsync(user);
    }

    public async Task UpdateAsync(string id, User user)
    {
        await _repository.UpdateAsync(id, user);
    }

    public async Task DeleteAsync(string id)
    {
        await _repository.DeleteAsync(id);
    }
}