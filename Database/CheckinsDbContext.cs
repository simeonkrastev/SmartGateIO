using Microsoft.EntityFrameworkCore;
using SmartGateIO.Models;

namespace SmartGateIO.Database
{
	// The DbContext classes are used as an interface to work directly with the database.
	public class CheckinsDbContext : DbContext
	{
		// Represents the Checkins table.
		public DbSet<CheckinData> Checkins { get; set; }

		// Constructor method for the DbContext
		public CheckinsDbContext(DbContextOptions<CheckinsDbContext> options)
			: base(options) {}

		// Inserts a new checkin to the database.
		public void AddCheckin(CheckinData checkinData)
		{
			Checkins.Add(checkinData);
			SaveChanges();
		}

		// Reads all checkins from the database.
		public List<CheckinData> GetCheckins()
		{
			return Checkins.ToList();
		}
	}
}
