using FluentValidation;
using RestaurantReservation.API.Contracts.Requests;

namespace RestaurantReservation.API.Validators;

public class EmployeeUpdateValidator : AbstractValidator<EmployeeUpdate>
{
    public EmployeeUpdateValidator()
    {
        RuleFor(employeeUpdate => employeeUpdate.Position).Length(1, 100);
    }
}