using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Syrna.PrivateMessaging.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Syrna.PrivateMessaging.PrivateMessageNotifications
{
    public class PrivateMessageNotificationRepository :
        EfCoreRepository<IPrivateMessagingDbContext, PrivateMessageNotification, Guid>,
        IPrivateMessageNotificationRepository
    {
        public PrivateMessageNotificationRepository(IDbContextProvider<IPrivateMessagingDbContext> dbContextProvider) :
            base(dbContextProvider)
        {
        }

        public virtual async Task<long> CountByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await (await GetQueryableAsync()).Where(n => n.UserId == userId).CountAsync(cancellationToken);
        }

        public virtual async Task<IReadOnlyList<PrivateMessageNotification>> GetListByUserIdAsync(Guid userId, int skipCount,
            int maxResultCount, CancellationToken cancellationToken = default)
        {
            return await (await GetQueryableAsync()).Where(n => n.UserId == userId).PageBy(skipCount, maxResultCount)
                .ToListAsync(cancellationToken);
        }

        public virtual async Task DeleteByPrivateMessageIdAsync(IEnumerable<Guid> privateMessageIds, CancellationToken cancellationToken = default)
        {
            await DeleteAsync(n => privateMessageIds.Contains(n.PrivateMessageId), true, cancellationToken);
        }
    }
}