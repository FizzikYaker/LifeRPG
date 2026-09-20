namespace LifeRPG.Models;

public class QuestItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public int RewardGold { get; set; }
    public QuestType Type { get; set; }
    public bool IsCompleted { get; set; }
    public bool IsSkipped { get; set; }
    public int StreakDays { get; set; } = 0;
    public bool IsNightShift { get; set; }
    public TimeSpan? ScheduledTime { get; set; }
    public int DurationMinutes { get; set; } = 60;

    public DateTime TargetDate { get; set; } = DateTime.Today;

    public string GetFormattedTimeRange()
    {
        if (!ScheduledTime.HasValue) return string.Empty;
        var startTime = ScheduledTime.Value;
        var endTime = startTime.Add(TimeSpan.FromMinutes(DurationMinutes));
        return $"{startTime:hh\\:mm} - {endTime:hh\\:mm}";
    }

    public void SkipTask()
    {
        IsSkipped = true;
    }

    public int GetCalculatedReward()
    {
        if (IsSkipped) return 0;
        if (Type == QuestType.WorkShift && IsNightShift) return RewardGold + 200;
        if (Type == QuestType.Daily && StreakDays >= 7) return (int)(RewardGold * 1.5);
        return RewardGold;
    }
}