using LibraryManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<LibraryManagementSystem.Services.IBookService, LibraryManagementSystem.Services.BookService>();
builder.Services.AddScoped<LibraryManagementSystem.Services.IUserService, LibraryManagementSystem.Services.UserService>();
builder.Services.AddScoped<LibraryManagementSystem.Services.IBorrowService, LibraryManagementSystem.Services.BorrowService>();
builder.Services.AddScoped<LibraryManagementSystem.Services.INotificationService, LibraryManagementSystem.Services.EmailNotificationService>();
builder.Services.AddScoped<LibraryManagementSystem.Services.IBookAvailabilityService, LibraryManagementSystem.Services.BookService>();
builder.Services.AddScoped<LibraryManagementSystem.Services.IAuthService, LibraryManagementSystem.Services.AuthService>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var jwtSettings = builder.Configuration.GetSection("Jwt");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
