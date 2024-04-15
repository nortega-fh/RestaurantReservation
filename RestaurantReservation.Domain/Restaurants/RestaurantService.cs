namespace RestaurantReservation.Domain.Restaurants;

public class RestaurantService : IRestaurantService
{
    private readonly IRestaurantRepository _repository;

    public RestaurantService(IRestaurantRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync(int pageSize, int pageNumber)
    {
        return await _repository.GetAllAsync(pageSize, pageNumber);
    }

    public async Task<Restaurant?> GetByIdAsync(string id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task CreateAsync(Restaurant restaurant)
    {
        await _repository.CreateAsync(restaurant);
    }

    public async Task UpdateAsync(Restaurant restaurant)
    {
        await _repository.UpdateAsync(restaurant);
    }

    public async Task DeleteAsync(string id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<bool> RestaurantExistsWithIdAsync(string id)
    {
        return await GetByIdAsync(id) is not null;
    }

    public async Task<bool> IsRestaurantAvailableOnDateAsync(string restaurantId, DateTime date)
    {
        var dateDayOfWeek = date.DayOfWeek;
        var restaurant = await GetByIdAsync(restaurantId);
        var isExceptionallyOpenOnDate = restaurant?.Schedule.ExceptionalOpenDays?.Contains(date) ?? false;
        var restaurantWeeklySchedule = restaurant?.Schedule.WeeklySchedule;
        var isDateAmongWorkingHours = (restaurantWeeklySchedule?.ContainsKey(dateDayOfWeek) ?? false)
                                      && restaurantWeeklySchedule[dateDayOfWeek].Find(scheduleBlock =>
                                          IsDateAmongScheduleBlock(scheduleBlock, date)) is not null;
        return isExceptionallyOpenOnDate || isDateAmongWorkingHours;
    }

    private static bool IsDateAmongScheduleBlock(ScheduleBlock scheduleBlock, DateTime date)
    {
        var monthToString = ParseIntToValidDateString(date.Month);
        var dayToString = ParseIntToValidDateString(date.Day);
        var dateString = $"{date.Year}-{monthToString}-{dayToString}";
        var scheduleStartHourToString = ParseIntToValidDateString(scheduleBlock.Start[0]);
        var scheduleStartMinuteToString = ParseIntToValidDateString(scheduleBlock.Start[1]);
        var scheduleEndHourToString = ParseIntToValidDateString(scheduleBlock.End[0]);
        var scheduleEndMinuteToString = ParseIntToValidDateString(scheduleBlock.End[1]);
        var scheduleStartDate =
            DateTime.Parse($"{dateString}T{scheduleStartHourToString}:{scheduleStartMinuteToString}:00Z");
        var scheduleEndDate = DateTime.Parse($"{dateString}T{scheduleEndHourToString}:{scheduleEndMinuteToString}:00Z");
        return date >= scheduleStartDate && date <= scheduleEndDate;
    }

    private static string ParseIntToValidDateString(int value)
    {
        return value < 10 ? "0" + value : value.ToString();
    }
}