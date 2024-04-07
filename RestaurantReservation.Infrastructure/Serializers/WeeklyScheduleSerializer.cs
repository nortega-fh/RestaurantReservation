using MongoDB.Bson;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using RestaurantReservation.Domain.Restaurants;

namespace RestaurantReservation.Infrastructure.Serializers;

public class WeeklyScheduleSerializer : DictionarySerializerBase<Dictionary<DayOfWeek, List<ScheduleBlock>>>
{
    public WeeklyScheduleSerializer() : base(DictionaryRepresentation.Document,
        new EnumSerializer<DayOfWeek>(BsonType.String),
        new EnumerableInterfaceImplementerSerializer<List<ScheduleBlock>>())
    {
    }

    protected override Dictionary<DayOfWeek, List<ScheduleBlock>> CreateInstance()
    {
        return new Dictionary<DayOfWeek, List<ScheduleBlock>>();
    }
}