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
        var dayOfWeek = date.DayOfWeek;
        var hours = date.Hour;
        var minutes = date.Minute;
        var restaurant = await GetByIdAsync(restaurantId);
        var isExceptionallyOpenOnDate = restaurant?.Schedule.ExceptionalOpenDays?.Contains(date) ?? false;
        var restaurantWeeklySchedule = restaurant?.Schedule.WeeklySchedule;
        var isDateAmongWorkingHours = (restaurantWeeklySchedule?.ContainsKey(dayOfWeek) ?? false)
                                      && restaurantWeeklySchedule[dayOfWeek].Find(scheduleBlock =>
                                          scheduleBlock.Start[0] >= hours && scheduleBlock.Start[1] >= minutes &&
                                          scheduleBlock.End[0] <= hours && scheduleBlock.End[1] <= minutes) is not null;
        return isExceptionallyOpenOnDate || isDateAmongWorkingHours;
    }
}