namespace PersonalGrowthTracker.Api.Domain.Entities
{
    public class MoodEntry
    {
        public int Id { get; set;  }
        public DateTime CreatedAtUTC { get; set;  }

        public int Mood { get; set;  }
        public string? Note { get; set; }

    }
}
