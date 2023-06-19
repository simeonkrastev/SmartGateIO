using Microsoft.AspNetCore.Mvc;
using SmartGateIO.Database;
using SmartGateIO.Models;

namespace SmartGateIO.Controllers
{
	[ApiController]
	[Route("api/report")]
	public class WebController : ControllerBase
	{
		private CheckinsDbContext _context;

		public WebController(CheckinsDbContext context)
		{
			_context = context;
		}

		// This async method returns Task<ActionResult<...>> so that it can be
		// run simultaneously while the server processes other requests.
		[HttpGet]
		public async Task<ActionResult<List<CheckinData>>> GetCheckinsData()
		{
			Console.WriteLine("GET request on api/report");
			return _context.GetCheckins();
		}
	}
}
