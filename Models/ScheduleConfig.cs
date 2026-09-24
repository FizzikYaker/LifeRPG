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
    public DateTime AnchorDate { get; set; } = DateTime.Today;

    // Используем TimeOnly? для точного совпадения с HTML input[type="time"]
    public TimeOnly? DayShiftWakeTime { get; set; } = new TimeOnly(6, 20);
    public TimeOnly? NightShiftWakeTime { get; set; } = new TimeOnly(18, 10);

    public int ShiftDurationHours { get; set; } = 12;
    public int CommuteHours { get; set; } = 2;
    public int MaxWorkloadPercent { get; set; } = 80;
}