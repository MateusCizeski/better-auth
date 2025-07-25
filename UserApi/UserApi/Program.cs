using Application.Users;
using Base.Application;
using Base.Infrastructure.AppDbContext;
using Base.Repository;
using CrossCutting.JWT;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<AppDbContext>(provider =>
    provider.GetRequiredService<ApplicationDbContext>());

builder.Services.AddScoped<IRepUser, RepUser>();
builder.Services.AddScoped<IAplicUser, AplicUser>();
builder.Services.AddScoped<IMapperUser, MapperUser>();
builder.Services.AddScoped<RepUser>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
builder.Services.AddScoped(typeof(IApplicationBase<>), typeof(ApplicationBase<>));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
