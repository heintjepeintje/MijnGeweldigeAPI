using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MijnGeweldigeAPI.WebApi;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MijnGeweldigeAPI.WebApi.Controllers;

[ApiController]
[Route("Environments")]
public class EnvironmentController : ControllerBase
{
	private readonly static List<Environment2D> environments = [
		new Environment2D(
			"Main Environment", 100, 200
		),
		new Environment2D(
			"Secondary Environment", 50, 100
		),
	];


	private readonly ILogger<EnvironmentController> _logger;

	public EnvironmentController(ILogger<EnvironmentController> logger)
	{
		_logger = logger;
	}

	[HttpGet(Name = "ReadEnvironments")]
	public ActionResult<IEnumerable<Environment2D>> Get()
	{
		return environments;
	}

	[HttpGet("{string:required}", Name = "ReadEnvironemntsByName")]
	public ActionResult<Environment2D> Get(string name)
	{
		Environment2D? environment = GetEnvironment(name);
		if (environment == null)
			return NotFound();

		return environment;
	}


	[HttpPost(Name = "CreateEnvironment")]
	public ActionResult Add(Environment2D environment)
	{
		if (GetEnvironment(environment.Name) != null)
			return BadRequest("Environment with name " + environment.Name + " already exists.");

		environments.Add(environment);
		return Created();
	}


	[HttpPut("{string:required}", Name = "UpdateEnvironmentByName")]
	public IActionResult Update(string name, Environment2D newEnvironment)
	{
		if (name != newEnvironment.Name)
			return BadRequest("The id of the object did not match the id of the route");

		Environment2D? environmentToUpdate = GetEnvironment(newEnvironment.Name);
		if (environmentToUpdate == null)
			return NotFound();

		environments.Remove(environmentToUpdate);
		environments.Add(newEnvironment);

		return Ok();
	}

	[HttpDelete("{string:required}", Name = "DeleteEnvironmentByName")]
	public IActionResult Update(string name)
	{
		Environment2D? environmentToDelete = GetEnvironment(name);
		if (environmentToDelete == null)
			return NotFound();

		environments.Remove(environmentToDelete);
		return Ok();
	}

	private Environment2D? GetEnvironment(string name)
	{
		foreach (Environment2D environment in environments)
		{
			if (environment.Name == name)
				return environment;
		}

		return null;
	}
}
