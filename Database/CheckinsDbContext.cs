using Microsoft.EntityFrameworkCore;
using SmartGateIO.Models;

namespace SmartGateIO.Database
{
	public class CheckinsDbContext : DbContext
	{
		public DbSet<CheckinData> Checkins { get; set; }
		public DbSet<Account> Accounts { get; set; }

		public CheckinsDbContext(DbContextOptions<CheckinsDbContext> options)
			: base(options)
		{
		}

        public void AddCheckin(CheckinData checkinData)
		{
			Checkins.Add(checkinData);
			SaveChanges();
		}

		public List<CheckinData> GetCheckins()
		{
			return Checkins.ToList();
		}



        public void AddAccount(Account account)
        {
            Accounts.Add(account);
            SaveChanges();
        }

        public List<Account> GetAccounts()
        {
            return Accounts.ToList();
        }
        public Account GetAccount(int id) 
        {
            foreach (Account account in Accounts) 
            {
                if (account.ID == id)
                {
                    return account;
                }
                
            }
            throw new KeyNotFoundException($"No user with Id:{id}!");
        }

    }
	class MockDataGenerator
    {
		public static void Seed(CheckinsDbContext context) 
		{
			CheckinData checkin1 = new CheckinData();
			checkin1.ID= 1;
			checkin1.RfidTag = 1151021376;
			checkin1.Date = DateTime.Now.ToString();
			context.AddCheckin(checkin1);

            CheckinData checkin2 = new CheckinData();
            checkin2.ID = 2;
            checkin2.RfidTag = 888888888;
            checkin2.Date = DateTime.Now.ToString();
            context.AddCheckin(checkin2);

            Account account1 = new Account();
            account1.ID = 1;
            account1.RfidTag = 1151021376;
            account1.Name = "Peter";
            context.AddAccount(account1);

            Account account2 = new Account();
            account1.ID = 2;
            account1.RfidTag = 888888888;
            account1.Name = "Ibrahim";
            context.AddAccount(account1); 

            context.SaveChanges();

        }
	}
}
