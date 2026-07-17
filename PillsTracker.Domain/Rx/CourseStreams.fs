namespace PillsTracker.Domain

//open System
//open System.Reactive
//open System.Reactive.Linq
//open System.Reactive.Subjects

///// <summary>
///// Реактивный поток событий курса.
///// Содержит события доз (плановые, фактические, корректировки),
///// которые используются для напоминаний, анализа и взаимодействий.
///// </summary>
//type CourseEvent =
//    | PlannedDose of CourseDose
//    | TakenDose of CourseDose
//    | AdjustedDose of CourseDose

///// <summary>
///// Реактивный источник событий курса.
///// Используется доменом для генерации событий, которые затем
///// обрабатываются правилами напоминаний и взаимодействий.
///// </summary>
//type CourseStream(courseId: CourseId) =

//    /// <summary>
//    /// Внутренний Subject, принимающий события курса.
//    /// </summary>
//    let subject = new Subject<CourseEvent>()

//    /// <summary>
//    /// Публичный поток событий курса.
//    /// </summary>
//    member _.Events : IObservable<CourseEvent> = subject.AsObservable()

//    /// <summary>
//    /// Публикует новое событие курса.
//    /// </summary>
//    member _.Publish(event: CourseEvent) =
//        subject.OnNext(event)


///// <summary>
///// Реактивные операции, связанные с напоминаниями.
///// Используют поток событий курса для определения момента отправки уведомлений.
///// </summary>
//module CourseReminderRx =

//    /// <summary>
//    /// Создаёт реактивное правило напоминаний:
//    /// - слушает события PlannedDose
//    /// - вычисляет время напоминания
//    /// - отменяет напоминание при TakenDose
//    /// - вызывает порт уведомлений при наступлении времени
//    /// </summary>
//    let bindReminder (notify: INotificationService) (stream: CourseStream) =

//        stream.Events
//        |> Observable.choose (function
//            | PlannedDose dose -> Some dose
//            | AdjustedDose dose -> Some dose
//            | _ -> None)
//        |> Observable.flatMap (fun dose ->
//            let plannedTime =
//                dose.PlannedDate
//                    .Add(dose.PlannedTime)
//                    .AddMinutes(float -dose.NotifyBeforeMinutes)

//            let cancelStream =
//                stream.Events
//                |> Observable.filter (function
//                    | TakenDose d when d.Id = dose.Id -> true
//                    | _ -> false)

//            Observable.timer(plannedTime - DateTime.UtcNow)
//            |> Observable.takeUntil(cancelStream)
//            |> Observable.map (fun _ -> dose)
//        )
//        |> Observable.subscribe (fun dose ->
//            notify.SendReminder(dose)
//        )
