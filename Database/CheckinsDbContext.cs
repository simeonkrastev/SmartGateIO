using Microsoft.EntityFrameworkCore;
using SmartGateIO.Models;

namespace SmartGateIO.Database
{
	public class CheckinsDbContext : DbContext
	{
		public DbSet<CheckinData> Checkins { get; set; }

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
			context.SaveChanges();
        }
	}
}
