﻿using System;
using JetBrains.Annotations;
using Volo.Abp.MultiTenancy;

namespace Syrna.PrivateMessaging.PrivateMessages;

[Serializable]
public class SendPrivateMessageEto : CreatePrivateMessageInfoModel, IMultiTenant
{
    public Guid? TenantId { get; set; }

    public SendPrivateMessageEto()
    {
    }

    public SendPrivateMessageEto(Guid? tenantId, Guid? fromUserId, Guid toUserId, [NotNull] string title,
        [CanBeNull] string content) : base(fromUserId, toUserId, title, content)
    {
        TenantId = tenantId;
        FromUserId = fromUserId;
        ToUserId = toUserId;
        Title = title;
        Content = content;
    }
}