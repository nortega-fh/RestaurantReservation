using FluentValidation;
using RestaurantReservation.API.Dtos.Requests;
using RestaurantReservation.API.Filters;
using RestaurantReservation.API.Repositories;
using RestaurantReservation.API.Services;
using RestaurantReservation.API.Settings;
using RestaurantReservation.API.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IValidator<CustomerCreateDto>, CustomerValidator>();

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("RestaurantReservationDb"));
builder.Services.AddSingleton<IRestaurantReservationDatabase, RestaurantReservationDatabase>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ModelValidationFilter>();
});

var app = builder.Build();

app.MapControllers();
app.MapGet("/", () => "Hello World!");

app.Run();