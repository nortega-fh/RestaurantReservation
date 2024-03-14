using RestaurantReservation.API.Repositories;
using RestaurantReservation.API.Services;
using RestaurantReservation.API.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("RestaurantReservationDb"));
builder.Services.AddSingleton<IRestaurantReservationDatabase, RestaurantReservationDatabase>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();
app.MapGet("/", () => "Hello World!");

app.Run();