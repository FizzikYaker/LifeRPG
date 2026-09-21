namespace LifeRPG.Models;

public enum SchedulePatternType
{
    Shift2_2_2_2, // 2 день / 2 отдых / 2 ночь / 2 отдых
    Standard5_2,  // 5 рабочих / 2 выходных
    Flexible      // Гибкий / Кастомный
}

public class ScheduleConfig
{
    public SchedulePatternType PatternType { get; set; } = SchedulePatternType.Shift2_2_2_2;
    public DateTime AnchorDate { get; set; } = new DateTime(2026, 9, 1); // Точка отсчета первой дневной смены

    // Время подъема
    public TimeSpan DayShiftWakeTime { get; set; } = new TimeSpan(6, 20, 0);  // 06:20
    public TimeSpan NightShiftWakeTime { get; set; } = new TimeSpan(18, 10, 0); // 18:10

    // Время работы/учебы и дорога (в часах)
    public int ShiftDurationHours { get; set; } = 12;
    public int CommuteHours { get; set; } = 2;

    // Максимальная загрузка дня делами (например, не более 80% свободного времени)
    public int MaxWorkloadPercent { get; set; } = 80;
}