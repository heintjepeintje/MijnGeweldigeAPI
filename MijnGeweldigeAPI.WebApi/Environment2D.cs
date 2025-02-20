namespace MijnGeweldigeAPI.WebApi;

public class Environment2D
{
	public Guid Id { get; set; }

    public string Name { get; set; }

	public int MaxWidth { get; set; }
	public int MaxHeight { get; set; }
	public Environment2D(string name, int width, int height)
	{
		this.Name = name;
		this.MaxWidth= width;
		this.MaxHeight = height;
	}

}
