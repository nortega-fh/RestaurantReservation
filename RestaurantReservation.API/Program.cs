using RestaurantReservation.API.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("RestaurantReservationDb"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();