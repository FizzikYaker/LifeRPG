namespace LifeRPG.Models;

public enum QuestPriority
{
    Low, Medium, High, Critical
}

public enum RecurrenceRule
{
    None,
    Daily,
    WorkDaysOnly,
    OffDaysOnly,
    SpecificDays   // N раз в неделю, гибко
}

public class QuestItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public int RewardGold { get; set; } = 50;

    public QuestType Type { get; set; } = QuestType.OneTime;      // <-- было QuestPriority, теперь QuestType
    public QuestPriority Priority { get; set; } = QuestPriority.Medium;
    public RecurrenceRule Recurrence { get; set; } = RecurrenceRule.None;
    public int TimesPerWeek { get; set; } = 3;                    // <-- новое поле для SpecificDays

    public bool IsCompleted { get; set; }
    public bool IsSkipped { get; set; }
    public int StreakDays { get; set; } = 0;
    public bool IsNightShift { get; set; }
    public TimeSpan? ScheduledTime { get; set; }
    public int DurationMinutes { get; set; } = 60;
    public DateTime TargetDate { get; set; } = DateTime.Today;
    public QuestLoad Load { get; set; } = QuestLoad.Light;

    public string GetFormattedTimeRange()
    {
        if (!ScheduledTime.HasValue) return string.Empty;
        var start = ScheduledTime.Value;
        var end = start.Add(TimeSpan.FromMinutes(DurationMinutes));
        return $"{start:hh\\:mm} - {end:hh\\:mm}";
    }

    public void SkipTask() => IsSkipped = true;

    public int GetCalculatedReward()
    {
        if (IsSkipped) return 0;
        if (Type == QuestType.WorkShift && IsNightShift) return RewardGold + 200;
        if (Type == QuestType.Daily && StreakDays >= 7) return (int)(RewardGold * 1.5);
        return RewardGold;
    }
}