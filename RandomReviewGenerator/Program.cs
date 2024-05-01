using Microsoft.Extensions.Caching.Memory;
using RandomReviewGenerator.Services;
using RandomReviewGenerator.Services.Interfaces;
using System.Reflection;

const string cacheKey = "markovChain";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IGenerateReviewService, GenerateReviewService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// Service to generate the Swagger xml file
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Services.GetService<IMemoryCache>()?.Set(cacheKey, GenerateReviewService.GenerateData());

// Does this need to be removed for Docker?
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();