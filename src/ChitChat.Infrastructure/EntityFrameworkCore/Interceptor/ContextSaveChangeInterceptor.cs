using ChitChat.Domain.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.DataAccess.Data.Interceptor
{
    public class ContextSaveChangeInterceptor : SaveChangesInterceptor
    {
        //private readonly IClaimService _claimService;

        //public ContextSaveChangeInterceptor(IClaimService claimService)
        //{
        //    _claimService = claimService;
        //}

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            var dbContext = eventData.Context;


            if (dbContext is not null)
            {
                foreach (var entry in dbContext.ChangeTracker.Entries<IAuditedEntity>())
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            //entry.Entity.CreatedBy = _claimService.GetUserId();
                            //entry.Entity.CreatedOn = DateTime.UtcNow;
                            break;
                        case EntityState.Modified:
                            //entry.Entity.UpdatedBy = _claimService.GetUserId();
                            //entry.Entity.UpdatedOn = DateTime.UtcNow;
                            break;
                    }
                }


            }
            var saveChangeResult = await base.SavingChangesAsync(eventData, result, cancellationToken);
            return saveChangeResult;
        }
    }
}
