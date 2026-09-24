using LifeRPG.Models;

namespace LifeRPG.Services;

public enum ShiftDayType { Day, Night, Off, Work } // Work — для 5/2 и Flexible, где нет деления день/ночь

public class ShiftScheduleService
{
    private readonly ScheduleConfig _config;

    public ShiftScheduleService(ScheduleConfig config)
    {
        _config = config;
    }

    public ShiftDayType GetShiftType(DateTime date)
    {
        int daysDiff = (date.Date - _config.AnchorDate.Date).Days;

        return _config.PatternType switch
        {
            SchedulePatternType.Shift2_2_2_2 => Get2222(daysDiff),
            SchedulePatternType.Standard5_2 => GetStandard52(date),
            SchedulePatternType.Flexible => GetFlexible(daysDiff),
            _ => ShiftDayType.Off
        };
    }

    private ShiftDayType Get2222(int daysDiff)
    {
        int cycle = (daysDiff % 8 + 8) % 8;
        return cycle switch
        {
            0 or 1 => ShiftDayType.Day,
            2 or 3 => ShiftDayType.Off,
            4 or 5 => ShiftDayType.Night,
            _ => ShiftDayType.Off
        };
    }

    private ShiftDayType GetStandard52(DateTime date)
    {
        var dow = date.DayOfWeek;
        return (dow == DayOfWeek.Saturday || dow == DayOfWeek.Sunday)
            ? ShiftDayType.Off
            : ShiftDayType.Work;
    }

    // "N рабочих через M выходных", например 1/4
    private ShiftDayType GetFlexible(int daysDiff)
    {
        int cycleLength = _config.WorkDaysInCycle + _config.OffDaysInCycle;
        int cycle = (daysDiff % cycleLength + cycleLength) % cycleLength;
        return cycle < _config.WorkDaysInCycle ? ShiftDayType.Work : ShiftDayType.Off;
    }

    public bool IsNightShift(DateTime date) => GetShiftType(date) == ShiftDayType.Night;
    public bool IsWorkDay(DateTime date)
    {
        var t = GetShiftType(date);
        return t == ShiftDayType.Day || t == ShiftDayType.Night || t == ShiftDayType.Work;
    }
}