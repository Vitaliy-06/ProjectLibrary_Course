using Library.Model;
using Microsoft.EntityFrameworkCore;
namespace Library.Data
{
	public class AppDbContext : DbContext
	{
		private readonly string connStr = "Server=(LocalDB)\\MSSQLLocalDB;Database=LibraryDB;Trusted_Connection=True;";
		
		public DbSet<Book> Books { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(connStr);
		}

	}
}
