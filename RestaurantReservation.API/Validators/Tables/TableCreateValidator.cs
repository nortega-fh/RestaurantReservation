using FluentValidation;
using RestaurantReservation.API.Contracts.Requests.Tables;

namespace RestaurantReservation.API.Validators.Tables;

public class TableCreateValidator : AbstractValidator<TableCreate>
{
    public TableCreateValidator()
    {
        RuleFor(table => table.Capacity).GreaterThan(0).LessThanOrEqualTo(20);
    }
}