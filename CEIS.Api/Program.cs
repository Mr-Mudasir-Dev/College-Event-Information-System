using CEIS.Api.Extensions;
using CEIS.Api.Middleware;
using CEIS.Application;
using CEIS.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddJwtAuthentication(builder.Configuration)
    .AddApiServices();

var app = builder.Build();

app.UseCors("AllowAngular");

using (var scope = app.Services.CreateScope())
{
    await CEIS.Infrastructure.Identity.RoleSeeder.SeedRolesAsync(scope.ServiceProvider);
    await CEIS.Infrastructure.Data.AdminSeeder.SeedAdminAsync(scope.ServiceProvider);
}
app.UseStaticFiles();
app.UseMiddleware<ExceptionMiddleware>();

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
