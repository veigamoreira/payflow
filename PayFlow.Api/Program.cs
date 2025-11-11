using Microsoft.OpenApi.Models;
using PayFlow.Application.Interfaces;
using PayFlow.Application.Services;
using PayFlow.Infrastructure.Factories;
using PayFlow.Infrastructure.Providers;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Força escuta na porta 80 (necessário para Docker)
builder.WebHost.UseUrls("http://+:80");

// Controllers + JSON config
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
    });

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PayFlow API", Version = "v1" });
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Application services
builder.Services.AddScoped<PaymentService>();

// Infrastructure
builder.Services.AddScoped<FastPayProvider>();
builder.Services.AddScoped<SecurePayProvider>();
builder.Services.AddScoped<IPaymentProviderFactory, PaymentProviderFactory>();

var app = builder.Build();

// Middleware
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PayFlow API v1");
    });
}

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
