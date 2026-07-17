// ------------------------------------------------------------
// Domain/Ports.fs
// ------------------------------------------------------------
// Порты — это интерфейсы (в функциональном стиле — типы функций),
// через которые домен взаимодействует с внешним миром.
// Домен знает только о портах, но не знает об их реализациях.
// Реализации находятся в слое Infrastructure.
// ------------------------------------------------------------

namespace PillsTracker.Domain

open System

/// <summary>
/// Порт для работы с курсами приёма препарата.
/// Определяет минимальный набор операций, необходимых домену.
/// Реализация порта находится во внешнем слое (адаптеры).
/// </summary>
type ICourseRepository =
    /// <summary>
    /// Возвращает курс по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор курса.</param>
    /// <returns>Курс или None, если курс не найден.</returns>
    abstract member Get : CourseId -> Course option

    /// <summary>
    /// Сохраняет или обновляет курс.
    /// </summary>
    /// <param name="course">Доменная сущность курса.</param>
    abstract member Save : Course -> unit


/// <summary>
/// Порт для работы с приёмами препарата (дозами) в рамках курса.
/// Используется доменом для получения и сохранения данных о дозах.
/// </summary>
type ICourseDoseRepository =
    /// <summary>
    /// Возвращает список всех доз, относящихся к указанному курсу.
    /// </summary>
    /// <param name="courseId">Идентификатор курса.</param>
    /// <returns>Список доменных сущностей доз.</returns>
    abstract member GetByCourse : CourseId -> CourseDose list

    /// <summary>
    /// Сохраняет или обновляет конкретный приём препарата.
    /// </summary>
    /// <param name="dose">Доменная сущность приёма.</param>
    abstract member Save : CourseDose -> unit


/// <summary>
/// Порт для работы с фактическими схемами курса.
/// Позволяет домену получать и сохранять актуальные схемы приёма.
/// </summary>
type ICourseRegimenRepository =
    /// <summary>
    /// Возвращает фактическую схему курса.
    /// </summary>
    /// <param name="courseId">Идентификатор курса.</param>
    /// <returns>Фактическая схема или None, если схема не найдена.</returns>
    abstract member GetByCourse : CourseId -> CourseRegimen option

    /// <summary>
    /// Сохраняет или обновляет фактическую схему курса.
    /// </summary>
    /// <param name="regimen">Доменная сущность схемы.</param>
    abstract member Save : CourseRegimen -> unit


/// <summary>
/// Порт для работы со справочными препаратами.
/// Используется доменом для получения информации о препаратах.
/// </summary>
type IPreparationRepository =
    /// <summary>
    /// Возвращает препарат по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор препарата.</param>
    /// <returns>Препарат или None, если не найден.</returns>
    abstract member Get : PreparationId -> Preparation option


/// <summary>
/// Порт для отправки уведомлений пользователю.
/// Домен вызывает этот порт, когда необходимо отправить напоминание.
/// </summary>
type INotificationService =
    /// <summary>
    /// Отправляет уведомление о приближении приёма препарата.
    /// </summary>
    /// <param name="dose">Приём препарата, для которого нужно отправить уведомление.</param>
    abstract member SendReminder : CourseDose -> unit


/// <summary>
/// Порт для получения информации о последнем приёме пищи.
/// Используется доменом при проверке MealRelation.
/// </summary>
type IMealTracker =
    /// <summary>
    /// Возвращает время последнего приёма пищи.
    /// </summary>
    /// <returns>Дата и время последнего приёма пищи.</returns>
    abstract member LastMealTime : unit -> DateTime

