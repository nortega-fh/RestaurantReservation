using System.Text.RegularExpressions;
using FluentValidation;
using RestaurantReservation.API.Contracts.Requests;

namespace RestaurantReservation.API.Validators;

public class CustomerValidator : AbstractValidator<CustomerCreate>
{
    public CustomerValidator()
    {
        RuleFor(customer => customer.FirstName).NotEmpty();
        RuleFor(customer => customer.LastName).NotEmpty();
        RuleFor(customer => customer.Email).NotEmpty();
        RuleFor(customer => customer.PhoneNumber).NotEmpty().Must(
            phoneNumber => Regex.IsMatch(phoneNumber, @"^\d+$")
        ).WithMessage("The phone number can only contain numbers");
    }
}