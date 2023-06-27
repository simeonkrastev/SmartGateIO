using Azure;
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

        public void DeleteAccount(int id)
        {
            Account account = GetAccountById(id);
            Accounts.Remove(account);
            SaveChanges();
        }

        public Account GetAccountById(int id)
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
        public Account GetAccoountByTag(int tag)
        {
            foreach (Account account in Accounts)
            {
                if (account.RfidTag == tag)
                {
                    return account;
                }
            }
            throw new KeyNotFoundException($"No user with tag:{tag}!");
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
            checkin1.Direction = "Going In";
			context.AddCheckin(checkin1);

            CheckinData checkin2 = new CheckinData();
            checkin2.ID = 2;
            checkin2.RfidTag = 888888888;
            checkin2.Date = DateTime.Now.ToString();
            checkin2.Direction = "Going In";
            context.AddCheckin(checkin2);

            CheckinData checkin3 = new CheckinData();
            checkin3.ID = 3;
            checkin3.RfidTag = 888888888;
            checkin3.Date = DateTime.Now.ToString();
            checkin3.Direction = "Going Out";
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
