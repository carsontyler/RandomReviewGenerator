
using Microsoft.Extensions.Caching.Memory;
using RandomReviewGenerator.Controllers;

const string cacheKey = "markovChain";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Services.GetService<IMemoryCache>().Set(cacheKey, GenerateReviewController.GenerateData());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();