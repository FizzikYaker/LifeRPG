namespace LifeRPG.Models; // Пространство имен

public class PlayerProfile // Шаблон профиля
{
    public string Name { get; set; } = "Игрок";
    // Имя персонажа

    public int Level { get; set; } = 1;
    // Текущий уровень

    public int CurrentXP { get; set; } = 0;
    // Текущий опыт на уровне

    public int GoldBalance { get; set; } = 0;
    // Баланс золота

    public int RequiredXP => Level * 1000;
    // '=>' — вычисляемое свойство: формула требуемого опыта (Уровень * 1000)

    public void AddGoldAndXP(int gold) // void — метод выполняет действие и ничего не возвращает
    {
        GoldBalance += gold; // '+=' добавляет золото к балансу
        CurrentXP += gold;   // 1 gold = 1 XP [cite: 9]
        CheckLevelUp();      // Вызываем проверку повышения уровня
    }

    public bool TrySpendGold(int price) // Возвращает true/false
    {
        if (GoldBalance >= price) // Если денег хватает
        {
            GoldBalance -= price; // Списываем сумму
            return true;          // Покупка успешна
        }
        return false;             // Денег не хватило
    }

    private void CheckLevelUp() // private — закрытый метод, вызывается только внутри этого класса
    {
        while (CurrentXP >= RequiredXP) // while — цикл пока опыта хватает на новый уровень
        {
            CurrentXP -= RequiredXP; // Вычитаем порог уровня
            Level++;                 // '++' увеличивает уровень на 1
        }
    }
}