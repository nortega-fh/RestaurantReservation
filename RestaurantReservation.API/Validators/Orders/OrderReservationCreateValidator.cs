using FluentValidation;
using RestaurantReservation.API.Contracts.Requests.Orders;

namespace RestaurantReservation.API.Validators.Orders;

public class OrderReservationCreateValidator : AbstractValidator<OrderReservationCreate>
{
    public OrderReservationCreateValidator()
    {
        RuleFor(order => order.EmployeeId).NotEmpty().NotNull().MaximumLength(24);
    }
}