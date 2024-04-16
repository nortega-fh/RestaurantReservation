using FluentValidation;
using RestaurantReservation.API.Contracts.Requests;

namespace RestaurantReservation.API.Validators;

public class OrderReservationCreateValidator : AbstractValidator<OrderReservationCreate>
{
    public OrderReservationCreateValidator()
    {
        RuleFor(order => order.EmployeeId).NotEmpty().NotNull().MaximumLength(20);
    }
}