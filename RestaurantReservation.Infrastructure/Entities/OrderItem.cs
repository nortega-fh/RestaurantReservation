using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestaurantReservation.Infrastructure.Entities;

public class OrderItem
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    [BsonElement("order_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string OrderId { get; set; } = null!;

    [BsonElement("item_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ItemId { get; set; } = null!;

    [BsonElement("quantity")]
    public int Quantity { get; set; }
}