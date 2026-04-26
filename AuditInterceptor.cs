using System.Xml.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

public class AuditInterceptor : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync
        (DbContextEventData eventData, InterceptionResult<int> result, 
         CancellationToken cancellationToken= default)
    {
        var context = eventData.Context;
        if (context == null) return await base.SavingChangesAsync(eventData, result, cancellationToken);

        var entires= context.ChangeTracker.Entries().Where(e=>
            e.State==EntityState.Added ||
            e.State== EntityState.Modified ||
            e.State== EntityState.Deleted)
            .ToList();

        var logs = entires.Select(entry=> new AuditLog
        {
            EntityName= entry.Entity.GetType().Name,
            Action= entry.State.ToString(),
            OccuredAt= DateTime.UtcNow
        }).ToList();

        context.Set<AuditLog>().AddRange(logs);
        return await base.SavingChangesAsync(eventData, result,cancellationToken);
    }
}