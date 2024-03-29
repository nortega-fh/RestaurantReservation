using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestaurantReservation.Infrastructure.Entities;

public class Order
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    [BsonElement("reservation_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ReservationId { get; set; } = null!;

    [BsonElement("employee_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string EmployeeId { get; set; } = null!;

    [BsonElement("order_date")]
    public DateTime OrderDate { get; set; }

    [BsonElement("total_amount")]
    public decimal TotalAmount { get; set; }
}