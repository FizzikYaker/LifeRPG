namespace LifeRPG.Models; // Пространство имен

public class RewardItem // Шаблон для покупки в магазине
{
    public Guid Id { get; set; } = Guid.NewGuid();
    // Уникальный ID награды

    public string Title { get; set; } = string.Empty;
    // Название (например, "Поиграть в видеоигры")

    public int PriceGold { get; set; }
    // Стоимость в золоте
}