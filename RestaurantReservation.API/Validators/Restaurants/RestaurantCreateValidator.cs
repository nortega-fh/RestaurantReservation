using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using RestaurantReservation.API.Contracts.Requests.Restaurants;
using RestaurantReservation.Domain.Restaurants;

namespace RestaurantReservation.API.Validators.Restaurants;

public class RestaurantCreateValidator : AbstractValidator<RestaurantCreate>
{
    public RestaurantCreateValidator()
    {
        RuleFor(restaurant => restaurant.Name).NotEmpty().Length(1, 85);
        RuleFor(restaurant => restaurant.Address).NotEmpty().Length(1, 90);
        RuleFor(restaurant => restaurant.PhoneNumber).NotEmpty().Length(1, 15);
        RuleFor(restaurant => restaurant.Schedule).NotNull().Must(HaveValidDaysAndHours)
            .WithMessage(
                "The weekly schedule can't be empty and should be of the form " +
                " \"dayOfWeek\": [ { \"start\": [ hours, minutes ], \"end\": [ hours, minutes ] } ]" +
                " where 0 <= hours <= 23 and 0 <= minutes <= 59");
    }

    private static bool HaveValidDaysAndHours(RestaurantSchedule schedule)
    {
        var weeklySchedule = schedule.WeeklySchedule;
        return !weeklySchedule.IsNullOrEmpty() && weeklySchedule.Keys.All(dayOfWeek =>
            weeklySchedule[dayOfWeek]
                .All(scheduleBlock => IsInHourRange(scheduleBlock) && IsInMinuteRange(scheduleBlock)));
    }

    private static bool IsInHourRange(ScheduleBlock scheduleBlock)
    {
        return IsHour(scheduleBlock.Start[0]) && IsHour(scheduleBlock.End[0]);
    }

    private static bool IsHour(int value)
    {
        return value is >= 0 and <= 23;
    }

    private static bool IsInMinuteRange(ScheduleBlock scheduleBlock)
    {
        return IsMinute(scheduleBlock.Start[1]) && IsMinute(scheduleBlock.End[1]);
    }

    private static bool IsMinute(int value)
    {
        return value is >= 0 and <= 59;
    }
}