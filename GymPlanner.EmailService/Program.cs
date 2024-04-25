using EmailService.Services;
using Hangfire;
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHangfire(s => s.UseSqlServerStorage(builder.Configuration.GetConnectionString("HangFire")));

builder.Services.AddHangfireServer();
builder.Services.AddTransient<RabbitMqListener>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHangfireDashboard("/hangfire");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var rabbitMqListener = app.Services.GetRequiredService<RabbitMqListener>();

RecurringJob.AddOrUpdate("emailJob", () => rabbitMqListener.Consume(), "*/5 * * * *"); // Запуск каждые 5 минут

app.Run();