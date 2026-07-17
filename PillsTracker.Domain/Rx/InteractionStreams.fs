namespace PillsTracker.Domain

//open System
//open System.Reactive.Linq

/// <summary>
/// Реактивные правила взаимодействий между препаратами.
/// Используют два потока событий курсов для анализа влияния.
/// </summary>
//module InteractionRx =

//    /// <summary>
//    /// Создаёт реактивное правило взаимодействия между двумя курсами.
//    /// Логика:
//    /// - слушает события PlannedDose и TakenDose двух курсов
//    /// - проверяет правило взаимодействия (InteractionRule)
//    /// - при нарушении вызывает callback (например, логирование или предупреждение)
//    /// </summary>
//    /// <param name="rule">Правило взаимодействия между группами препаратов.</param>
//    /// <param name="streamA">Поток событий курса A.</param>
//    /// <param name="streamB">Поток событий курса B.</param>
//    /// <param name="onConflict">Функция обратного вызова при конфликте.</param>
//    let bindInteractionRule
//        (rule: InteractionRule)
//        (streamA: CourseStream)
//        (streamB: CourseStream)
//        (onConflict: CourseDose * CourseDose -> unit) =

//        Observable.combineLatest(streamA.Events, streamB.Events)
//        |> Observable.subscribe (fun (evA, evB) ->

//            match evA, evB with
//            | PlannedDose doseA, TakenDose doseB
//            | TakenDose doseA, PlannedDose doseB ->

//                let aId = doseA.PreparationId
//                let bId = doseB.PreparationId

//                if InteractionLogic.affects rule aId bId then
//                    match rule.Severity with
//                    | InteractionSeverity.Critical ->
//                        onConflict(doseA, doseB)

//                    | InteractionSeverity.RequiresSpacing ->
//                        let plannedA =
//                            doseA.PlannedDate.Add(doseA.PlannedTime)

//                        let takenB = doseB.ActualIntakeTime |> Option.defaultValue doseB.PlannedDate

//                        if (plannedA - takenB).TotalMinutes < 120.0 then
//                            onConflict(doseA, doseB)

//                    | InteractionSeverity.Strong
//                    | InteractionSeverity.Mild ->
//                        () // можно логировать или показывать предупреждение

//            | _ -> ()
//        )
