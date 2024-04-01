namespace RestaurantReservation.API.Contracts.Responses;

public class CustomerResponse
{
    public string Id { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}