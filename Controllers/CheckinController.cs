using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
		public async Task<IActionResult> ReceiveRfidData([FromBody] string body)
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
			List<Account> accounts = _context.GetAccounts();
			bool cardValid = false;
			foreach (Account account in accounts)
			{
				if (account.RfidTag == tag)
				{
					cardValid = true;
					if (account.Status == "IN")
					{
						account.Status = "OUT";
					}
					else
					{
						account.Status = "IN";
					}
				}
			}
			CheckinResponse responseBody = new CheckinResponse {
				Name = "Aleksander Krasmatsov",
				Validation = cardValid,
				DateAndTime = DateTime.Now.ToString()
			};
			return StatusCode(200, responseBody);
		}
        [HttpGet("Id")]
        public async Task<ActionResult<List<CheckinData>>> GetCheckinData(string Id)
        {
			List<CheckinData> result = new List<CheckinData>();
            Account account = _context.GetAccount(int.Parse(Id));
            foreach (CheckinData checkin in _context.GetCheckins())
            {
				if (checkin.ID == account.ID)
				{
					result.Add(checkin);
				}
            }
			return result; 
        }
    }

    

    class CheckinResponse
	{
		public string Name { get; set; }
		public bool Validation { get; set; }
		public string DateAndTime { get; set; }
	}
}
