using CaseBankdata.Services.Interfaces;
using CaseBankdata.Services;
using Microsoft.EntityFrameworkCore;
using CaseBankdata.Data;
using CaseBankData.Services;


//ASP.NET Core Web application using Entity Framework core
var builder = WebApplication.CreateBuilder(args);

//Reads from appsettings
var configuration = builder.Configuration;

//Adding all the services used in the application
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//ILogger is out of the box and not needed here
//AddScoped means 
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITransferService, TransferService>();

builder.Services.AddDbContext<CaseBankdataDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//I don't use auth in this code-case, but typically I would use JWT
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
