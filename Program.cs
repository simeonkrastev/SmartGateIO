using Microsoft.EntityFrameworkCore;
using SmartGateIO.Database;

namespace SmartGateIO
{
	public class Program
	{
		public static void Main(string[] args)
		{
			// Use the ASP.NET to build the application
			WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
			builder.Services.AddControllers();
			builder.Services.AddDbContext<CheckinsDbContext>(
				opt => opt.UseInMemoryDatabase("Checkins"));
			
			// Confugurations for Swagger - a web tool to manually test API endpoints.
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			// Create an instance of the application
			WebApplication app = builder.Build();

			// Configurations for Swagger.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			// Configure the web server to provide the HTML/CSS/JS files
			app.UseDefaultFiles();
			app.UseStaticFiles();
			app.UseAuthorization();
			app.MapControllers();

			// Other configuration and startup code to be put here.
			// ...

			// Start the application
			app.Run();
		}
	}
}