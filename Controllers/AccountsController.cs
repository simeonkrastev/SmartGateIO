using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartGateIO.Database;
using SmartGateIO.Models;
using System.Collections.Generic;
using static SmartGateIO.Controllers.WebController;

namespace SmartGateIO.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsContoller
    {
        private CheckinsDbContext _context;

        public AccountsContoller(CheckinsDbContext context)
        {
            _context = context;
        }



        [HttpGet]
        public async Task<ActionResult<List<Account>>> GetAccounts()
        {
            return _context.GetAccounts();
            
        }
    }
}
