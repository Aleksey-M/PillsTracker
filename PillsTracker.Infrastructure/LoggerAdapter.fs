// ------------------------------------------------------------
// Infrastructure/LoggerAdapter.fs
// ------------------------------------------------------------
// Адаптер логирования.
// Реализует порт Logger, используя stdout.
// Можно заменить на Serilog, Seq, Elastic — домен не заметит.
// ------------------------------------------------------------

namespace Infrastructure

//open Domain.Ports

//module StdoutLogger =
//    let logger =
//        { Info = fun msg -> printfn "[INFO] %s" msg
//          Error = fun msg -> eprintfn "[ERROR] %s" msg }
