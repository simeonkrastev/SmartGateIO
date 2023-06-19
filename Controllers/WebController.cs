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

		[HttpGet]
		public async Task<ActionResult<List<CheckinData>>> GetCheckinsData()
		{
			Console.WriteLine("GET request on api/report");
			return _context.GetCheckins();
		}
	}
}
