// ------------------------------------------------------------
// Application/UseCases.fs
// ------------------------------------------------------------
// Use Cases — сценарии использования.
// Они связывают домен (валидацию, бизнес-логику)
// с портами (логирование, сохранение в БД).
//
// Здесь происходит orchestration: последовательность действий.
// Но нет инфраструктуры — только вызовы портов.
// ------------------------------------------------------------

namespace Application

//open Domain
//open Domain.Ports

//module CreateOrder =
//    let execute (logger: Logger) validate save order =
//        match validate order with
//        | Ok valid ->
//            logger.Info "Order validated"
//            save valid
//        | Error err ->
//            logger.Error $"Validation failed: {err}"
//            Error err
