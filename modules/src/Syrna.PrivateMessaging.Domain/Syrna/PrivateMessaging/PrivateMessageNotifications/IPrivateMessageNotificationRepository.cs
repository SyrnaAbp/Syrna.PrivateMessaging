using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Syrna.PrivateMessaging.PrivateMessageNotifications
{
    public interface IPrivateMessageNotificationRepository : IRepository<PrivateMessageNotification, Guid>
    {
        Task<long> CountByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        
        Task<IReadOnlyList<PrivateMessageNotification>> GetListByUserIdAsync(Guid userId, int skipCount,
            int maxResultCount, CancellationToken cancellationToken = default);

        Task DeleteByPrivateMessageIdAsync(IEnumerable<Guid> privateMessageIds,
            CancellationToken cancellationToken = default);
    }
}