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
    public class AccountsContoller : ControllerBase
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
        [HttpGet("Id")]
        public async Task<ActionResult<Account>> GetAccount(string Id)
        {
            try
            {
            Account account = _context.GetAccountById(int.Parse(Id));
            return account;

            }
            catch (KeyNotFoundException ex)
            {  
                string message = ex.Message;
                return StatusCode(404, message);
            }

        }
        [HttpDelete("Id")]
        public async Task<ActionResult> DeleteAccounts(string Id)
        {
            try
            {
                Console.WriteLine($"Trying to delete account with Id{Id}");
                _context.DeleteAccount(int.Parse(Id));
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                string message = ex.Message;
                return StatusCode(404, message);
            }

        }
        [HttpPost("{Name}/{cardTag}")]
        public async Task<ActionResult<Account>> AddAccounts(string Name, int cardTag)
        {

            Console.WriteLine($"Trying to add an account with name{Name} and card tag{cardTag}.");
            Account account = new Account();
            account.Name = Name;
            account.RfidTag = cardTag;
            account.Status = "OUT";
            _context.AddAccount(account);
            return Ok(); 

        }
    }
}
