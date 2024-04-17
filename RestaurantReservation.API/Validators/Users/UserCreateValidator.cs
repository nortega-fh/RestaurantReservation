using FluentValidation;
using RestaurantReservation.API.Contracts.Requests.Users;

namespace RestaurantReservation.API.Validators.Users;

public class UserCreateValidator : AbstractValidator<UserCreate>
{
    public UserCreateValidator()
    {
        RuleFor(createdUser => createdUser.Username).NotEmpty();
        RuleFor(createdUser => createdUser.Password).NotEmpty();
        RuleFor(createdUser => createdUser.FirstName).NotEmpty();
        RuleFor(createdUser => createdUser.LastName).NotEmpty();
    }
}