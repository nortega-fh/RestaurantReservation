using FluentValidation;
using RestaurantReservation.API.Contracts.Requests;

namespace RestaurantReservation.API.Validators;

public class TableCreateValidator : AbstractValidator<TableCreate>
{
    public TableCreateValidator()
    {
        RuleFor(table => table.Capacity).GreaterThan(0).LessThanOrEqualTo(20);
    }
}