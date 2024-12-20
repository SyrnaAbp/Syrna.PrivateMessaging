using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.ObjectExtending;

namespace Syrna.PrivateMessaging.PrivateMessages.Dtos
{
    [Serializable]
    public class UpdatePrivateMessageDto : CreateOrUpdatePrivateMessageDto, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; }
    }
}