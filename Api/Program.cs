using Api.Extension;
using ApiBase.Domain.Interfaces;
using ApiBase.Infra.UnitOfWork;
using Application.Users;
using Domain;
using Infra.Helper;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.RefreshTokens;
using Repository.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ContextDataBase>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddScoped<DbContext>(provider => provider.GetRequiredService<ContextDataBase>());
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

#region User
builder.Services.AddScoped<IRepositoryUser, RepositoryUser>();
builder.Services.AddScoped<IApplicationUser, ApplicationUser>();
builder.Services.AddScoped<IMapperUser, MapperUser>();
#endregion

#region RefreshToken
builder.Services.AddScoped<IRepRefreshToken, RepRefreshToken>();
#endregion

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendDev", policy =>
        policy.WithOrigins(
            "http://localhost:5173",                 
            "https://meuservidorubuntu.com.br"
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials());
});

builder.Services.AddControllers();
builder.Services.AddJwtBearerAuthentication(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen();
builder.Services.AddApiDoc();

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseApiDoc();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseCors("AllowFrontendDev");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
