using ApiBase.Domain.Interfaces;
using ApiBase.Infra.UnitOfWork;
using Application.Users;
using Domain.Jwt;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
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
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "User API", 
        Version = "v1" 
    });
});

var app = builder.Build();

app.UseSwagger(c =>
{
    c.RouteTemplate = "api/users/{documentName}/swagger.json";
});

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/api/users/v1/swagger.json", "User API v1");

    app.UseSwaggerUI(c => 
    {
        c.RoutePrefix = "api/users/swagger";
    });
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
