using Dapper;
using Microsoft.Data.SqlClient;

namespace MijnGeweldigeAPI.WebApi.Controllers;
public class EnvironmentRepository
{
	private readonly string _connectionString;

	public EnvironmentRepository(string connection)
	{
		_connectionString = connection;
	}

	public async Task<Environment2D> InsertAsync(Environment2D environment)
	{
		Guid guid = Guid.NewGuid();
		environment.Id = guid;
		SqlConnection connection = new SqlConnection(_connectionString);
		await connection.ExecuteAsync("INSERT INTO [Environments] (Id, Name, MaxWidth, MaxHeight) VALUES (@Id, @Name, @MaxWidth, @MaxHeight)", environment);
		Console.WriteLine("Hallo larsje!");
		return environment;
	}

	public async Task<Environment2D?> ReadAsync(Guid id)
	{
		SqlConnection connection = new(_connectionString);
		return await connection.QuerySingleOrDefaultAsync<Environment2D>("SELECT * FROM [dbo.Environments] WHERE Id = @Id", new { id });
	}

	public async Task<IEnumerable<Environment2D?>> ReadAsync()
	{
		SqlConnection connection = new(_connectionString);
		return await connection.QueryAsync<Environment2D>("SELECT * FROM [dbo.Environments]");
	}

	public async Task UpdateAsync(Guid id, Environment2D environment)
	{
		SqlConnection connection = new(_connectionString);
		await connection.ExecuteAsync($"UPDATE [dbo.Environments] SET Name = @Name, MaxWidth = @MaxWidth, MaxHeight = @MaxHeight", environment);
	}

	public async Task DeleteAsync(Guid id)
	{
		SqlConnection connection = new(_connectionString);
		await connection.ExecuteAsync($"DELETE FROM [dbo.Environments] WHERE Id = @Id", id);
	}


}
