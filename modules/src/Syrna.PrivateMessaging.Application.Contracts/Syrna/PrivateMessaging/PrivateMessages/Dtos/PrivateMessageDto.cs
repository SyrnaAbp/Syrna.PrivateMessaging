using System;
using Syrna.PrivateMessaging.Users.Dtos;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Syrna.PrivateMessaging.PrivateMessages.Dtos
{
    public class PrivateMessageDto : ExtensibleFullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public Guid? FromUserId { get; set; }
        
        public Guid ToUserId { get; set; }

        [CanBeNull]
        public PmUserDto FromUser { get; set; }
        
        public PmUserDto ToUser { get; set; }
        
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime? ReadTime { get; set; }

        public string ConcurrencyStamp { get; set; }

    }
}