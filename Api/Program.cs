using ApiBase.Core.Domain.Interfaces;
using ApiBase.Core.Infra.UnitOfWork;
using Application.Users;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ContextDataBase>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<DbContext>(provider => provider.GetRequiredService<ContextDataBase>());
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRepositoryUser, RepositoryUser>();
builder.Services.AddScoped<IApplicationUser, ApplicationUser>();

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
