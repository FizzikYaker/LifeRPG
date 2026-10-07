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
            SchedulePatternType.Shift => Get2222(daysDiff),
            SchedulePatternType.Standard => GetStandard52(date),
            SchedulePatternType.Flexible => GetFlexible(daysDiff),
            _ => ShiftDayType.Off
        };
    }

    private ShiftDayType Get2222(int daysDiff)
    {
        // Берём длины блоков из настроек. Math.Max(1, ...) защищает от нуля:
        // если поле в панели очистить, получится 0, и деление на 0 уронило бы приложение.
        int work = Math.Max(1, _config.WorkDaysInCycle);
        int off = Math.Max(1, _config.OffDaysInCycle);

        // --- Режим БЕЗ деления на день/ночь (галочка выключена) ---
        if (!_config.DayOrNight)
        {
            int cycleLength = work + off; // например 2 + 2 = 4 дня в цикле

            // (x % n + n) % n работает и для дат ДО точки отсчёта (отрицательный daysDiff)
            int cycle = (daysDiff % cycleLength + cycleLength) % cycleLength;

            // Первые 'work' дней цикла рабочие, остальные выходные
            return cycle < work ? ShiftDayType.Work : ShiftDayType.Off;
        }

        // --- Режим С делением на день/ночь (галочка включена) ---
        // Одна "половина" = рабочий блок + выходной блок (например 2 + 2 = 4 дня).
        int half = work + off;

        // Полный цикл = дневной блок и ночной блок (4 + 4 = 8 дней).
        int fullCycle = half * 2;

        // Позиция текущего дня внутри полного цикла: от 0 до fullCycle - 1
        int pos = (daysDiff % fullCycle + fullCycle) % fullCycle;

        if (pos < work) return ShiftDayType.Day;         // 1-й блок: дневные смены
        if (pos < half) return ShiftDayType.Off;         // выходные после дневных
        if (pos < half + work) return ShiftDayType.Night; // 2-й блок: ночные смены
        return ShiftDayType.Off;                          // выходные после ночных
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