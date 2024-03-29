namespace RestaurantReservation.API.Contracts.Responses;

public class ErrorResponse
{
    public string RequestPath { get; set; } = null!;

    public IDictionary<string, string[]> Errors { get; set; } = null!;
}