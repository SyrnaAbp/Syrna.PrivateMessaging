using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Syrna.PrivateMessaging.PrivateMessages;
using Syrna.PrivateMessaging.PrivateMessageNotifications;

namespace Syrna.PrivateMessaging.EntityFrameworkCore
{
    [ConnectionStringName(PrivateMessagingDbProperties.ConnectionStringName)]
    public interface IPrivateMessagingDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
        DbSet<PrivateMessage> PrivateMessages { get; set; }
        DbSet<PrivateMessageNotification> PrivateMessageNotifications { get; set; }
    }
}
