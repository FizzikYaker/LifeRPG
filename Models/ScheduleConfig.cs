namespace LifeRPG.Models;

public enum SchedulePatternType
{
    Shift2_2_2_2,
    Standard5_2,
    Flexible
}

public class ScheduleConfig
{
    public SchedulePatternType PatternType { get; set; } = SchedulePatternType.Shift2_2_2_2;
    public DateTime AnchorDate { get; set; } = new DateTime(2026, 9, 1);
    public TimeSpan DayShiftWakeTime { get; set; } = new TimeSpan(6, 20, 0);
    public TimeSpan NightShiftWakeTime { get; set; } = new TimeSpan(18, 10, 0);
    public int ShiftDurationHours { get; set; } = 12;
    public int CommuteHours { get; set; } = 2;
    public int MaxWorkloadPercent { get; set; } = 80;
}