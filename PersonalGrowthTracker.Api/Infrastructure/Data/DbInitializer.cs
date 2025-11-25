using PersonalGrowthTracker.Api.Domain.Entities;

namespace PersonalGrowthTracker.Api.Infrastructure.Data;

public static class DbInitializer
{
    public static void Seed(AppDbContext db)
    {
        if (db.MoodEntries.Any())
        {
            return;
        }

        var now = DateTime.UtcNow;

        var seedEntries = new List<MoodEntry>
        {
            new MoodEntry
            {
                Mood = 4,
                Note = "Bad sleep, hard to focus, but I’ve started working.",
                CreatedAtUTC = now.AddDays(-5)
            },
            new MoodEntry
            {
                Mood = 7,
                Note = "Good focus while working, finished an important task.",
                CreatedAtUTC = now.AddDays(-3)
            },
            new MoodEntry
            {
                Mood = 6,
                Note = "An okay day, a little tired, but I’m keeping up with my habits.",
                CreatedAtUTC = now.AddDays(-1)
            },
            new MoodEntry
            {
                Mood = 8,
                Note = "Satisfied with the progress, the app is almost ready to be shown.",
                CreatedAtUTC = now
            }
        };

        db.MoodEntries.AddRange(seedEntries);
        db.SaveChanges();
    }
}
