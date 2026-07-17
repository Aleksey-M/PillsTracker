namespace PillsTracker.Domain;

/// <summary>
/// Конкретный приём препарата в рамках курса.
/// Используется для генерации уведомлений и отслеживания фактического приёма.
/// </summary>
public class CourseDose
{
    /// <summary>
    /// Уникальный идентификатор приёма.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор курса, к которому относится приём.
    /// </summary>
    public Guid CourseId { get; set; }

    /// <summary>
    /// Идентификатор препарата (дублируется для удобства запросов).
    /// </summary>
    public Guid PreparationId { get; set; }

    /// <summary>
    /// Идентификатор фактической схемы, по которой рассчитан приём.
    /// </summary>
    public Guid CourseRegimenId { get; set; }

    /// <summary>
    /// Дата запланированного приёма.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Время запланированного приёма.
    /// </summary>
    public TimeSpan Time { get; set; }

    /// <summary>
    /// За сколько минут до приёма нужно отправить уведомление.
    /// </summary>
    public int NotifyBeforeMinutes { get; set; }

    /// <summary>
    /// Фактическое время приёма.
    /// Null, если пользователь ещё не отметил приём.
    /// </summary>
    public DateTime? ActualIntakeTime { get; set; }
}
