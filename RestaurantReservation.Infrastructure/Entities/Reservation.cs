using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestaurantReservation.Infrastructure.Entities;

public class Reservation
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("reservation_date")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime ReservationDate { get; set; }

    [BsonElement("party_size")]
    public int PartySize { get; set; }

    [BsonElement("customer_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string CustomerId { get; set; } = null!;

    [BsonElement("restaurant_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string RestaurantId { get; set; } = null!;

    [BsonElement("table_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string TableId { get; set; } = null!;
}