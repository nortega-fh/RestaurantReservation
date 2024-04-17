namespace RestaurantReservation.Domain.MenuItems;

public class MenuItemService : IMenuItemService
{
    private readonly IMenuItemRepository _repository;

    public MenuItemService(IMenuItemRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<IEnumerable<MenuItem>> GetAllAsync(string restaurantId, int pageSize, int pageNumber)
    {
        return await _repository.GetAllAsync(restaurantId, pageSize, pageNumber);
    }

    public async Task<MenuItem?> GetByIdAsync(string restaurantId, string menuItemId)
    {
        return await _repository.GetByIdAsync(restaurantId, menuItemId);
    }

    public async Task CreateAsync(MenuItem menuItem)
    {
        await _repository.CreateAsync(menuItem);
    }

    public async Task UpdateAsync(MenuItem menuItem)
    {
        await _repository.UpdateAsync(menuItem);
    }

    public async Task DeleteAsync(string menuItemId)
    {
        await _repository.DeleteAsync(menuItemId);
    }

    public async Task<bool> ExistsWithIdAndRestaurantAsync(string restaurantId, string menuItemId)
    {
        return await GetByIdAsync(restaurantId, menuItemId) is not null;
    }
}