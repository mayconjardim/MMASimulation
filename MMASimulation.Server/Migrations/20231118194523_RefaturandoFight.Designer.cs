﻿// <auto-generated />
using System;
using MMASimulation.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MMASimulation.Server.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231118194523_RefaturandoFight")]
    partial class RefaturandoFight
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("MMASimulation.Shared.Models.Fighters.Fighter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Draw")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Face")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Loss")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("WeightClass")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Wins")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Fighters");
                });

            modelBuilder.Entity("MMASimulation.Shared.Models.Fighters.FighterRatings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Aggressiveness")
                        .HasColumnType("REAL");

                    b.Property<double>("Agility")
                        .HasColumnType("REAL");

                    b.Property<double>("ClinchGrappling")
                        .HasColumnType("REAL");

                    b.Property<double>("ClinchStriking")
                        .HasColumnType("REAL");

                    b.Property<double>("Conditioning")
                        .HasColumnType("REAL");

                    b.Property<double>("Control")
                        .HasColumnType("REAL");

                    b.Property<double>("Dodging")
                        .HasColumnType("REAL");

                    b.Property<int>("FighterId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Gnp")
                        .HasColumnType("REAL");

                    b.Property<double>("GroundGame")
                        .HasColumnType("REAL");

                    b.Property<double>("Kicking")
                        .HasColumnType("REAL");

                    b.Property<double>("KoResistance")
                        .HasColumnType("REAL");

                    b.Property<double>("Motivation")
                        .HasColumnType("REAL");

                    b.Property<double>("Punching")
                        .HasColumnType("REAL");

                    b.Property<double>("Strength")
                        .HasColumnType("REAL");

                    b.Property<double>("SubDefense")
                        .HasColumnType("REAL");

                    b.Property<double>("Submission")
                        .HasColumnType("REAL");

                    b.Property<double>("Takedowns")
                        .HasColumnType("REAL");

                    b.Property<double>("TakedownsDef")
                        .HasColumnType("REAL");

                    b.Property<double>("Toughness")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("FighterId")
                        .IsUnique();

                    b.ToTable("FighterRatings");
                });

            modelBuilder.Entity("MMASimulation.Shared.Models.Fighters.FighterStrategies", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("FighterId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StratAvoidClinch")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StratClinchTakedowns")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StratClinching")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StratDirtyBoxing")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StratGNP")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StratKicking")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StratLNP")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StratPositioning")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StratPunching")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StratStandUp")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StratSub")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StratTakedowns")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StratThaiClinch")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FighterId")
                        .IsUnique();

                    b.ToTable("FighterStrategies");
                });

            modelBuilder.Entity("MMASimulation.Shared.Models.Fighters.FighterStyles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClinchType")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("DirtyBoxing")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DirtyFighting")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("EasySubs")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FancyKicks")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FancyPunches")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FancySubmissions")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FighterId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FightingStyle")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("JudoTD")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("PullsGuard")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Stalling")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TacticalStyle")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("TechSubs")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ThaiClinch")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("UseKneesGround")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("UseSoccerKicks")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("UseStomps")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("WrestlingTD")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FighterId")
                        .IsUnique();

                    b.ToTable("FighterStyles");
                });

            modelBuilder.Entity("MMASimulation.Shared.Models.Fights.Fight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Fighter1Id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Fighter2Id")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("GeneratePBP")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Happened")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumberRounds")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("TitleBout")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Fighter1Id");

                    b.HasIndex("Fighter2Id");

                    b.ToTable("Fights");
                });

            modelBuilder.Entity("MMASimulation.Shared.Models.Fights.FightPBP", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("FightId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PbpData")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FightId");

                    b.ToTable("FightPBPs");
                });

            modelBuilder.Entity("MMASimulation.Shared.Models.Fighters.FighterRatings", b =>
                {
                    b.HasOne("MMASimulation.Shared.Models.Fighters.Fighter", "Fighter")
                        .WithOne("FighterRatings")
                        .HasForeignKey("MMASimulation.Shared.Models.Fighters.FighterRatings", "FighterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fighter");
                });

            modelBuilder.Entity("MMASimulation.Shared.Models.Fighters.FighterStrategies", b =>
                {
                    b.HasOne("MMASimulation.Shared.Models.Fighters.Fighter", null)
                        .WithOne("FighterStrategies")
                        .HasForeignKey("MMASimulation.Shared.Models.Fighters.FighterStrategies", "FighterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MMASimulation.Shared.Models.Fighters.FighterStyles", b =>
                {
                    b.HasOne("MMASimulation.Shared.Models.Fighters.Fighter", null)
                        .WithOne("FighterStyles")
                        .HasForeignKey("MMASimulation.Shared.Models.Fighters.FighterStyles", "FighterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MMASimulation.Shared.Models.Fights.Fight", b =>
                {
                    b.HasOne("MMASimulation.Shared.Models.Fighters.Fighter", "Fighter1")
                        .WithMany()
                        .HasForeignKey("Fighter1Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MMASimulation.Shared.Models.Fighters.Fighter", "Fighter2")
                        .WithMany()
                        .HasForeignKey("Fighter2Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fighter1");

                    b.Navigation("Fighter2");
                });

            modelBuilder.Entity("MMASimulation.Shared.Models.Fights.FightPBP", b =>
                {
                    b.HasOne("MMASimulation.Shared.Models.Fights.Fight", "Fight")
                        .WithMany("Pbp")
                        .HasForeignKey("FightId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Fight");
                });

            modelBuilder.Entity("MMASimulation.Shared.Models.Fighters.Fighter", b =>
                {
                    b.Navigation("FighterRatings")
                        .IsRequired();

                    b.Navigation("FighterStrategies")
                        .IsRequired();

                    b.Navigation("FighterStyles")
                        .IsRequired();
                });

            modelBuilder.Entity("MMASimulation.Shared.Models.Fights.Fight", b =>
                {
                    b.Navigation("Pbp");
                });
#pragma warning restore 612, 618
        }
    }
}
