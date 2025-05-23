﻿using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace Syrna.PrivateMessaging.MongoDB
{
    public class PrivateMessagingMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public PrivateMessagingMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}