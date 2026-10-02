using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SharedModelsLib;

public partial class ColvalteacherSportsdbContext : DbContext
{
    public ColvalteacherSportsdbContext()
    {
    }

    public ColvalteacherSportsdbContext(DbContextOptions<ColvalteacherSportsdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Sport> Sports { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseMySQL("Name=ConnectionStrings:OnlineMySqlDB");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Player");

            entity.HasIndex(e => e.TeamId, "FK_Player_Team");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Age).HasColumnType("int(11)");
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.TeamId).HasColumnType("int(11)");

            entity.HasOne(d => d.Team).WithMany(p => p.Players)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Player_Team");
        });

        modelBuilder.Entity<Sport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Sport");

            entity.HasIndex(e => e.Name, "UQ_Sport_Name").IsUnique();

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PlayersPerTeam).HasColumnType("int(11)");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Team");

            entity.HasIndex(e => e.SportId, "FK_Team_Sport");

            entity.HasIndex(e => new { e.Name, e.SportId }, "UQ_Team_Name_Sport").IsUnique();

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.SportId).HasColumnType("int(11)");

            entity.HasOne(d => d.Sport).WithMany(p => p.Teams)
                .HasForeignKey(d => d.SportId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Team_Sport");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
