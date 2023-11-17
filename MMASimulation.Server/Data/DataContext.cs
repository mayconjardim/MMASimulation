using Microsoft.EntityFrameworkCore;
using MMASimulation.Shared.Fighters;

namespace MMASimulation.Server.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Fighter> Fighters { get; set; }
        public DbSet<FighterRatings> FighterRatings { get; set; }
        public DbSet<FighterStrategies> FighterStrategies { get; set; }
        public DbSet<FighterStyles> FighterStyles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Fighter>()
           .HasOne(f => f.FighterRatings)
           .WithOne(fr => fr.Fighter)
           .HasForeignKey<FighterRatings>(fr => fr.FighterId)
           .IsRequired();

            modelBuilder.Entity<Fighter>()
            .HasOne(f => f.FighterStrategies)
            .WithOne(fr => fr.Fighter)
            .HasForeignKey<FighterStrategies>(fr => fr.FighterId)
            .IsRequired();

            modelBuilder.Entity<Fighter>()
            .HasOne(f => f.FighterStyles)
            .WithOne(fr => fr.Fighter)
            .HasForeignKey<FighterStyles>(fr => fr.FighterId)
            .IsRequired();

        }

    }
}