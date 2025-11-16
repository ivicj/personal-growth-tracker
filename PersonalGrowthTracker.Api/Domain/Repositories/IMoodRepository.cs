using PersonalGrowthTracker.Api.Domain.Entities;

namespace PersonalGrowthTracker.Api.Domain.Repositories
{
    public interface IMoodRepository
    {
        IEnumerable<MoodEntry> GetAll();
        MoodEntry? GetById(int id);
        MoodEntry Add(MoodEntry entry);
    }
}
