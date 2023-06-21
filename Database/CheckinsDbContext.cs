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

            CheckinData checkin3 = new CheckinData();
            checkin3.ID = 3;
            checkin3.RfidTag = 888888888;
            checkin3.Date = DateTime.Now.ToString();
            context.AddCheckin(checkin3);

            Account account1 = new Account();
            account1.ID = 1;
            account1.RfidTag = 1151021376;
            account1.Name = "Peter";
            account1.Status = "IN";
            context.AddAccount(account1);

            Account account2 = new Account();
            account2.ID = 2;
            account2.RfidTag = 888888888;
            account2.Name = "Ibrahim";
            account2.Status = "OUT";
            context.AddAccount(account2); 

            context.SaveChanges();

        }
	}
}
