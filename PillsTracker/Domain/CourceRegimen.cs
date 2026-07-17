namespace PillsTracker.Domain;

/// <summary>
/// Фактическая схема приёма в рамках конкретного курса.
/// Может отличаться от справочной Regimen, если пользователь
/// изменил дозировку или частоту приёма.
/// </summary>
public class CourseRegimen
{
    /// <summary>
    /// Уникальный идентификатор фактической схемы.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор курса, к которому относится схема.
    /// </summary>
    public Guid CourseId { get; set; }

    /// <summary>
    /// Идентификатор базовой схемы, на основе которой создана фактическая.
    /// </summary>
    public Guid RegimenId { get; set; }

    /// <summary>
    /// Фактическое количество приёмов в день.
    /// Может быть увеличено или уменьшено пользователем.
    /// </summary>
    public int IntakesPerDay { get; set; }

    /// <summary>
    /// Фактическая дозировка, например "2 таблетки".
    /// </summary>
    public string DoseAmount { get; set; } = null!;

    /// <summary>
    /// Фактическое отношение приёма к еде.
    /// </summary>
    public MealRelation MealRelation { get; set; }

    /// <summary>
    /// Дата, когда эта схема была применена.
    /// Позволяет хранить историю изменений.
    /// </summary>
    public DateTime AppliedAt { get; set; }
}
