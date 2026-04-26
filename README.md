## Tech Stack

--.NET 10
--ASP.NET Core Minimal APIs
--Entity Framework Core
--PostgreSQL (via Npgsql)


##Features

--Add books to the library
--Register members
--Borrow a book (with availability check)
--View all active loans with book title and member name
--Automatic audit logging on every database write via SaveChangesInterceptor
-- Global query filter — inactive members are excluded from all queries automatically
