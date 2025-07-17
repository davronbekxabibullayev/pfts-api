using Pfts.Infrastucture.Extensions.DataSeeding.Role;
using Pfts.Infrastucture.Persistence.EntityFramework;

Console.Title = "Pfts.Api";

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDevhubLocalization(builder.Configuration)
    .AddApplication()
    .AddApplicationApi()
    .AddApplicationInfrastructure(builder.Configuration)
    .AddControllers().Services
    .AddApplicationAuth()
    .AddEndpointsApiExplorer()
    .AddApplicationSwagger()
    .ConfigureSwagger();

var app = builder.Build();

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

//app.MapGroup("account").MapIdentityApi<User>();
app.MapControllers();

app.Run();

static async void MigrateDatabase(IHost host)
{
    using var scope = host.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    var provider = scope.ServiceProvider;

    var mapper = provider.GetService<IMapper>();

    db.Database.Migrate();
    db.SeedAsync(mapper);
    new InitialRolesUserSeed()
            .SeedAsync(db, provider)
            .Wait();
}
