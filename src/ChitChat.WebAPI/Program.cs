using Chitchat.DataAccess.Data;
using ChitChat.Application;
using ChitChat.DataAccess;
using ChitChat.Infrastructure;
using ChitChat.Infrastructure.Validations;
using ChitChat.WebAPI;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
DotNetEnv.Env.Load();
ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
builder.Services
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddControllers(config => config.Filters.Add(typeof(ValidateModelAttribute)));
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
// Migrate Database
using var scope = app.Services.CreateAsyncScope();
await AutomatedMigration.MigrateAsync(scope.ServiceProvider);

app.UseHttpsRedirection();
app.AddInfrastuctureApplication();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

/*void ApplyMigration() // apply new pending migration
{
    using (var scope = app.Services.CreateScope()) // Get the services 
    {
        var _db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        if (_db.Database.GetPendingMigrations().Count() > 0) // apply all the pending migration
        {
            _db.Database.Migrate();
        }
    }
}*/
