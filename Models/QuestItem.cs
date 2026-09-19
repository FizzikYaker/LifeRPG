namespace LifeRPG.Models; // Пространство имен

public class QuestItem // Класс задачи
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

    /// <summary>
    /// Метод формирует интервал времени в формате "08:00 - 16:00"
    /// </summary>
    public string GetFormattedTimeRange()
    {
        // Если время начала не задано, возвращаем пустую строку
        if (!ScheduledTime.HasValue) return string.Empty;

        var startTime = ScheduledTime.Value; // Берем время начала
        var endTime = startTime.Add(TimeSpan.FromMinutes(DurationMinutes)); // Прибавляем минуты для получения времени окончания

        // Возвращаем отформатированную строку "08:00 - 16:00"
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