using MijnGeweldigeAPI.WebApi;
using MijnGeweldigeAPI.WebApi.Controllers;

var builder = WebApplication.CreateBuilder(args);

var sqlConnectionString = builder.Configuration["SqlConnectionString"];
Console.WriteLine(sqlConnectionString);
builder.Services.AddTransient(o => new EnvironmentRepository(sqlConnectionString));
Environment2D environment = new("Mijn geweldige environment", 100, 200);

EnvironmentRepository repository = new EnvironmentRepository(sqlConnectionString);
await repository.InsertAsync(environment);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
