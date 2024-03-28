using FluentValidation;
using RestaurantReservation.API.Dtos.Requests;

namespace RestaurantReservation.API.Validators;

public class UserLoginValidator : AbstractValidator<UserLogin>
{
    public UserLoginValidator()
    {
        RuleFor(credentials => credentials.Username).NotEmpty();
        RuleFor(credentials => credentials.Password).NotEmpty();
    }
}