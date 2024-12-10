using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Syrna.PrivateMessaging.PrivateMessageNotifications
{
    public interface IPrivateMessageNotificationManager : IDomainService
    {
        Task<IReadOnlyList<PrivateMessageNotification>> GetListAsync(Guid userId, int skipCount, int maxResultCount);
    }
}