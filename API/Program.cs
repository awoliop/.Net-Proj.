using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using API.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// the code i added!!
builder.Services.AddControllers();
builder.Services.AddSingleton<IMongoClient>(new MongoClient(builder.Configuration.GetConnectionString("MongoDBConnection")));
builder.Services.AddScoped<DataContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();

