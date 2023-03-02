using UrlShortener.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureCors();

builder.Services.ConfigureCommonService();

builder.Services.ConfigureAppSettings(builder.Configuration);

builder.Services.ConfigureSqlDBContext(builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "API Running !!!");

app.UseProcessingTimeCalculatorMiddleware();

app.Run();
