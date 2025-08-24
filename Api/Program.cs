using ApiBase.Domain.Interfaces;
using ApiBase.Infra.UnitOfWork;
using Application.Users;
using Domain.Jwt;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Repository;
using Repository.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ContextDataBase>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddScoped<DbContext>(provider => provider.GetRequiredService<ContextDataBase>());
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRepositoryUser, RepositoryUser>();
builder.Services.AddScoped<IApplicationUser, ApplicationUser>();
builder.Services.AddScoped<IMapperUser, MapperUser>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
