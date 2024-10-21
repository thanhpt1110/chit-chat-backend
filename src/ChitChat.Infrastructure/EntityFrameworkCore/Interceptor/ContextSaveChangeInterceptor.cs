using ChitChat.Application.Helpers;
using ChitChat.Domain.Common;

using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ChitChat.DataAccess.Data.Interceptor
{
    public class ContextSaveChangeInterceptor : SaveChangesInterceptor
    {
        private readonly IClaimService _claimService;

        public ContextSaveChangeInterceptor(IClaimService claimService)
        {
            _claimService = claimService;
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            var dbContext = eventData.Context;


            if (dbContext is not null)
            {

                foreach (var entry in dbContext.ChangeTracker.Entries<BaseEntity>())
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            if (entry.Entity.Id == Guid.Empty)
                                entry.Entity.Id = Guid.NewGuid(); // Sử dụng Guid.NewGuid() thay vì new Guid()

                            if (entry.Entity is IAuditedEntity auditedEntity)
                            {
                                auditedEntity.CreatedBy = _claimService.GetUserId(); // Cast entity sang IAuditedEntity
                                auditedEntity.CreatedOn = DateTime.UtcNow;
                                auditedEntity.UpdatedBy = auditedEntity.CreatedBy;
                                auditedEntity.UpdatedOn = auditedEntity.CreatedOn;
                            }
                            break;

                        case EntityState.Modified:
                            if (entry.Entity is IAuditedEntity auditedEntityModify)
                            {
                                auditedEntityModify.UpdatedBy = _claimService.GetUserId();
                                auditedEntityModify.UpdatedOn = DateTime.UtcNow;
                            }
                            break;
                    }
                }
            }
            var saveChangeResult = await base.SavingChangesAsync(eventData, result, cancellationToken);
            return saveChangeResult;
        }
    }
}
