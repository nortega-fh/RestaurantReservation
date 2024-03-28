using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using RestaurantReservation.API.Filters;
using RestaurantReservation.API.Repositories;
using RestaurantReservation.API.Services;
using RestaurantReservation.API.Settings;
using RestaurantReservation.API.Validators;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddValidatorsFromAssemblyContaining<CustomerValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("RestaurantReservationDb"));
builder.Services.AddSingleton<IRestaurantReservationDatabase, RestaurantReservationDatabase>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IUserService, UserService>();

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
builder.Services.AddMvc(options => options.Filters.Add<ModelValidationFilter>());
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();
app.MapGet("/", () => "Hello World!");

app.Run();