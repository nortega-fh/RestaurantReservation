using FluentValidation;
using RestaurantReservation.API.Contracts.Requests.Orders;

namespace RestaurantReservation.API.Validators.Orders;

public class OrderMenuItemRequestValidator : AbstractValidator<OrderMenuItemRequest>
{
    public OrderMenuItemRequestValidator()
    {
        RuleFor(orderMenuItem => orderMenuItem.MenuItemId).NotEmpty();
        RuleFor(orderMenuItem => orderMenuItem.Quantity).GreaterThan(0);
    }
}