global using PaymentDemoApi.Models;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using Ardalis.GuardClauses;
using PaymentDemoApi.Core.IConfiguration;
using PaymentDemoApi.Core.Data;
using PaymentDemoApi.Core.Services;
using PaymentDemoApi.Core.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

builder.Services.AddDbContext<PaymentDemoContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("Connection")));
builder.Services.AddControllers();

builder.Services.AddTransient<ICoOwnerService,CoOwnerService>();
builder.Services.AddTransient<IMonthDetailService, MonthDetailService>();

//adding the unit of work to  the DI container
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
  