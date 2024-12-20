using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Syrna.PrivateMessaging.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Volo.Abp;

namespace Syrna.PrivateMessaging.PrivateMessages
{
    public static class LinqExt
    {
        public static IQueryable<T> OrderByIff<T>(this IQueryable<T> query, bool condition, string sorting)
        {
            Check.NotNull(query, nameof(query));
            return condition
                ? DynamicQueryableExtensions.OrderBy(query, sorting)
                : query;
        }
    }
    public class PrivateMessageRepository : EfCoreRepository<IPrivateMessagingDbContext, PrivateMessage, Guid>,
        IPrivateMessageRepository
    {
        public PrivateMessageRepository(IDbContextProvider<IPrivateMessagingDbContext> dbContextProvider) : base(
            dbContextProvider)
        {
        }

        public virtual async Task<IReadOnlyList<PrivateMessage>> GetListAsync(IEnumerable<Guid> ids,
            bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            var query = includeDetails ? await WithDetailsAsync() : await GetQueryableAsync();

            return await query.Where(p => ids.Contains(p.Id)).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> CountSendingAsync(Guid userId, bool unreadOnly = false,
            CancellationToken cancellationToken = default)
        {
            return await (await GetQueryableAsync())
                .Where(m => m.FromUserId == userId && (!unreadOnly || !m.ReadTime.HasValue))
                .CountAsync(cancellationToken);
        }

        public virtual async Task<IReadOnlyList<PrivateMessage>> GetListSendingAsync(Guid userId, int skipCount,
            int maxResultCount, string sorting, bool unreadOnly = false, CancellationToken cancellationToken = default)
        {
            return await (await GetQueryableAsync())
                .Where(m => m.FromUserId == userId && (!unreadOnly || !m.ReadTime.HasValue))
                //.OrderByDescending(m => m.CreationTime)
                .OrderByIff(!sorting.IsNullOrEmpty(), sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(cancellationToken);
        }

        public virtual async Task<long> CountReceivingAsync(Guid userId, bool unreadOnly = false,
            CancellationToken cancellationToken = default)
        {
            return await (await GetQueryableAsync())
                .Where(m => m.ToUserId == userId && (!unreadOnly || !m.ReadTime.HasValue))
                .CountAsync(cancellationToken);
        }

        public virtual async Task<IReadOnlyList<PrivateMessage>> GetListReceivingAsync(Guid userId, int skipCount,
            int maxResultCount, string sorting, bool unreadOnly = false, CancellationToken cancellationToken = default)
        {
            return await (await GetQueryableAsync())
                .Where(m => m.ToUserId == userId && (!unreadOnly || !m.ReadTime.HasValue))
                 //.OrderByDescending(m => m.CreationTime)
                 .OrderByIff(!sorting.IsNullOrEmpty(), sorting)
                 .PageBy(skipCount, maxResultCount)
               .ToListAsync(cancellationToken);
        }
    }
}