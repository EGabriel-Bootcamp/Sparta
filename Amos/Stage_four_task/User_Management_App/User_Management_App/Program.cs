using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using User_Management.DataAccess.Implementation;
using User_Management.DataAccess.User_Context;
using User_Management.Domain.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add Entity Framework
builder.Services.AddDbContext<UserManagement_DbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("UserConnections")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

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
