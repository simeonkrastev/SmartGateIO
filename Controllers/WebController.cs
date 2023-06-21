using Microsoft.AspNetCore.Mvc;
using SmartGateIO.Database;
using SmartGateIO.Models;
using System.Collections.Generic;

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
            Console.WriteLine("Client user IP address is " + HttpContext.Connection.RemoteIpAddress);
            return _context.GetCheckins();
		}

        [HttpGet("Id")]
        public async Task<ActionResult<List<CheckinData>>> GetCheckinAccount(string Id)
        {
            List<CheckinData> result = new List<CheckinData>();
			Account account = _context.GetAccount(int.Parse(Id));
			foreach (CheckinData checkin in _context.GetCheckins())
			{
				if (checkin.RfidTag == account.RfidTag)
				{
					result.Add(checkin);
				}
			}

            return result;

        }
    }
}
