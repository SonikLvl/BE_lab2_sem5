using BE_project.Data;
using BE_project.Data.Repositories;
using BE_project.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddControllers();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IRecordRepository, RecordRepository>();
builder.Services.AddScoped<IRecordService, RecordService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

var jwtKey = builder.Configuration["Jwt:Key"];

if (string.IsNullOrEmpty(jwtKey))
{
    throw new InvalidOperationException("JWT Key not found in configuration.");
}

var keyBytes = Encoding.UTF8.GetBytes(jwtKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // Параметри валідації токена
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false, 
        ValidateAudience = false, 
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ClockSkew = TimeSpan.Zero 
    };

    //  Обробники подій (еквівалент @jwt..._loader)
    options.Events = new JwtBearerEvents
    {
        // Ця подія спрацьовує, якщо токен є, але він недійсний
        OnAuthenticationFailed = (context) =>
        {
            // з'ясовуємо причину помилки
            string errorType = "invalid_token";
            if (context.Exception is SecurityTokenExpiredException)
            {
                errorType = "token_expired";
            }

            // Зберігаємо причину в HttpContext, щоб OnChallenge міг її прочитати
            context.HttpContext.Items["JwtErrorType"] = errorType;

            // Дозволяємо помилці пройти далі, що автоматично викличе OnChallenge
            return Task.CompletedTask;
        },

        OnChallenge = async (context) =>
        {
            // Зупиняємо стандартну обробку
            context.HandleResponse();
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";

            object responseBody;

            // Перевіряємо, чи OnAuthenticationFailed залишив нам повідомлення
            if (context.HttpContext.Items.TryGetValue("JwtErrorType", out var errorTypeObj))
            {
                // це була помилка "token_expired" або "invalid_token"
                string errorType = errorTypeObj as string;
                if (errorType == "token_expired")
                {
                    responseBody = new { message = "The token has expired.", error = "token_expired" };
                }
                else // "invalid_token"
                {
                    responseBody = new { message = "Signature verification failed.", error = "invalid_token" };
                }
            }
            else
            {
                // токен просто не надали (@jwt.unauthorized_loader)
                responseBody = new
                {
                    description = "Request does not contain an access token.",
                    error = "authorization_required",
                };
            }

            var jsonResponse = JsonSerializer.Serialize(responseBody);
            await context.Response.WriteAsync(jsonResponse);
        }
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<ApplicationDbContext>();

        dbContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    // This makes Swagger available at the root URL (http://localhost:8080/)
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
