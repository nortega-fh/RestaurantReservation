using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestaurantReservation.API.Entities;

public class OrderItem
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("order_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string OrderId { get; set; }

    [BsonElement("item_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ItemId { get; set; }

    [BsonElement("quantity")]
    public int Quantity { get; set; }
}