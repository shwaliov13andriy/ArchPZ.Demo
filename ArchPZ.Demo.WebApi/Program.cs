using ArchPZ.Demo.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Реєстрація сервісів Application шару (MediatR)
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(ArchPZ.Demo.Application.DTOs.OrderDto).Assembly));

// Реєстрація сервісів Infrastructure шару
builder.Services.AddInfrastructure();

// Підключення контролерів
builder.Services.AddControllers();

// OpenAPI / Swagger
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
