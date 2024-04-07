namespace RestaurantReservation.Domain.Restaurants;

/// <summary>
///     <c>RestaurantSchedule</c> represents the schedule that a Restaurant follows.
/// </summary>
public class RestaurantSchedule
{
    /// <summary>
    ///     <c>WeeklySchedule</c> represents the usual schedule the restaurant goes by each day of the week.
    ///     If the list of schedule blocks of a day of the week is empty, the Restaurant is closed that day.
    /// </summary>
    public Dictionary<DayOfWeek, List<ScheduleBlock>> WeeklySchedule { get; set; } = new();

    /// <summary>
    ///     <c>ExceptionalOpenDays</c> represents dates that are open that are usually closed on the weekly schedule
    /// </summary>
    public List<DateTime>? ExceptionalOpenDays { get; set; } = new();

    /// <summary>
    ///     <c>ExceptionalClosedDays</c> represents dates that are closed that are usually open on the weekly schedule
    /// </summary>
    public List<DateTime>? ExceptionalClosedDays { get; set; } = new();
}