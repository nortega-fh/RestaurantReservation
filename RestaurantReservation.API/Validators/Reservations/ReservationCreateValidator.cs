using FluentValidation;
using RestaurantReservation.API.Contracts.Requests.Reservations;

namespace RestaurantReservation.API.Validators.Reservations;

public class ReservationCreateValidator : AbstractValidator<ReservationCreate>
{
    public ReservationCreateValidator()
    {
        RuleFor(reservation => reservation.PartySize).NotNull().GreaterThan(0);
        RuleFor(reservation => reservation.StartDate).NotNull().GreaterThanOrEqualTo(DateTime.Today)
            .WithMessage("The reservation's date can't be in the past");
        RuleFor(reservation => reservation.EndDate).NotNull().GreaterThan(reservation => reservation.StartDate)
            .WithMessage("Reservation end date should be greater than reservation start date")
            .Must((reservation, endDate) => endDate.Date == reservation.StartDate.Date)
            .WithMessage("The reservation's end date should be at the same day than the reservation's start date");
        RuleFor(reservation => reservation.CustomerId).MaximumLength(50);
    }
}