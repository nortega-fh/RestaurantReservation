using FluentValidation;
using RestaurantReservation.API.Contracts.Requests;

namespace RestaurantReservation.API.Validators;

public class EmployeeCreateValidator : AbstractValidator<EmployeeCreate>
{
    public EmployeeCreateValidator()
    {
        RuleFor(employeeCreate => employeeCreate.Username).Length(10, 50);
        RuleFor(employeeCreate => employeeCreate.Position).Length(1, 100);
    }
}