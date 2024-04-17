namespace RestaurantReservation.API.Contracts.Responses.API;

public class ErrorResponse
{
    public string RequestPath { get; set; } = null!;

    public Dictionary<string, string[]> Errors { get; set; } = null!;
}