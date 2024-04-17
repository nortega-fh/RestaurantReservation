namespace RestaurantReservation.Domain.Restaurants;

/// <summary>
///     Class <c>ScheduleBlock</c> represents a block of hours in a schedule
/// </summary>
public class ScheduleBlock
{
    /// <summary>
    ///     <c>Start</c> represents the start hour of the block, conformed by two integers: the number of hours and the number
    ///     of minutes
    ///     <value><c>Start[0]</c>: represents the hour, should be between 0 and 23</value>
    ///     <value><c>Start[1]</c>: represents the minutes, should be between 0 and 59</value>
    /// </summary>
    public int[] Start { get; set; } = new int[2];

    /// <summary>
    ///     <c>End</c> represents the ending hour of the block, conformed by two integers: the number of hours and the number
    ///     of minutes
    ///     <value><c>End[0]</c>: represents the hour, should be between 0 and 23</value>
    ///     <value><c>End[1]</c>: represents the minutes, should be between 0 and 59</value>
    /// </summary>
    public int[] End { get; set; } = new int[2];
}