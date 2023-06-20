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

		[HttpPost]
		public async Task<IActionResult> ReceiveRfidData([FromBody]string body)
		{
			int tag = int.Parse(body);
			Console.WriteLine("POST request on api/checkin. RFID tag: " + tag);
			Console.WriteLine(Request.HttpContext.Connection.RemoteIpAddress);
			

			CheckinData checkinData = new CheckinData
			{
				RfidTag = tag,
				Date = DateTime.Now.ToString()
			};

			_context.AddCheckin(checkinData);
			CheckinResponse responseBody = new CheckinResponse { Name = "Aleksander Krasmatsov", Validation = true, DateAndTime = DateTime.Now.ToString()};
			return StatusCode(200, responseBody);
		}
	}

	class CheckinResponse
	{
		public string? Name { get; set; }
		public bool Validation { get; set; }
		public string DateAndTime { get; set; }
	}
}
