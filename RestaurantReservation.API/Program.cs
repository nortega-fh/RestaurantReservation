using FluentValidation;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.IdentityModel.Tokens;
using RestaurantReservation.API.AuthHandlers;
using RestaurantReservation.API.Filters;
using RestaurantReservation.API.Validators.Customers;
using RestaurantReservation.Domain.Customers;
using RestaurantReservation.Domain.Employees;
using RestaurantReservation.Domain.MenuItems;
using RestaurantReservation.Domain.Orders;
using RestaurantReservation.Domain.Reservations;
using RestaurantReservation.Domain.Restaurants;
using RestaurantReservation.Domain.Tables;
using RestaurantReservation.Domain.Users;
using RestaurantReservation.Infrastructure;
using RestaurantReservation.Infrastructure.Repositories;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

var builder = WebApplication.CreateBuilder(args);

// AutoMapper configuration
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// FluentValidation Configuration
builder.Services.AddValidatorsFromAssemblyContaining<CustomerValidator>();
builder.Services.AddFluentValidationAutoValidation();

// DB Configuration
builder.Services.Configure<MongoDbParameters>(builder.Configuration.GetSection("RestaurantReservationDb"));
builder.Services.AddSingleton<IRestaurantReservationDatabase, RestaurantReservationDatabase>();

// Repositories
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
builder.Services.AddScoped<ITableRepository, TableRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Services
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<ITableService, TableService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IMenuItemService, MenuItemService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IOrderService, OrderService>();

// Components
builder.Services.AddScoped<IReservationAvailabilityChecker, ReservationAvailabilityChecker>();

// JWT Auth
builder.Services.AddScoped<ITokenGenerator, JwtTokenGenerator>();
builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Auth:Issuer"],
        ValidAudience = builder.Configuration["Auth:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(builder.Configuration["Auth:Secret"]))
    };
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

builder.Services.AddMvc(options => options.Filters.Add<ModelValidationFilter>());
builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();
app.MapGet("/", () => "Hello World!");

app.Run();