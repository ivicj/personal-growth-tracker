using PersonalGrowthTracker.Api.Domain.Entities;

namespace PersonalGrowthTracker.Api.Domain.Repositories
{
    public class InMemoryMoodRepository : IMoodRepository
    {
        private readonly List<MoodEntry> _entries = [];
        private int _nextId = 1;

        public MoodEntry Add(MoodEntry entry)
        {
            entry.Id = _nextId++;
            entry.CreatedAtUTC = DateTime.UtcNow;
            _entries.Add(entry);
            return entry;
        }

        public IEnumerable<MoodEntry> GetAll()
            => _entries.OrderByDescending(e => e.CreatedAtUTC);

        public MoodEntry? GetById(int id)
            => _entries.FirstOrDefault(e => e.Id == id);
    }
}
