using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
		public async Task<ActionResult<List<CheckinWebResponse>>> GetCheckinWebResponse()
		{

			Console.WriteLine("GET request on api/report");
            Console.WriteLine("Client user IP address is " + HttpContext.Connection.RemoteIpAddress);
			List<CheckinWebResponse> result = new List<CheckinWebResponse>();
			foreach (CheckinData data in _context.GetCheckins())
			{
				try
				{
                    Account account = _context.GetAccoountByTag(data.RfidTag);

                    CheckinWebResponse response = new CheckinWebResponse();
                    response.ID = data.ID;
                    response.Name = account.Name;
                    response.Date = DateTime.Now.ToString();
					response.ValidationStatus = true;
					response.Direction = data.Direction;
                    result.Add(response);
                }
				catch (KeyNotFoundException)
				{

					CheckinWebResponse response = new CheckinWebResponse();
					response.Date = DateTime.Now.ToString();
					response.ValidationStatus = false;
					response.Direction = "Going In";
					response.Name = "Not recognised";
                    result.Add(response);
                }
            }
            return result;
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

        public class CheckinWebResponse
		{
            public int ID { get; set; }
            public string Name { get; set; }
            public string Date { get; set; }
			public bool ValidationStatus { get; set; }
            public string Direction { get; set; }
        }
    }
}
