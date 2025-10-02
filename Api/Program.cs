using ApiBase.Domain.Interfaces;
using ApiBase.Infra.UnitOfWork;
using Application.Roles;
using Application.Users;
using Domain;
using Domain.BlacklistedTokens;
using Infra.Middlewares;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.BlacklistedTokens;
using Repository.RefreshTokens;
using Repository.Roles;
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
builder.Services.AddScoped<IRepRefreshToken, RepRefreshToken>();
builder.Services.AddScoped<IRepBlacklistedToken, RepBlacklistedToken>();
builder.Services.AddScoped<IAplicRole, AplicRole>();
builder.Services.AddScoped<IMapperRole, MapperRole>();
builder.Services.AddScoped<IRepRole, RepRole>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
//app.UseMiddleware<JwtBlacklistMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
