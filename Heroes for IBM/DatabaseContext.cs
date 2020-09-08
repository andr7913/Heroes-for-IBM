using Heroes.Entity;
using Microsoft.EntityFrameworkCore;


namespace Heroes_for_IBM
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions opt) : base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Pet>()
                .HasOne(p => p.Hero)
                .WithMany(h => h.Pets)
                .OnDelete(DeleteBehavior.SetNull);
        }

        public System.Data.Entity.DbSet<Pet> Pets { get; set; }
        public System.Data.Entity.DbSet<Hero> Heroes { get; set; }

    }
}