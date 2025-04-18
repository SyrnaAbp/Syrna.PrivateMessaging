﻿using Volo.Abp.Domain.Services;
using Volo.Abp.Users;

namespace Syrna.PrivateMessaging.MainDemo
{
    public abstract class MainDemoDomainService : DomainService
    {
        protected ICurrentUser CurrentUser => LazyServiceProvider.LazyGetRequiredService<ICurrentUser>();
    }
}