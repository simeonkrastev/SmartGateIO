using Microsoft.AspNetCore.Mvc;
using SmartGateIO.Database;
using SmartGateIO.Models;

namespace SmartGateIO.Controllers
{
	[ApiController]
	[Route("api/checkin")]
	public class CheckinController : ControllerBase
	{
		private CheckinsDbContext _context;

		public CheckinController(CheckinsDbContext context)
		{
			_context = context;
		}

		// This async method returns Task<ActionResult<...>> so that it can be
		// run simultaneously while the server processes other requests.
		[HttpPost]
		public async Task<IActionResult> ReceiveRfidData([FromBody]string body)
		{
			int tag = int.Parse(body);
			Console.WriteLine("POST request on api/checkin. RFID tag: " + tag);

			CheckinData checkinData = new CheckinData
			{
				RfidTag = tag,
				Date = DateTime.Now.ToString()
			};

			_context.AddCheckin(checkinData);

			return StatusCode(200);
		}
	}
}
