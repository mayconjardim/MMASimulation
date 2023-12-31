﻿using Microsoft.EntityFrameworkCore;
using MMASimulation.Shared.Models.Fighters;
using MMASimulation.Shared.Models.Fights;

namespace MMASimulation.Server.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Fighter> Fighters { get; set; }
        public DbSet<FighterRatings> FighterRatings { get; set; }
        public DbSet<FighterStrategies> FighterStrategies { get; set; }
        public DbSet<FighterStyles> FighterStyles { get; set; }

        public DbSet<Fight> Fights { get; set; }
        public DbSet<FightPBP> FightPBPs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Fighter
            modelBuilder.Entity<Fighter>()
            .HasOne(f => f.FighterRatings)
            .WithOne(fr => fr.Fighter)
            .HasForeignKey<FighterRatings>(fr => fr.FighterId)
            .IsRequired();

            modelBuilder.Entity<Fighter>()
            .HasOne(f => f.FighterStrategies)
            .WithOne()
            .HasForeignKey<FighterStrategies>(fr => fr.FighterId)
            .IsRequired();

            modelBuilder.Entity<Fighter>()
            .HasOne(f => f.FighterStyles)
            .WithOne()
            .HasForeignKey<FighterStyles>(fr => fr.FighterId)
            .IsRequired();

            //Fight
            modelBuilder.Entity<Fight>()
            .HasOne(f => f.Fighter1)
            .WithMany()
            .HasForeignKey(f => f.Fighter1Id)
            .IsRequired();

            modelBuilder.Entity<Fight>()
            .HasOne(f => f.Fighter2)
            .WithMany()
            .HasForeignKey(f => f.Fighter2Id)
            .IsRequired();

            modelBuilder.Entity<Fight>()
            .HasMany(f => f.Pbp)
            .WithOne(pbp => pbp.Fight)
            .OnDelete(DeleteBehavior.Cascade);

        }

    }
}