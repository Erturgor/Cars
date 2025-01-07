using Microsoft.EntityFrameworkCore;
using Cars.Application.Cars;
using Cars.Infrastructure;
using FluentValidation.AspNetCore;
using FluentValidation;
using Cars.API;
using Microsoft.AspNetCore.Identity;
using Cars.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(opt =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg =>
cfg.RegisterServicesFromAssembly(typeof(List.Handler).Assembly));
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Create>();
builder.Services.AddValidatorsFromAssemblyContaining<Edit>();
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
    });
});
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    opt.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<DataContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context, userManager, roleManager);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured");
}
app.Run();
