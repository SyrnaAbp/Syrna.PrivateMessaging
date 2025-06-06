﻿using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Syrna.PrivateMessaging.MongoDB
{
    [ConnectionStringName(PrivateMessagingDbProperties.ConnectionStringName)]
    public interface IPrivateMessagingMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
