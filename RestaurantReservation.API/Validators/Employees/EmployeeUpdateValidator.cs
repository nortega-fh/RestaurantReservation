using FluentValidation;
using RestaurantReservation.API.Contracts.Requests.Employees;

namespace RestaurantReservation.API.Validators.Employees;

public class EmployeeUpdateValidator : AbstractValidator<EmployeeUpdate>
{
    public EmployeeUpdateValidator()
    {
        RuleFor(employeeUpdate => employeeUpdate.Position).Length(1, 100);
    }
}