using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Repositories;

namespace RestaurantReservation.Domain.Services;

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

    public async Task<Restaurant?> CreateAsync(Restaurant restaurant)
    {
        return await _repository.CreateAsync(restaurant);
    }

    public async Task UpdateAsync(string id, Restaurant restaurant)
    {
        await _repository.UpdateAsync(id, restaurant);
    }

    public async Task DeleteAsync(string id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<bool> RestaurantExistsWithIdAsync(string id)
    {
        return await GetByIdAsync(id) is not null;
    }
}