using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Syrna.PrivateMessaging.PrivateMessages;
using Syrna.PrivateMessaging.PrivateMessageNotifications;

namespace Syrna.PrivateMessaging.EntityFrameworkCore
{
    [ConnectionStringName(PrivateMessagingDbProperties.ConnectionStringName)]
    public class PrivateMessagingDbContext : AbpDbContext<PrivateMessagingDbContext>, IPrivateMessagingDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */
        public DbSet<PrivateMessage> PrivateMessages { get; set; }
        public DbSet<PrivateMessageNotification> PrivateMessageNotifications { get; set; }
        public PrivateMessagingDbContext(DbContextOptions<PrivateMessagingDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigurePrivateMessaging();
        }
    }
}
