// ------------------------------------------------------------
// Infrastructure/SqlAdapter.fs
// ------------------------------------------------------------
// Адаптер для работы с БД.
// Реализует порт SaveOrder.
// Здесь может быть EF Core, Dapper, Npgsql — домен не знает.
// ------------------------------------------------------------

namespace Infrastructure

//open Domain
//open Domain.Ports

//module SqlOrderRepository =
//    let saveOrder order =
//        // Здесь будет SQL-логика
//        Ok order.Id
