using MijnGeweldigeAPI.WebApi;
using MijnGeweldigeAPI.WebApi.Controllers;

var builder = WebApplication.CreateBuilder(args);


var sqlConnectionString = builder.Configuration.GetValue<string>("SqlConnectionString");
var sqlConnectionStringFound = !string.IsNullOrWhiteSpace(sqlConnectionString);
builder.Services.AddTransient(o => new EnvironmentRepository(sqlConnectionString));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapGet("/", () => $"The API is up . Connection string found: {(sqlConnectionStringFound ? "" : "")}");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
