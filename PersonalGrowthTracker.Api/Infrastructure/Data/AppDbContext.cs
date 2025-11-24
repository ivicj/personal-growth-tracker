using Microsoft.EntityFrameworkCore;
using PersonalGrowthTracker.Api.Domain.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace PersonalGrowthTracker.Api.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<MoodEntry> MoodEntries => Set<MoodEntry>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MoodEntry>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Mood).IsRequired();
            entity.Property(e => e.CreatedAtUTC)
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
    }
}
