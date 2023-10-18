using healthcareBackend_.NET.Models;
using Microsoft.EntityFrameworkCore;

namespace healthcareBackend_.NET.Data
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<MedItems> MedItems { get; set; }
		public DbSet<MedCart> MedCart { get; set; }

		public DbSet<UserControl> UserControl { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			modelBuilder.Entity<MedItems>()
				.HasOne(item => item.MedCategory)
				.WithMany()
				.HasForeignKey(item => item.CategoryId);

			modelBuilder.Entity<MedCart>()
				.HasOne(cart => cart.MedItems)
				.WithMany()
				.HasForeignKey(cart => cart.ItemId);
		}

		public DbSet<healthcareBackend_.NET.Models.MedCategory>? MedCategory { get; set; }
	}
}