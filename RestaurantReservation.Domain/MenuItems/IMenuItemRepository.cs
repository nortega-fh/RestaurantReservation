namespace RestaurantReservation.Domain.MenuItems;

public interface IMenuItemRepository
{
    Task<IEnumerable<MenuItem>> GetAllAsync(string restaurantId, int pageSize, int pageNumber);
    Task<MenuItem?> GetByIdAsync(string restaurantId, string menuItemId);
    Task CreateAsync(MenuItem menuItem);
    Task UpdateAsync(MenuItem menuItem);
    Task DeleteAsync(string menuItemId);
}