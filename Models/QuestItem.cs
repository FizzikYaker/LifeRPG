namespace LifeRPG.Models; // Пространство имен

public class QuestItem // Класс задачи
{
    public Guid Id { get; set; } = Guid.NewGuid();
    // Уникальный ID задачи

    public string Title { get; set; } = string.Empty;
    // Название квеста ("Учеба 1 час")

    public int RewardGold { get; set; }
    // Базовая награда в золоте

    public QuestType Type { get; set; }
    // Тип квеста (Study, WorkShift, Daily и т.д.)

    public bool IsCompleted { get; set; }
    // Флаг: выполнена ли задача (true = да)

    public bool IsSkipped { get; set; }
    // Новый bool-флаг: пропущена ли задача официально ("Сегодня учебы нет")

    public DateTime? LastCompletedAt { get; set; }
    // Дата последнего выполнения

    public int StreakDays { get; set; } = 0;
    // Стрик дней

    public bool IsNightShift { get; set; }
    // Флаг ночной смены

    /// <summary>
    /// Метод для нажатия кнопки "Сегодня учебы нет"
    /// </summary>
    public void SkipTask() // void — метод просто изменяет состояние объекта
    {
        IsSkipped = true; // Помечаем, что задача официально отменена на сегодня
        // При этом IsCompleted остается false, золота 0, но стрик НЕ ломается!
    }

    public int GetCalculatedReward()
    {
        if (IsSkipped) return 0; // Если задача была отменена кнопкой — награда 0

        if (Type == QuestType.WorkShift && IsNightShift)
        {
            return RewardGold + 200; // Бонус за ночную смену
        }

        if (Type == QuestType.Daily && StreakDays >= 7)
        {
            return (int)(RewardGold * 1.5); // Бонус +50% за стрик 7+ дней [cite: 8]
        }

        return RewardGold;
    }
}