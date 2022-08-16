using CarSupplier.Api;
using CarSupplier.BusinessLogic.Services;
using CarSupplier.DataAccess.MSSQL;
using CarSupplier.DataAccess.MSSQL.Repositories;
using CarSupplier.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.AddAutoMapper(typeof(DataAccessMappingProfile), typeof(ApiMappingProfile));

// Add services to the container.
builder.Services.AddTransient<ICarDealershipService, CarDealershipService>();

builder.Services.AddTransient<ICarDealershipRepository, CarDealershipRepository>();

builder.Services.AddDbContext<CarSupplierContext>(x =>
    x.UseSqlServer(builder.Configuration.GetConnectionString("CarSupplierContext")));

builder.Services.AddControllers();
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

app.UseCors(options => options
    .WithOrigins(new[] { "http://localhost:4200" })
    .AllowAnyHeader()
    .AllowAnyMethod());

app.UseAuthorization();

app.MapControllers();

app.Run();
