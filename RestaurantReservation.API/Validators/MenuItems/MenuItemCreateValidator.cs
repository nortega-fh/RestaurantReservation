using FluentValidation;
using RestaurantReservation.API.Contracts.Requests.MenuItems;

namespace RestaurantReservation.API.Validators.MenuItems;

public class MenuItemCreateValidator : AbstractValidator<MenuItemCreate>
{
    public MenuItemCreateValidator()
    {
        RuleFor(menuItem => menuItem.Name).Length(1, 100);
        RuleFor(menuItem => menuItem.Description).Length(0, 250);
        RuleFor(menuItem => menuItem.Price).GreaterThan(0);
    }
}