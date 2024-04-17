using FluentValidation;
using RestaurantReservation.API.Contracts.Requests.Users;

namespace RestaurantReservation.API.Validators.Users;

public class UserLoginValidator : AbstractValidator<UserLogin>
{
    public UserLoginValidator()
    {
        RuleFor(credentials => credentials.Username).NotEmpty();
        RuleFor(credentials => credentials.Password).NotEmpty();
    }
}