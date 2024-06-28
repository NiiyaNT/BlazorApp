using BlazorAppTest.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppTest.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<AccountDetail> AccountDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountDetail>()
                .HasOne(a => a.Person)
                .WithMany(p => p.AccountDetails)
                .HasForeignKey(a => a.PersonId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
