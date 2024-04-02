namespace RestaurantReservation.API.Contracts.Responses;

public class ErrorResponse
{
    public string RequestPath { get; set; } = null!;

    public Dictionary<string, string[]> Errors { get; set; } = null!;
}