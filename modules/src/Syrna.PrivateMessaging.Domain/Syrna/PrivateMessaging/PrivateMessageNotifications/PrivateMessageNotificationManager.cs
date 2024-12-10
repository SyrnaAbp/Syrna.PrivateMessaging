using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Syrna.PrivateMessaging.PrivateMessageNotifications
{
    public class PrivateMessageNotificationManager : DomainService, IPrivateMessageNotificationManager
    {
        private readonly IPrivateMessageNotificationRepository _repository;

        public PrivateMessageNotificationManager(
            IPrivateMessageNotificationRepository repository)
        {
            _repository = repository;
        }
        
        public virtual async Task<IReadOnlyList<PrivateMessageNotification>> GetListAsync(Guid userId, int skipCount, int maxResultCount)
        {
            return await _repository.GetListByUserIdAsync(userId, skipCount, maxResultCount);
        }
    }
}