using System.Text.RegularExpressions;
using FluentValidation;
using RestaurantReservation.API.Contracts.Requests.Customers;

namespace RestaurantReservation.API.Validators.Customers;

public class CustomerUpdateValidator : AbstractValidator<CustomerUpdate>
{
    public CustomerUpdateValidator()
    {
        RuleFor(update => update.FirstName).NotEmpty();
        RuleFor(update => update.LastName).NotEmpty();
        RuleFor(update => update.Email).NotEmpty();
        RuleFor(update => update.PhoneNumber).NotEmpty().Must(
            phoneNumber => Regex.IsMatch(phoneNumber, @"^\d+$")
        ).WithMessage("The phone number can only contain numbers");
    }
}