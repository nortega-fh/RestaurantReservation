using FluentValidation;
using RestaurantReservation.API.Contracts.Requests.Employees;

namespace RestaurantReservation.API.Validators.Employees;

public class EmployeeCreateValidator : AbstractValidator<EmployeeCreate>
{
    public EmployeeCreateValidator()
    {
        RuleFor(employeeCreate => employeeCreate.Username).Length(5, 50);
        RuleFor(employeeCreate => employeeCreate.Position).Length(1, 100);
    }
}