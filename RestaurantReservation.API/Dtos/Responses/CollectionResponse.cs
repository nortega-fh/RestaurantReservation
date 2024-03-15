using System.Runtime.Serialization;

namespace RestaurantReservation.API.Dtos.Responses;

public class CollectionResponse<T> where T : class
{
    public ResponseMetadata Metadata { get; set; } = null!;
    public IEnumerable<T> Items { get; set; } = null!;
}

public record ResponseMetadata(int TotalItems, int PageSize, int PageNumber);