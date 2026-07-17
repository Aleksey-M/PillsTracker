namespace PillsTracker.Domain

open System

/// <summary>
/// Доменные операции, связанные с конкретным приёмом препарата.
/// Содержат чистые функции, которые изменяют состояние дозы
/// путём создания новой версии записи.
/// </summary>
module DoseOperations =

    /// <summary>
    /// Отмечает дозу как принятую.
    /// Если доза уже была отмечена ранее, возвращает ошибку <see cref="DoseAlreadyTaken"/>.
    /// </summary>
    /// <param name="actualTime">Фактическое время приёма.</param>
    /// <param name="dose">Доменная сущность приёма препарата.</param>
    /// <returns>
    /// <c>Ok</c> с обновлённой дозой или <c>Error</c> с доменной ошибкой.
    /// </returns>
    let markTaken actualTime dose =
        match dose.ActualIntakeTime with
        | Some _ -> Error DoseAlreadyTaken
        | None ->
            Ok { dose with ActualIntakeTime = Some actualTime }


/// <summary>
/// Доменные операции, связанные с корректировкой расписания приёмов.
/// Используются для сдвига времени следующего приёма.
/// </summary>
module RegimenOperations =

    /// <summary>
    /// Сдвигает время следующего запланированного приёма на указанное количество минут.
    /// Используется при позднем или раннем фактическом приёме.
    /// </summary>
    /// <param name="minutes">Количество минут для сдвига.</param>
    /// <param name="dose">Доменная сущность приёма препарата.</param>
    /// <returns>
    /// Новая версия дозы с обновлённым временем приёма.
    /// </returns>
    let shiftNextDose (minutes: int) (dose: CourseDose) =
        let newTime = dose.PlannedTime + TimeSpan.FromMinutes(float minutes)
        Ok { dose with PlannedTime = newTime }


/// <summary>
/// Доменные операции, связанные с логикой взаимодействия препаратов.
/// Содержат чистые функции для анализа влияния между группами препаратов.
/// </summary>
module InteractionLogic =

    /// <summary>
    /// Проверяет, влияет ли препарат A на препарат B согласно правилу взаимодействия.
    /// Учитывает направление влияния (взаимное, одностороннее, позитивное, негативное).
    /// </summary>
    /// <param name="rule">Правило взаимодействия.</param>
    /// <param name="a">Препарат, который потенциально оказывает влияние.</param>
    /// <param name="b">Препарат, который потенциально подвергается влиянию.</param>
    /// <returns>
    /// <c>true</c>, если влияние существует согласно правилу; иначе <c>false</c>.
    /// </returns>
    let affects (rule: InteractionRule) (a: PreparationId) (b: PreparationId) =
        match rule.Direction with
        | MutualPositive
        | MutualNegative ->
            (rule.GroupA |> List.contains a && rule.GroupB |> List.contains b)
            || (rule.GroupB |> List.contains a && rule.GroupA |> List.contains b)

        | AffectsB_Positive
        | AffectsB_Negative ->
            rule.GroupA |> List.contains a
            && rule.GroupB |> List.contains b

        | BffectsA_Positive
        | BffectsA_Negative ->
            rule.GroupB |> List.contains a
            && rule.GroupA |> List.contains b

    /// <summary>
    /// Возвращает текстовое описание взаимодействия с указанием степени важности.
    /// Используется для отображения пользователю или логирования.
    /// </summary>
    /// <param name="rule">Правило взаимодействия.</param>
    /// <returns>Строка с описанием и уровнем важности.</returns>
    let explain rule =
        $"{rule.Description} (Severity: {rule.Severity})"

    /// <summary>
    /// Проверяет, является ли взаимодействие критичным.
    /// Критичное взаимодействие означает полную несовместимость препаратов.
    /// </summary>
    /// <param name="rule">Правило взаимодействия.</param>
    /// <returns><c>true</c>, если взаимодействие критичное.</returns>
    let isCritical rule =
        rule.Severity = InteractionSeverity.Critical

    /// <summary>
    /// Проверяет, требует ли взаимодействие временного промежутка между приёмами.
    /// Используется для автоматической корректировки расписания.
    /// </summary>
    /// <param name="rule">Правило взаимодействия.</param>
    /// <returns><c>true</c>, если требуется промежуток.</returns>
    let requiresSpacing rule =
        rule.Severity = InteractionSeverity.RequiresSpacing




(*
Пример создания
let rule = {
    GroupA = [ PreparationId(Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")) ]
    GroupB = [ PreparationId(Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb")) ]
    Description = "Препараты группы A ухудшают всасываемость препаратов группы B"
    Direction = InteractionDirection.AffectsB_Negative
    Severity = InteractionSeverity.RequiresSpacing
}

валидация
match InteractionValidation.validateAll rule with
| Ok validRule -> printfn "Rule OK"
| Error err -> printfn "Validation error: %A" err


проверка влияния
let a = PreparationId(Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"))
let b = PreparationId(Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"))

let doesAaffectB = InteractionLogic.affects rule a b

*)

