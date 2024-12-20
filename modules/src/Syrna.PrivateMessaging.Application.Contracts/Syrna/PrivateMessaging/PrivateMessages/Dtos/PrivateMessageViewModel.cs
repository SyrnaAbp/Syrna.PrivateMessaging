using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Syrna.PrivateMessaging.Users.Dtos;
using Volo.Abp.Application.Dtos;

namespace Syrna.PrivateMessaging.PrivateMessages.Dtos
{
    public class PrivateMessageViewModel : ExtensibleEntityDto<Guid>
    {

        public Guid? FromUserId { get; set; }

        public Guid ToUserId { get; set; }

        [CanBeNull]
        public PmUserDto FromUser { get; set; }

        public string FromUserName => FromUser == null ? "SYSTEM" : FromUser.UserName;

        public PmUserDto ToUser { get; set; }

        public string ToUserName => ToUser == null ? "SYSTEM" : ToUser.UserName;

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime? ReadTime { get; set; }
        
        [UIHint("Hidden")]
        public string ConcurrencyStamp { get; set; }

        public virtual Guid? CreatorId { get; set; }

        public DateTime CreationTime { get; set; }
    }
}