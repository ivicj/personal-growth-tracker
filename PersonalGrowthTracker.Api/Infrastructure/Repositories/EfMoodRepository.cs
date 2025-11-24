using Microsoft.EntityFrameworkCore;
using PersonalGrowthTracker.Api.Domain.Entities;
using PersonalGrowthTracker.Api.Domain.Repositories;
using PersonalGrowthTracker.Api.Infrastructure.Data;

namespace PersonalGrowthTracker.Api.Infrastructure.Repositories;

public class EfMoodRepository : IMoodRepository
{
    private readonly AppDbContext _db;

    public EfMoodRepository(AppDbContext db)
    {
        _db = db;
    }

    public IEnumerable<MoodEntry> GetAll()
    {
        return _db.MoodEntries
            .OrderByDescending(x => x.CreatedAtUTC)
            .AsNoTracking()
            .ToList();
    }

    public MoodEntry? GetById(int id)
    {
        return _db.MoodEntries
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == id);
    }

    public MoodEntry Add(MoodEntry entry)
    {
        entry.CreatedAtUTC = DateTime.UtcNow;
        _db.MoodEntries.Add(entry);
        _db.SaveChanges();
        return entry;
    }
}
