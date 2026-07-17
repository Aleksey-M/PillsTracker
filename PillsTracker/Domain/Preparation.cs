namespace PillsTracker.Domain;

/// <summary>
/// Справочная сущность, описывающая препарат как объект домена.
/// Не содержит данных о конкретных курсах или дозировках.
/// Используется как основа для всех фактических курсов.
/// </summary>
public class Preparation
{
    /// <summary>
    /// Уникальный идентификатор препарата.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название препарата (торговое или действующее вещество).
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Описание препарата, его свойства, механизм действия и т.п.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Дата создания записи (для аудита и сортировки).
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Условная сила препарата от 1 до 10.
    /// Используется для рекомендаций и визуализации.
    /// </summary>
    public int Strength { get; set; }
}
