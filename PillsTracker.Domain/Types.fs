// ------------------------------------------------------------
// Domain/Types.fs
// ------------------------------------------------------------
// Этот файл содержит чистые доменные типы.
// Здесь нет побочных эффектов, зависимостей, логирования,
// инфраструктуры или ссылок на внешние системы.
// Только данные, которые описывают бизнес-модель.
// ------------------------------------------------------------

namespace PillsTracker.Domain

open System

/// <summary>
/// Уникальный идентификатор курса.
/// Используется для строгой типизации и предотвращения ошибок.
/// </summary>
type CourseId = CourseId of Guid

/// <summary>
/// Уникальный идентификатор препарата.
/// </summary>
type PreparationId = PreparationId of Guid

/// <summary>
/// Уникальный идентификатор справочной схемы.
/// </summary>
type RegimenId = RegimenId of Guid

/// <summary>
/// Уникальный идентификатор конкретного приёма препарата.
/// </summary>
type CourseDoseId = CourseDoseId of Guid

/// <summary>
/// Уникальный идентификатор фактической схемы курса.
/// </summary>
type CourseRegimenId = CourseRegimenId of Guid

/// <summary>
/// Отношение приёма препарата к приёму пищи.
/// Используется как часть схемы приёма.
/// </summary>
type MealRelation =
    /// <summary>Принимать до еды.</summary>
    | BeforeMeal
    /// <summary>Принимать вместе с едой.</summary>
    | WithMeal
    /// <summary>Принимать после еды.</summary>
    | AfterMeal

/// <summary>
/// Фактический курс приёма препарата.
/// Содержит реальные даты начала/окончания и плановую длительность.
/// </summary>
type Course = {
    /// <summary>Уникальный идентификатор курса.</summary>
    Id: CourseId

    /// <summary>Идентификатор препарата, который принимается в рамках курса.</summary>
    PreparationId: PreparationId

    /// <summary>
    /// Плановая длительность курса в днях.
    /// Может отличаться от фактической.
    /// </summary>
    PlannedDurationDays: int

    /// <summary>Реальная дата начала курса.</summary>
    StartDate: DateTime

    /// <summary>
    /// Реальная дата окончания курса.
    /// Может быть раньше (если бросили) или позже (если продлили).
    /// </summary>
    EndDate: DateTime option
}

/// <summary>
/// Фактическая схема приёма в рамках конкретного курса.
/// Может отличаться от справочной Regimen.
/// </summary>
type CourseRegimen = {
    /// <summary>Уникальный идентификатор фактической схемы.</summary>
    Id: CourseRegimenId

    /// <summary>Идентификатор курса, к которому относится схема.</summary>
    CourseId: CourseId

    /// <summary>Идентификатор базовой схемы, на основе которой создана фактическая.</summary>
    BaseRegimenId: RegimenId

    /// <summary>Фактическое количество приёмов в день.</summary>
    IntakesPerDay: int

    /// <summary>Фактическая дозировка, например "2 таблетки".</summary>
    DoseAmount: string

    /// <summary>Фактическое отношение приёма к еде.</summary>
    MealRelation: MealRelation

    /// <summary>Дата, когда эта схема была применена.</summary>
    AppliedAt: DateTime
}

/// <summary>
/// Конкретный приём препарата в рамках курса.
/// Используется для генерации уведомлений и отслеживания фактического приёма.
/// </summary>
type CourseDose = {
    /// <summary>Уникальный идентификатор приёма.</summary>
    Id: CourseDoseId

    /// <summary>Идентификатор курса, к которому относится приём.</summary>
    CourseId: CourseId

    /// <summary>Идентификатор препарата (дублируется для удобства запросов).</summary>
    PreparationId: PreparationId

    /// <summary>Идентификатор фактической схемы, по которой рассчитан приём.</summary>
    CourseRegimenId: CourseRegimenId

    /// <summary>Дата запланированного приёма.</summary>
    PlannedDate: DateTime

    /// <summary>Время запланированного приёма.</summary>
    PlannedTime: TimeSpan

    /// <summary>За сколько минут до приёма нужно отправить уведомление.</summary>
    NotifyBeforeMinutes: int

    /// <summary>
    /// Фактическое время приёма.
    /// Null, если пользователь ещё не отметил приём.
    /// </summary>
    ActualIntakeTime: DateTime option
}

/// <summary>
/// Справочная сущность, описывающая препарат как объект домена.
/// Не содержит данных о конкретных курсах или дозировках.
/// </summary>
type Preparation = {
    /// <summary>Уникальный идентификатор препарата.</summary>
    Id: PreparationId

    /// <summary>Название препарата.</summary>
    Name: string

    /// <summary>Описание препарата, его свойства, механизм действия и т.п.</summary>
    Description: string option

    /// <summary>Дата создания записи (для аудита и сортировки).</summary>
    CreatedAt: DateTime

    /// <summary>
    /// Условная сила препарата от 1 до 10.
    /// Используется для рекомендаций и визуализации.
    /// </summary>
    Strength: int
}

/// <summary>
/// Справочная схема приёма препарата.
/// Описывает "идеальный" или рекомендованный режим.
/// </summary>
type Regimen = {
    /// <summary>Уникальный идентификатор схемы приёма.</summary>
    Id: RegimenId

    /// <summary>Количество приёмов препарата в день по инструкции.</summary>
    IntakesPerDay: int

    /// <summary>Рекомендованная дозировка, например "1–3 таблетки".</summary>
    DoseAmount: string

    /// <summary>Рекомендация по приёму относительно еды.</summary>
    MealRelation: MealRelation

    /// <summary>Название схемы (например "Стандартная").</summary>
    Name: string

    /// <summary>Дополнительное описание схемы.</summary>
    Description: string option

    /// <summary>Дата последнего редактирования схемы.</summary>
    LastEditedAt: DateTime
}

/// <summary>
/// Направление влияния между группами препаратов.
/// Определяет характер и направление взаимодействия.
/// </summary>
type InteractionDirection =
    /// <summary>A усиливает B и B усиливает A.</summary>
    | MutualPositive
    /// <summary>A ухудшает B и B ухудшает A.</summary>
    | MutualNegative
    /// <summary>A усиливает действие B.</summary>
    | AffectsB_Positive
    /// <summary>A ухудшает действие B.</summary>
    | AffectsB_Negative
    /// <summary>B усиливает действие A.</summary>
    | BffectsA_Positive
    /// <summary>B ухудшает действие A.</summary>
    | BffectsA_Negative

/// <summary>
/// Степень важности влияния между препаратами.
/// Используется для принятия решений о совместимости.
/// </summary>
type InteractionSeverity =
    /// <summary>Слабое влияние.</summary>
    | Mild
    /// <summary>Требует промежутка между приёмами.</summary>
    | RequiresSpacing
    /// <summary>Сильное влияние.</summary>
    | Strong
    /// <summary>Критично несовместимы.</summary>
    | Critical

/// <summary>
/// Правило взаимодействия между двумя группами препаратов.
/// Содержит описание, направление и важность влияния.
/// </summary>
type InteractionRule = {
    /// <summary>Список препаратов группы A.</summary>
    GroupA: PreparationId list

    /// <summary>Список препаратов группы B.</summary>
    GroupB: PreparationId list

    /// <summary>
    /// Текстовое описание влияния.
    /// Например: "Препараты группы A ухудшают всасываемость препаратов группы B".
    /// </summary>
    Description: string

    /// <summary>Направление влияния между группами.</summary>
    Direction: InteractionDirection

    /// <summary>Степень важности взаимодействия.</summary>
    Severity: InteractionSeverity
}
