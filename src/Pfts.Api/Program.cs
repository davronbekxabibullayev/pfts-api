using Microsoft.EntityFrameworkCore;
using Pfts.Api.Extensions;
using Pfts.Api.Localization.Extensions;
using Pfts.Api.Middlewares;
using Pfts.Application;
using Pfts.Domain.Models;
using Pfts.Infrastructure.Persistance.EntityFramework;


Console.Title = "Pfts.Api";

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddLocalization(builder.Configuration)
    .AddApplication()
    .AddApplicationApi()
    .AddControllers().Services
    .AddAppDbContext(builder.Configuration.GetConnectionString("DefaultConnection")!)
    .AddApplicationAuth()
    .AddEndpointsApiExplorer()
    .AddApplicationSwagger()
    .ConfigureSwagger();

var app = builder.Build();

app.MigrateDbContext<AppDbContext>((context, services) =>
{
    context.Database.Migrate();
});

MigrateDatabase(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseMiddleware<GlobalMiddlewareErrorHander>();
app.UseRequestLocalization();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

static void MigrateDatabase(IHost host)
{
    using var scope = host.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    var provider = scope.ServiceProvider;

    /*new InitialRolesUserSeed()
            .SeedAsync(db, provider)
            .Wait();*/
}
