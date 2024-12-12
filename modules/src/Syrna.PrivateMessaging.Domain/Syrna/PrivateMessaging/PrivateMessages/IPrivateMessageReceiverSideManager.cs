using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Syrna.PrivateMessaging.PrivateMessages
{
    public interface IPrivateMessageReceiverSideManager : IDomainService
    {
        Task SetReadAsync(PrivateMessage privateMessage);
        
        Task<long> CountAsync(Guid userId, bool unreadOnly = false);

        Task<IReadOnlyList<PrivateMessage>> GetListAsync(Guid userId, int skipCount, int maxResultCount, string sorting,
            bool unreadOnly = false);

        Task DeleteAsync(PrivateMessage privateMessage);
    }
}