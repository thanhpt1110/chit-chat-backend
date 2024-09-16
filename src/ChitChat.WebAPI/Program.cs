using ChitChat.Application;
using ChitChat.DataAccess;
using ChitChat.DataAccess.Data;
using ChitChat.Infrastructure;
using ChitChat.WebAPI;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddDataAccessService(builder.Configuration)
    .AddApplicationServices();
builder
    .AddInfrastructure()
    .AddWebAPI();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
ApplyMigration();
app.UseHttpsRedirection();
app.AddInfrastuctureApplication();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

void ApplyMigration() // apply new pending migration
{
    using (var scope = app.Services.CreateScope()) // Get the services 
    {
        var _db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        if (_db.Database.GetPendingMigrations().Count() > 0) // apply all the pending migration
        {
            _db.Database.Migrate();
        }
    }
}