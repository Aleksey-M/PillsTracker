namespace PillsTracker.Domain;

/// <summary>
/// Отношение приёма препарата к приёму пищи.
/// Используется как часть схемы приёма.
/// </summary>
public enum MealRelation
{
    BeforeMeal,
    WithMeal,
    AfterMeal
}

/// <summary>
/// Справочная схема приёма препарата.
/// Описывает "идеальный" или рекомендованный режим,
/// который может быть изменён в реальном курсе.
/// </summary>
public class Regimen
{
    /// <summary>
    /// Уникальный идентификатор схемы приёма.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Количество приёмов препарата в день по инструкции.
    /// </summary>
    public int IntakesPerDay { get; set; }

    /// <summary>
    /// Рекомендованная дозировка, например "1–3 таблетки".
    /// Это текстовое поле, т.к. инструкции бывают сложными.
    /// </summary>
    public string DoseAmount { get; set; } = null!;

    /// <summary>
    /// Рекомендация по приёму относительно еды.
    /// </summary>
    public MealRelation MealRelation { get; set; }

    /// <summary>
    /// Название схемы (например "Стандартная", "Усиленная").
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Дополнительное описание схемы.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Дата последнего редактирования схемы.
    /// </summary>
    public DateTime LastEditedAt { get; set; }
}
