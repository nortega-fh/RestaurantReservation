using FluentValidation;
using RestaurantReservation.API.Contracts.Requests;

namespace RestaurantReservation.API.Validators;

public class RestaurantCreateValidator : AbstractValidator<RestaurantCreate>
{
    public RestaurantCreateValidator()
    {
        RuleFor(restaurant => restaurant.Name).NotEmpty().Length(1, 85);
        RuleFor(restaurant => restaurant.Address).NotEmpty().Length(1, 90);
        RuleFor(restaurant => restaurant.PhoneNumber).NotEmpty().Length(1, 15);
        RuleFor(restaurant => restaurant.OpeningHours).NotEmpty().Must(openingHours =>
                openingHours.Keys.All(dayOfWeek => Enum.TryParse<DayOfWeek>(dayOfWeek, true, out var _)))
            .WithMessage("The opening hours should have the days of the week as keys.");
    }
}