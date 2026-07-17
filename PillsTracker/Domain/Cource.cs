namespace PillsTracker.Domain;

/// <summary>
/// Фактический курс приёма препарата.
/// Содержит реальные даты начала/окончания и плановую длительность.
/// </summary>
public class Course
{
    /// <summary>
    /// Уникальный идентификатор курса.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор препарата, который принимается в рамках курса.
    /// </summary>
    public Guid PreparationId { get; set; }

    /// <summary>
    /// Плановая длительность курса в днях.
    /// Может отличаться от фактической.
    /// </summary>
    public int PlannedDurationDays { get; set; }

    /// <summary>
    /// Реальная дата начала курса.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Реальная дата окончания курса.
    /// Может быть раньше (если бросили) или позже (если продлили).
    /// </summary>
    public DateTime? EndDate { get; set; }
}

