﻿using System.Text.RegularExpressions;
using FluentValidation;
using RestaurantReservation.API.Contracts.Requests.Customers;

namespace RestaurantReservation.API.Validators.Customers;

public class CustomerValidator : AbstractValidator<CustomerCreate>
{
    public CustomerValidator()
    {
        RuleFor(customer => customer.Email).NotEmpty();
        RuleFor(customer => customer.PhoneNumber).NotEmpty().Must(
            phoneNumber => Regex.IsMatch(phoneNumber, @"^\d+$")
        ).WithMessage("The phone number can only contain numbers");
    }
}